using UnityEngine;

public readonly struct DialogueValues
{
    public readonly string characterName;
    public readonly string voiceline;

    public DialogueValues(string characterName, string voiceline)
    {
        this.characterName = characterName;
        this.voiceline = voiceline;
    }
}
