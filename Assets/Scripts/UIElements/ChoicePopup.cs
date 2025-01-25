using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class ChoicePopup : MonoBehaviour
{
    private CanvasGroup canvasGroup;

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
    }
}
