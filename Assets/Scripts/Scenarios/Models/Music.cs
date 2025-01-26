using System.Xml.Serialization;
using UnityEngine;

[XmlRoot("Music")]
public class Music : ChapterElement
{
    [XmlAttribute("name")]
    public string name;
}
