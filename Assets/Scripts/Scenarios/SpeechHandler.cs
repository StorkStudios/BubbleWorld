using System.Collections;
using System.Xml;
using UnityEngine;

public class SpeechHandler : ChapterElementHandler
{
    private Speech speech;

    private bool finished = false;

    public override void Init(XmlNode node)
    {
        base.Init(node);
        
        XmlAttribute character = node.Attributes["character"];
        XmlAttribute characterId = node.Attributes["character_id"];
        speech = new Speech(character?.InnerText ?? "", node.InnerXml, characterId?.InnerText);
    }

    public override IEnumerator Enter()
    {
        Director.Instance.DirectorStepEvent += OnDirectorStep;
        Director.Instance.ElementReadEvent?.Invoke("Speech", speech);
        yield return new WaitUntil(() => finished);
        Director.Instance.DirectorStepEvent -= OnDirectorStep;
    }

    private void OnDirectorStep(string _)
    {
        finished = true;
    }
}
