using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class ChapterElementDeserializer<T> where T : ChapterElement 
{
    private XmlSerializer serializer = new XmlSerializer(typeof(T));

    public T Deserialize(XmlNode node)
    {
        return (T)serializer.Deserialize(new StringReader(node.OuterXml));
    }
}
