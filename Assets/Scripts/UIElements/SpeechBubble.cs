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

    public event System.Action<SpeechBubble> Finished;

    public bool Hidden { get; private set; } = true;

    private CanvasGroup canvasGroup;

    private Sequence sequence;

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
            sequence?.Kill();
            canvasGroup.DOKill();
            canvasGroup.DOFade(0, fadeDuration);
        }
    }

    private void ShowContent(string text, float duration)
    {
        content.alpha = 0;
        content.text = "";

        TMP_TextInfo textInfo = content.GetTextInfo(text);
        content.ForceMeshUpdate();
        sequence = textInfo.AnimateTextWordByWord(1 / textFadeSpeed, 1 / textSequenceSpeed);
        sequence.OnComplete(() => this.CallDelayed(duration, () => Finished?.Invoke(this)));
    }
}
