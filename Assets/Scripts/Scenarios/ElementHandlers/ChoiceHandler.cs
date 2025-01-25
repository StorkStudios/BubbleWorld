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
        }
    }

    public override IEnumerator Enter()
    {
        Director.Instance.DirectorStepEvent += OnDirectorStep;
        Director.Instance.ElementReadEvent?.Invoke("Options", options);
        yield return new WaitUntil(() => stepParameter != null);
        Director.Instance.DirectorStepEvent -= OnDirectorStep;
    }

    public override XmlNode GetNextChild()
    {
        if (choiceReturned)
        {
            return null;
        }
        
        XmlNode selectedNode = Node.SelectSingleNode($"Switch/Case[@option_id='{stepParameter}']");
        if (selectedNode == null && choiceType == ChoiceType.Options)
        {
            Debug.LogError($"Selected option not found: {stepParameter}");
        }
        choiceReturned = true;
        return selectedNode;
    }

    public override IEnumerator Exit()
    {
        yield return null;
    }

    private void OnDirectorStep(string selectedOptionId)
    {
        stepParameter = selectedOptionId;
    }
}
