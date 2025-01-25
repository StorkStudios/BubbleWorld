using System.Xml.Serialization;
using UnityEngine;

[XmlRoot("Background")]
public class Background : ChapterElement
{
    [XmlAttribute("name")]
    public string Name;
}
