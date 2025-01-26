using System.Xml.Serialization;
using UnityEngine;

[XmlRoot("GameOver")]
public class GameOver : ChapterElement
{
    [XmlAttribute("text")]
    public string text;

    [XmlAttribute("background")]
    public string background;
}
