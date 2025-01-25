using System.Xml;
using UnityEngine;

public class SpeechHandler : ChapterElementHandler
{
    private Speech speech;

    public override void Init(XmlNode node)
    {
        base.Init(node);
        
        XmlAttribute character = node.Attributes["character"];
        XmlAttribute characterId = node.Attributes["character_id"];
        speech = new Speech(character?.InnerText ?? "", node.InnerXml, characterId?.InnerText);
    }

    public override void Enter()
    {
        Debug.Log($"Speech node: {speech.characterName}: {speech.voiceline}");
        Director.Instance.ElementReadEvent?.Invoke("Speech", speech);
    }
}
