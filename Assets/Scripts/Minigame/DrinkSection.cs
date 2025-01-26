using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrinkSection : MonoBehaviour
{
    [SerializeField]
    private List<Image> ingedientsPlaces;
    [SerializeField]
    private TeaIconMap iconMap;

    public void ShowDrink(DrinkData data)
    {
        foreach (Image image in ingedientsPlaces)
        {
            image.color = Color.clear;
        }

        int i = 0;
        foreach (Sprite sprite in data.GetSprites(iconMap))
        {
            if (sprite != null)
            {
                ingedientsPlaces[i].sprite = sprite;
                ingedientsPlaces[i].color = Color.white;
            }

            i++;
        }
    }
}
