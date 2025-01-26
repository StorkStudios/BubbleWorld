using System.Collections;
using System.Xml;
using UnityEngine;

public class MinigameEndHandler : ChapterElementHandler
{
    private ChapterElementDeserializer<MinigameEnd> reader = new ChapterElementDeserializer<MinigameEnd>();

    private MinigameEnd minigameEnd;
    private bool finished = false;

    public override void Init(XmlNode node)
    {
        base.Init(node);

        minigameEnd = reader.Deserialize(node);
    }

    public override IEnumerator Enter()
    {
        Director.Instance.DirectorStepEvent += OnDirectorStep;
        Director.Instance.ElementReadEvent?.Invoke("MinigameEnd", minigameEnd);
        yield return new WaitUntil(() => finished);
        Director.Instance.DirectorStepEvent -= OnDirectorStep;
    }

    private void OnDirectorStep(string minigameResult)
    {
        finished = true;
        StoryVariablesManager.Instance.Variables["MinigameResult"] = int.Parse(minigameResult);
    }
}
