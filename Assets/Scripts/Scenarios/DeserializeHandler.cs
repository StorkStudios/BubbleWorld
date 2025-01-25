using System.Collections;
using System.Xml;
using UnityEngine;

public class DeserializeHandler<T> : ChapterElementHandler where T : ChapterElement
{
    private ChapterElementReader<T> reader = new ChapterElementReader<T>();

    private T element;

    public override void Init(XmlNode node)
    {
        base.Init(node);

        element = reader.Deserialize(node);
    }

    public override IEnumerator Enter()
    {
        Director.Instance.ElementReadEvent?.Invoke(typeof(T).Name, element);
        yield return null;
    }
}
