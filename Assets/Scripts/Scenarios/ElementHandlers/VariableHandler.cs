using System.Xml;
using UnityEngine;

public class VariableHandler : ChapterElementHandler
{
    public override void Init(XmlNode node)
    {
        base.Init(node);
        
        string name = node.Attributes["name"].InnerText;
        string valueString = node.Attributes["value"].InnerText;
        int value = int.Parse(valueString);
        StoryVariablesManager.Instance.Variables[name] = value;
    }
}
