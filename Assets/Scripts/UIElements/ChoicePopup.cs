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

    private CanvasGroup canvasGroup;

    private Coroutine currentTimer;
    private HashSet<ChoiceButton> currentChoices = new HashSet<ChoiceButton>();

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
    }

    private void Start()
    {
        Director.Instance.ElementReadEvent += OnElementRead;
    }

    private void OnElementRead(string element, ChapterElement values)
    {
        if (element != "Options")
        {
            return;
        }

        canvasGroup.DOFade(1, fadeDuration);

        Options options = values as Options;

        foreach(Option option in options.options)
        {
            ChoiceButton choiceButton = Instantiate(choiceButtonPrefab.gameObject, transform).GetComponent<ChoiceButton>();
            choiceButton.Label = option.optionText;
            choiceButton.Clicked += () => OnOptionSelected(option.id);
            currentChoices.Add(choiceButton);
        }

        if (options.duration.HasValue)
        {
            StartTimer(options.duration.Value, options.defaultId);
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
    }
}
