using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

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

    private DrinkData currentDrink;
    private DrinkData targetDrink;
    private TeaMixage targetMixage;

    private bool doneClicked = false;

    private void Start()
    {
        Director.Instance.ElementReadEvent += OnElementRead;
        ingredientsSection.SetClickableBase(false);
        ingredientsSection.SetClickableSyroup(false);
        ingredientsSection.SetClickableJellies(false);
        ingredientsSection.BaseSelected += OnBaseSelected;
        ingredientsSection.SyroupSelected += OnSyroupSelected;
        ingredientsSection.JellySelected += OnJellySelected;
        controlsSection.Done += OnDone;
        controlsSection.Reset += OnReset;
        controlsSection.MixButtonStateChanged += drinkSection.SetShakeDrink;
    }

    private void OnDone()
    {
        ingredientsSection.SetClickableBase(false);
        ingredientsSection.SetClickableSyroup(false);
        ingredientsSection.SetClickableJellies(false);

        controlsSection.SetClickable(false);

        doneClicked = true;
    }

    private void OnReset()
    {
        currentDrink = new DrinkData();

        ingredientsSection.SetClickableBase(true);
        ingredientsSection.SetClickableSyroup(true);
        ingredientsSection.SetClickableJellies(true);

        controlsSection.SetClickable(true);
        controlsSection.currentMixage = null;
        controlsSection.OnButtonDownUpdate(0);

        drinkSection.ShowDrink(currentDrink);

        doneClicked = false;
    }

    private void OnBaseSelected(Button _, TeaBase teaBase)
    {
        currentDrink.teaBase = teaBase;
        ingredientsSection.SetClickableBase(false);
        drinkSection.ShowDrink(currentDrink);
    }

    private void OnSyroupSelected(Button _, TeaSyroup teaSyroup)
    {
        if (!currentDrink.teaSyroup.HasValue)
        {
            currentDrink.teaSyroup = teaSyroup;
        }
        else
        {
            currentDrink.secondSyroup = teaSyroup;
            ingredientsSection.SetClickableSyroup(false);
        }
        drinkSection.ShowDrink(currentDrink);
    }

    private void OnJellySelected(Button _, TeaJelly teaJelly)
    {
        if (!currentDrink.firstJelly.HasValue)
        {
            currentDrink.firstJelly = teaJelly;
        }
        else if (!currentDrink.secondJelly.HasValue)
        {
            currentDrink.secondJelly = teaJelly;
        }
        else
        {
            currentDrink.thirdJelly = teaJelly;
            ingredientsSection.SetClickableJellies(false);
        }
        drinkSection.ShowDrink(currentDrink);
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

        currentDrink = new DrinkData();
        targetDrink = new DrinkData()
        {
            teaBase = values.teaBase,
            teaSyroup = values.teaSyroup,
            secondSyroup = values.secondSyroup,
            firstJelly = values.teaJellies.Length > 0 ? values.teaJellies[0] : null,
            secondJelly = values.teaJellies.Length > 1 ? values.teaJellies[1] : null,
            thirdJelly = values.teaJellies.Length > 2 ? values.teaJellies[2] : null
        };
        targetMixage = values.teaMixage;

        ingredientsSection.SetClickableBase(true);
        ingredientsSection.SetClickableSyroup(true);
        ingredientsSection.SetClickableJellies(true);

        controlsSection.SetClickable(true);
        controlsSection.currentMixage = null;
        controlsSection.OnButtonDownUpdate(0);

        drinkSection.ShowDrink(currentDrink);

        doneClicked = false;
    }

    private void EndMinigame()
    {
        GameManager.Instance.CanSkip = false;

        AnimateOutRectTransform(ingredientsSection.transform as RectTransform);
        AnimateOutRectTransform(drinkSection.transform as RectTransform);
        AnimateOutRectTransform(controlsSection.transform as RectTransform).OnComplete(() => 
        {
            Director.Instance.DirectorStepEvent?.Invoke(GetRating().ToString());
            GameManager.Instance.CanSkip = true;
        });

        ingredientsSection.SetClickableBase(false);
        ingredientsSection.SetClickableSyroup(false);
        ingredientsSection.SetClickableJellies(false);

        controlsSection.SetClickable(false);
        controlsSection.OnButtonDownUpdate(0);
    }

    private int GetRating()
    {
        int v = 0;
        if (currentDrink.IsSame(targetDrink))
        {
            v += 1;
        }
        if (targetMixage == controlsSection.currentMixage)
        {
            v += 1;
        }
        if (!doneClicked)
        {
            v -= 1;
        }
        return v;
    }

    private Tween AnimateOutRectTransform(RectTransform rectTransform)
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
        return rectTransform.DOAnchorPos(target, animationDuration);
    }

    private Tween AnimateInRectTransform(RectTransform rectTransform)
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
        return rectTransform.DOAnchorPos(target, animationDuration);
    }
}
