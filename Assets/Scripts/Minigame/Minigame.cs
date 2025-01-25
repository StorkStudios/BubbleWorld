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
        (ingredientsSection.transform as RectTransform).DOAnchorPos(Vector2.zero, animationDuration);
        (drinkSection.transform as RectTransform).DOAnchorPos(Vector2.zero, animationDuration);
        (controlsSection.transform as RectTransform).DOAnchorPos(Vector2.zero, animationDuration);
    }

    private void EndMinigame()
    {
        AnimateRectTransform(ingredientsSection.transform as RectTransform);
        AnimateRectTransform(drinkSection.transform as RectTransform);
        AnimateRectTransform(controlsSection.transform as RectTransform);
    }

    private void AnimateRectTransform(RectTransform rectTransform)
    {
        Vector2 target = rectTransform.sizeDelta;
        target.x = Mathf.Min(target.x, 0);
        target.y = Mathf.Min(target.y, 0);
        target.y *= -1;
        rectTransform.DOAnchorPos(target, animationDuration);
    }
}
