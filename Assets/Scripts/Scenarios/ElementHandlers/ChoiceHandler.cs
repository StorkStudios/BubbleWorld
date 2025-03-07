using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class ChoiceHandler : ChapterElementHandler
{
    private enum ChoiceType
    {
        Options,
        Variable
    }

    private string stepParameter;
    private Options options;
    private bool choiceReturned = false;
    private ChoiceType choiceType;
    private string variableName;

    public override void Init(XmlNode node)
    {
        base.Init(node);

        XmlNode optionsNode = node.SelectSingleNode("Options");
        if (optionsNode != null)
        {
            choiceType = ChoiceType.Options;
            XmlAttribute duration = optionsNode.Attributes["duration"];
            float? durationValue = duration != null ? float.Parse(duration.InnerText) : null;
            XmlAttribute defaultId = optionsNode.Attributes["default_id"];
            string defaultIdValue = defaultId?.InnerText;
            List<Option> optionsList = new List<Option>();
            foreach (XmlNode optionNode in optionsNode.SelectNodes("Option"))
            {
                string text = optionNode.InnerXml;
                string id = optionNode.Attributes["id"].InnerText;
                optionsList.Add(new Option(id, text));
            }
            options = new Options(durationValue, optionsList, defaultIdValue);
        }
        else
        {
            choiceType = ChoiceType.Variable;
            XmlNode variableNode = node.SelectSingleNode("Variable");
            if (variableNode == null)
            {
                Debug.LogError("No Variable nor Options node not found in Choice element");
                return;
            }
            variableName = variableNode.Attributes["name"].InnerText;
        }
    }

    public override IEnumerator Enter()
    {
        if (choiceType == ChoiceType.Options)
        {
            Director.Instance.DirectorStepEvent += OnDirectorStep;
            Director.Instance.ElementReadEvent?.Invoke("Options", options);
            yield return new WaitUntil(() => stepParameter != null);
            Director.Instance.DirectorStepEvent -= OnDirectorStep;
        }
        else
        {
            yield break;
        }
    }

    public override XmlNode GetNextChild()
    {
        if (choiceReturned)
        {
            return null;
        }
        
        XmlNode selectedNode;
        if (choiceType == ChoiceType.Options)
        {
            selectedNode = Node.SelectSingleNode($"Switch/Case[@option_id='{stepParameter}']");
            if (selectedNode == null)
            {
                Debug.LogWarning($"Selected option not found: {stepParameter}. Select is being skipped");
            }
        }
        else
        {
            int value = StoryVariablesManager.Instance.Variables[variableName];
            selectedNode = Node.SelectSingleNode($"Switch/Case[@value='{value}']");
            if (selectedNode == null)
            {
                Debug.LogWarning($"Variable value not found: {variableName}. Select is being skipped");
            }
        }
        choiceReturned = true;
        return selectedNode;
    }

    private void OnDirectorStep(string selectedOptionId)
    {
        if (selectedOptionId == "bulech")
        {
            Debug.Log(selectedOptionId);
        }
        stepParameter = selectedOptionId;
    }
}
