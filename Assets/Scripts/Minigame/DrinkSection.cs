using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrinkSection : MonoBehaviour
{
    [SerializeField]
    private List<Image> ingedientsPlaces;
    [SerializeField]
    private TeaIconMap iconMap;
    [SerializeField]
    private RectTransform drinkTransform;

    private Tween positionTween = null;
    private Tween rotationTween = null;

    public void SetShakeDrink(bool shake)
    {
        if (shake)
        {
            positionTween = drinkTransform.DOShakePosition(9999);
            rotationTween = drinkTransform.DOShakeRotation(9999);
        }
        else
        {
            positionTween.Complete();
            rotationTween.Complete();
            positionTween = null;
            rotationTween = null;
        }
    }

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
