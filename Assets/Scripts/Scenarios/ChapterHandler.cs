using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class ChapterHandler : ChapterElementHandler
{
    private Chapter chapter;

    private ChapterElementReader<Chapter> reader = new ChapterElementReader<Chapter>();

    private IEnumerator children;

    public override void Init(XmlNode node)
    {
        base.Init(node);
        
        chapter = reader.Deserialize(node);
        children = node.GetEnumerator();
    }

    public override void Enter()
    {
        
    }

    public override XmlNode GetNextChild()
    {
        return children.MoveNext() ? (XmlNode)children.Current : null;
    }
}
