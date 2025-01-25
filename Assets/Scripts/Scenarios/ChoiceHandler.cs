using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class ChoiceHandler : ChapterElementHandler
{
    public override void Init(XmlNode node)
    {
        base.Init(node);

        XmlNode optionsNode = node.SelectSingleNode("Options");
        XmlAttribute duration = optionsNode.Attributes["duration"];
        float? durationValue = duration != null ? float.Parse(duration.InnerText) : null;
        List<Option> optionsList = new List<Option>();
        foreach (XmlNode optionNode in optionsNode.SelectNodes("Option"))
        {
            string text = optionNode.InnerXml;
            string id = optionNode.Attributes["id"].InnerText;
            optionsList.Add(new Option(id, text));
        }
        Options options = new Options(durationValue, optionsList);
        Director.Instance.ElementReadEvent?.Invoke("Options", options);
    }

    public override void Enter()
    {

    }

    public override XmlNode GetNextChild()
    {
        return null;
    }

    public override void Exit()
    {

    }
}
