
using System.Xml.Serialization;

[XmlRoot("Enter")]
public class Enter : ChapterElement
{
    [XmlAttribute("character_id")]
    public string characterId;

    [XmlAttribute("eyes")]
    public CharacterData.EyesPose eyes;

    [XmlAttribute("mouth")]
    public CharacterData.MouthPose mouth;

    [XmlAttribute("position")]
    public float position;
}