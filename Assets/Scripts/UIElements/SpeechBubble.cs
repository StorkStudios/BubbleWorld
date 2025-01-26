using DG.Tweening;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class SpeechBubble : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI content;
    [SerializeField]
    private float textFadeSpeed;
    [SerializeField]
    private float textSequenceSpeed;
    [SerializeField]
    private float fadeDuration;

    public event System.Action Finished;

    public bool Hidden { get; private set; } = true;

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
    }

    public void Show(string text, float duration)
    {
        if (Hidden)
        {
            Hidden = false;
            content.alpha = 0;
            content.text = "";
            canvasGroup.DOFade(1, fadeDuration).OnComplete(() =>
            {
                ShowContent(text, duration);
            });
        }
        else
        {
            ShowContent(text, duration);
        }
    }

    public void Hide()
    {
        if (!Hidden)
        {
            Hidden = true;
            canvasGroup.DOFade(0, fadeDuration);
        }
    }

    private void ShowContent(string text, float duration)
    {
        content.alpha = 0;
        content.text = "";

        TMP_TextInfo textInfo = content.GetTextInfo(text);

        int k = 0;
        for (int i = 0; i < textInfo.characterCount; i++)
        {
            if (char.IsWhiteSpace(text[i]))
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
                tween = tween.OnComplete(() => this.CallDelayed(duration, () => Finished?.Invoke()));
            }
        }
    }
}
