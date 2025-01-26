using System.Globalization;
using System.Xml.Serialization;
using UnityEngine;

[XmlRoot("Exit")]
public class Exit : ChapterElement
{
    [XmlAttribute("character_id")]
    public string characterId;

    [XmlAttribute("duration")]
    public string durationValue;

    [XmlIgnore]
    public float? duration
    {
        get
        {
            return durationValue == null ? null : float.Parse(durationValue, CultureInfo.InvariantCulture);
        }
    }
}
