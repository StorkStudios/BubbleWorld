using System.Xml.Serialization;

public class Speech : ChapterElement
{
    public readonly string characterName;
    public readonly string characterId;
    public readonly string voiceline;
    public readonly float? duration;

    public Speech(string character, string voiceline, string characterId, float? duration)
    {
        characterName = character;
        this.voiceline = voiceline.Trim();
        this.characterId = characterId;
        this.duration = duration;
    }
}
