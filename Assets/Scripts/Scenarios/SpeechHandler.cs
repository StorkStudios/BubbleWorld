using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

public class SpeechHandler : ChapterElementHandler
{
    private Speech speech;

    private ChapterElementReader<Speech> reader = new ChapterElementReader<Speech>();

    public override void Init(XmlNode node)
    {
        base.Init(node);
        
        speech = reader.Deserialize(node);
    }

    public override void Enter()
    {
        Debug.Log($"Speech node: {speech.characterName}: {speech.voiceline}");
    }
}
