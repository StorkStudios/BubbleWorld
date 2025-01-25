
using System.Xml.Serialization;

[XmlRoot("Update")]
public class Update : ChapterElement
{
    [XmlAttribute("character_id")]
    public string characterId;

    [XmlAttribute("eyes")]
    public string eyesPose;

    [XmlAttribute("mouth")]
    public string mouthPose;

    [XmlAttribute("position")]
    public string positionValue;

    [XmlIgnore]
    public CharacterData.EyesPose? eyes
    {
        get
        {
            return eyesPose == null ? null :(CharacterData.EyesPose)System.Enum.Parse(typeof(CharacterData.EyesPose), eyesPose);
        }
    }

    [XmlIgnore]
    public CharacterData.MouthPose? mouth
    {
        get
        {
            return mouthPose == null ? null : (CharacterData.MouthPose)System.Enum.Parse(typeof(CharacterData.MouthPose), mouthPose);
        }
    }

    [XmlIgnore]
    public float? position
    {
        get
        {
            return positionValue == null ? null : float.Parse(positionValue);
        }
    }
}