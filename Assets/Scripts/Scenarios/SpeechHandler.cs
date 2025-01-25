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
        
        speech = new Speech();
        XmlAttribute character = node.Attributes["character"];
        speech.characterName = character?.InnerText;
        speech.voiceline = node.InnerXml;
    }

    public override void Enter()
    {
        Debug.Log($"Speech node: {speech.characterName}: {speech.voiceline}");
        Director.Instance.ElementReadEvent?.Invoke("Speech", speech);
    }
}
