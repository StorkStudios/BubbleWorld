using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Options : ChapterElement
{
    public readonly float? duration;
    public readonly List<Option> options;

    public Options(float? duration, List<Option> options)
    {
        this.duration = duration;
        this.options = options;
    }
}
