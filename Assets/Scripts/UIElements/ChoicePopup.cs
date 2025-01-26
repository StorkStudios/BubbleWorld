using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class ChoicePopup : MonoBehaviour
{
    [SerializeField]
    private ChoiceButton choiceButtonPrefab;
    [SerializeField]
    private float fadeDuration;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private Coroutine currentTimer;
    private HashSet<ChoiceButton> currentChoices = new HashSet<ChoiceButton>();

    private void Awake()
    {
        rectTransform = transform as RectTransform;
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
    }

    private void Start()
    {
        Director.Instance.ElementReadEvent += OnElementRead;
    }

    private void OnElementRead(string element, ChapterElement values)
    {
        switch (element)
        {
            case "Options":
                StartOption(values as Options);
                break;
            case "MinigameStart":
                OnMinigameStart();
                break;
            case "MinigameEnd":
                OnMinigameEnd();
                break;
        }
    }

    private void StartOption(Options values)
    {
        GameManager.Instance.CanSkip = false;

        canvasGroup.DOFade(1, fadeDuration);

        foreach (Option option in values.options)
        {
            ChoiceButton choiceButton = Instantiate(choiceButtonPrefab.gameObject, transform).GetComponent<ChoiceButton>();
            choiceButton.Label = option.optionText;
            choiceButton.Clicked += () => OnOptionSelected(option.id);
            currentChoices.Add(choiceButton);
        }

        if (values.duration.HasValue)
        {
            StartTimer(values.duration.Value, values.defaultId);
        }
    }

    private void StartTimer(float duration, string id)
    {
        currentTimer = this.CallDelayed(duration, () => {
            OnOptionSelected(id);
            currentTimer = null;
        });
    }

    private void OnOptionSelected(string id)
    {
        foreach (ChoiceButton button in currentChoices)
        {
            button.Clickable = false;
        }
        canvasGroup.DOFade(0, fadeDuration).OnComplete(() =>
        {
            foreach (ChoiceButton button in currentChoices)
            {
                Destroy(button.gameObject);
            }
            currentChoices.Clear();
        });

        if (currentTimer != null)
        {
            StopCoroutine(currentTimer);
            currentTimer = null;
        }

        Director.Instance.DirectorStepEvent?.Invoke(id);
        GameManager.Instance.CanSkip = true;
    }

    private void OnMinigameStart()
    {
        rectTransform.DOSizeDelta(new Vector2(-450, rectTransform.sizeDelta.y - 100), fadeDuration);
        rectTransform.DOAnchorPos(new Vector2(-450 / 2, rectTransform.anchoredPosition.y + 100 / 2), fadeDuration);
    }

    private void OnMinigameEnd()
    {
        rectTransform.DOSizeDelta(new Vector2(0, rectTransform.sizeDelta.y + 100), fadeDuration);
        rectTransform.DOAnchorPos(new Vector2(-450 / 2, rectTransform.anchoredPosition.y - 100 / 2), fadeDuration);
    }
}
