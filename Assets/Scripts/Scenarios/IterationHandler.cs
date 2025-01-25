using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class IterationHandler : ChapterElementHandler
{
    private IEnumerator children;

    public override void Init(XmlNode node)
    {
        base.Init(node);
        
        children = node.GetEnumerator();
    }

    public override XmlNode GetNextChild()
    {
        return children.MoveNext() ? (XmlNode)children.Current : null;
    }
}
