using System.Collections.Generic;
using UnityEngine;

public class DrinkData
{
    public TeaBase? teaBase;
    public TeaSyroup? teaSyroup;
    public TeaSyroup? secondSyroup;
    public TeaJelly? firstJelly;
    public TeaJelly? secondJelly;
    public TeaJelly? thirdJelly;

    public IEnumerable<Sprite> GetSprites(TeaIconMap iconMap)
    {
        if (teaBase.HasValue)
        {
            yield return iconMap.TeaBases[teaBase.Value];
        }
        if (teaSyroup.HasValue)
        {
            yield return iconMap.TeaSyroups[teaSyroup.Value];
        }
        if (secondSyroup.HasValue)
        {
            yield return iconMap.TeaSyroups[secondSyroup.Value];
        }
        if (firstJelly.HasValue)
        {
            yield return iconMap.TeaJellies[firstJelly.Value];
        }
        if (secondJelly.HasValue)
        {
            yield return iconMap.TeaJellies[secondJelly.Value];
        }
        if (thirdJelly.HasValue)
        {
            yield return iconMap.TeaJellies[thirdJelly.Value];
        }
    }
}
