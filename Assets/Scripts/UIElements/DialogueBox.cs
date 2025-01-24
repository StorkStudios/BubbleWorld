using DG.Tweening;
using System.Collections;
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

    private float FadeSpeed => 1 / fadeDuration;

    public bool Hidden
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

            hidden = value;
        }
    }

    private bool hidden = false;

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
}
