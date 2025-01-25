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

    public virtual bool HasChild()
    {
        return Node.HasChildNodes;
    }

    public virtual XmlNode GetChild()
    {
        return Node.FirstChild;
    }

    public virtual void Exit()
    {

    }
}