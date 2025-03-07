using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class DialogueBox : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI speechText;
    [SerializeField]
    private TextMeshProUGUI characterName;

    [Header("Animation")]
    [SerializeField]
    private float fadeDuration;
    [SerializeField]
    private float textFadeSpeed;
    [SerializeField]
    private float textSequenceSpeed;

    private float FadeSpeed => 1 / fadeDuration;

    private bool Hidden
    {
        get => hidden;
        set
        {
            if (hidden == value)
            {
                return;
            }

            canvasGroup.DOKill();

            float target = value ? 0 : 1;
            canvasGroup.DOFade(target, FadeSpeed).SetSpeedBased();

            if (!value)
            {
                speechText.text = "";
                characterName.text = "";
            }

            hidden = value;
        }
    }

    private bool hidden = false;
    private bool animationCompleted = false;

    private CanvasGroup canvasGroup;

    private Sequence animationSequence = null;
    private Coroutine durationCoroutine = null;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        HideCurrentDialogue();
        Director.Instance.ElementReadEvent += OnElementRead;
    }

    public void ShowDialogue(Speech values)
    {
        if (Hidden)
        {
            return;
        }

        if (durationCoroutine != null)
        {
            StopCoroutine(durationCoroutine);
            durationCoroutine = null;
        }
        animationCompleted = false;

        characterName.text = values.characterName;

        KillTweens();

        for (int i = 0; i < speechText.textInfo.characterCount; i++)
        {
            speechText.textInfo.SetCharacterAlpha(i, 0);
        }
        characterName.alpha = 0;

        
        TMP_TextInfo textInfo = speechText.GetTextInfo(values.voiceline);
        Sequence sequence = textInfo.AnimateTextWordByWord(1 / textFadeSpeed, 1 / textSequenceSpeed);
        sequence = sequence.OnComplete(() => animationCompleted = true);
        if (values.duration.HasValue)
        {
            sequence = sequence.OnComplete(() =>
            {
                durationCoroutine = this.CallDelayed(values.duration.Value, () =>
                {
                    durationCoroutine = null;
                    Skip();
                });
            });
        }
        animationSequence = sequence;
        characterName.DOFade(1, textFadeSpeed).SetSpeedBased();
    }

    public void HideCurrentDialogue()
    {
        KillTweens();

        animationCompleted = false;
        speechText.DOFade(0, textFadeSpeed).SetSpeedBased();
        characterName.DOFade(0, textFadeSpeed).SetSpeedBased().OnComplete(() => animationCompleted = true);
    }

    private void KillTweens()
    {
        animationSequence.Kill();
        characterName.DOKill();
    }

    public void Skip()
    {
        if (!GameManager.Instance.CanSkip || Hidden || PlayerInputAdapter.Instance.SkipHeld)
        {
            return;
        }

        if (animationCompleted)
        {
            if (durationCoroutine != null)
            {
                StopCoroutine(durationCoroutine);
                durationCoroutine = null;
            }

            Director.Instance.DirectorStepEvent?.Invoke("bulech");

            return;
        }

        characterName.DOComplete();
        animationSequence.Complete();
    }

    private void OnElementRead(string element, ChapterElement values)
    {
        switch(element)
        {
            case "Speech":
                ShowDialogue(values as Speech);
                break;
            case "MinigameStart":
                Hidden = true;
                break;
            case "MinigameEnd":
                Hidden = false;
                break;
            case "Options":
                OnOptions();
                break;
        }
    }

    private void OnOptions()
    {
        if (durationCoroutine != null)
        {
            StopCoroutine(durationCoroutine);
            durationCoroutine = null;
        }
    }
}
