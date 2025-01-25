using DG.Tweening;
using UnityEngine;

public class Minigame : MonoBehaviour
{
    [SerializeField]
    private float animationDuration;
    [SerializeField]
    private IngredientsSection ingredientsSection;
    [SerializeField]
    private DrinkSection drinkSection;
    [SerializeField]
    private ControlsSection controlsSection;

    private void Start()
    {
        Director.Instance.ElementReadEvent += OnElementRead;
    }

    private void OnElementRead(string element, ChapterElement values)
    {
        switch (element)
        {
            case "MinigameStart":
                StartMinigame(values as MinigameStart);
                break;
            case "MinigameEnd":
                EndMinigame();
                break;
        }
    }

    private void StartMinigame(MinigameStart values)
    {
        AnimateInRectTransform(ingredientsSection.transform as RectTransform);
        AnimateInRectTransform(drinkSection.transform as RectTransform);
        AnimateInRectTransform(controlsSection.transform as RectTransform);
    }

    private void EndMinigame()
    {
        AnimateOutRectTransform(ingredientsSection.transform as RectTransform);
        AnimateOutRectTransform(drinkSection.transform as RectTransform);
        AnimateOutRectTransform(controlsSection.transform as RectTransform);
    }

    private void AnimateOutRectTransform(RectTransform rectTransform)
    {
        Vector2 target = rectTransform.sizeDelta;
        if (target.x < 0)
        {
            target.x = rectTransform.anchoredPosition.x;
        }
        if (target.y < 0)
        {
            target.y = rectTransform.anchoredPosition.y;
        }
        else
        {
            target.y *= -1;
        }
        rectTransform.DOAnchorPos(target, animationDuration);
    }

    private void AnimateInRectTransform(RectTransform rectTransform)
    {
        Vector2 target = rectTransform.sizeDelta;
        if (target.x < 0)
        {
            target.x = rectTransform.anchoredPosition.x;
        }
        else
        {
            target.x = 0;
        }
        if (target.y < 0)
        {
            target.y = rectTransform.anchoredPosition.y;
        }
        else
        {
            target.y = 0;
        }
        rectTransform.DOAnchorPos(target, animationDuration);
    }
}
