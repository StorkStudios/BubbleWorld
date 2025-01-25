using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class ChapterElementHandler
{
    public XmlNode Node { get; protected set; }

    public virtual void Init(XmlNode node)
    {
        Node = node;
    }

    public virtual IEnumerator Enter()
    {
        yield return null;
    }

    public virtual XmlNode GetNextChild()
    {
        return null;
    }

    public virtual IEnumerator Exit()
    {
        yield return null;
    }
}