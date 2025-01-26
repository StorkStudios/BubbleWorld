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

    private readonly HashSet<Tween> sequenceTweens = new HashSet<Tween>();
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
        int k = values.characterName.Length > 0 ? 1 : 0;
        for (int i = 0; i < textInfo.characterCount; i++)
        {
            if (char.IsWhiteSpace(speechText.text[i]))
            {
                k++;
            }

            int j = i;

            Tween tween = DOTween.To(
                () => textInfo.GetCharacterAlpha(j),
                x => textInfo.SetCharacterAlpha(j, x),
                1,
                textFadeSpeed)
                .SetDelay(k / textSequenceSpeed)
                .SetSpeedBased();
            
            if (i == textInfo.characterCount - 1)
            {
                tween = tween.OnComplete(() => animationCompleted = true);
                if (values.duration.HasValue)
                {
                    tween = tween.OnComplete(() =>
                    {
                        durationCoroutine = this.CallDelayed(values.duration.Value, () =>
                        {
                            durationCoroutine = null;
                            Skip();
                        });
                    });
                }
            }

            sequenceTweens.Add(tween);
        }
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
        foreach (Tween tween in sequenceTweens)
        {
            tween.Kill();
        }
        sequenceTweens.Clear();
        characterName.DOKill();
    }

    public void Skip()
    {
        if (!GameManager.Instance.CanSkip || Hidden)
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
        foreach (Tween tween in sequenceTweens)
        {
            tween.Complete();
        }
        sequenceTweens.Clear();
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
        }
    }
}
