
using System.Xml.Serialization;

[XmlRoot("Enter")]
public class Enter
{
    [XmlAttribute("character")]
    public string Character { get; set; }
}