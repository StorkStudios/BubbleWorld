using System.Xml.Serialization;

public class Speech : ChapterElement
{
    public string characterName;
    public string voiceline;

    public Speech(string character, string voiceline)
    {
        characterName = character;
        this.voiceline = voiceline.Trim();
    }
}
