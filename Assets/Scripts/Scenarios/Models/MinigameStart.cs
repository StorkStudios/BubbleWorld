using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public enum TeaBase { EarlGray, Mint, Matcha }
public enum TeaSyroup { PassionFruit, Strawberry, Milk }
public enum TeaJelly { Chocolate, Almond, Banana }
public enum TeaMixage { Light, Medium, Hard }

public class MinigameStart : ChapterElement
{
    [XmlElement("TeaBase")]
    public TeaBase teaBase;

    [XmlElement("TeaSyroup")]
    public TeaSyroup teaSyroup;

    [XmlElement("SecondSyroup")]
    public string secondSyroupString;

    [XmlArray("TeaJellies")]
    [XmlArrayItem("Jelly")]
    public TeaJelly[] teaJellies;

    [XmlElement("TeaMixage")]
    public TeaMixage teaMixage;

    [XmlIgnore]
    public TeaSyroup? secondSyroup
    {
        get
        {
            if (secondSyroupString == null)
            {
                return null;
            }
            return (TeaSyroup)System.Enum.Parse(typeof(TeaSyroup), secondSyroupString);
        }
    }

    public MinigameStart()
    {

    }
}
