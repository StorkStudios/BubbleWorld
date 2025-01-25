using System.Xml.Serialization;

[XmlRoot("Speech")]
public class Speech : ChapterElement
{
    [XmlAttribute("character")]
    public string characterName;
    [XmlText]
    public string voiceline;

    public Speech()
    {
        
    }

    public Speech(string character, string voiceline)
    {
        characterName = character;
        this.voiceline = voiceline;
    }
}
