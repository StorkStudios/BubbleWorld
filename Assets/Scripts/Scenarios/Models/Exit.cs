using System.Xml.Serialization;
using UnityEngine;

[XmlRoot("Exit")]
public class Exit : ChapterElement
{
    [XmlAttribute("character_id")]
    public string characterId;
}
