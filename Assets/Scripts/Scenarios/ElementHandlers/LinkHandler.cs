using System.Xml;
using UnityEngine;

public class LinkHandler : ChapterElementHandler
{
    public override void Init(XmlNode node)
    {
        base.Init(node);
        
        string path = node.Attributes["path"].InnerText;
        GameManager.Instance.ChangeScript(path);
    }
}
