using System.Collections.Generic;
using System.Xml;

public class ChapterElementHandler
{
    public XmlNode Node { get; protected set; }

    public virtual void Init(XmlNode node)
    {
        Node = node;
    }

    public virtual void Enter()
    {

    }

    public virtual XmlNode GetNextChild()
    {
        return null;
    }

    public virtual void Exit()
    {

    }
}