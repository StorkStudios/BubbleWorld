using System.Xml.Serialization;

public class Speech : ChapterElement
{
    public string characterName;
    public string characterId;
    public string voiceline;

    public Speech(string character, string voiceline, string characterId)
    {
        characterName = character;
        this.voiceline = voiceline.Trim();
        this.characterId = characterId;
    }
}
