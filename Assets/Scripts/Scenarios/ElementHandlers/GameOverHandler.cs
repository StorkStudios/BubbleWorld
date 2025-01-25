using System.Collections;
using System.Xml;
using UnityEngine;

public class GameOverHandler : ChapterElementHandler
{
    private readonly ChapterElementDeserializer<GameOver> deserializer = new ChapterElementDeserializer<GameOver>();

    private GameOver gameOver;

    public override void Init(XmlNode node)
    {
        base.Init(node);

        gameOver = deserializer.Deserialize(node);
    }

    public override IEnumerator Enter()
    {
        Director.Instance.ElementReadEvent?.Invoke("GameOver", gameOver);
        yield return new WaitUntil(() => false);
    }
}
