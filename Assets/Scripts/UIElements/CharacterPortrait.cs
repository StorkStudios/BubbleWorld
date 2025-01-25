using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class CharacterPortrait : MonoBehaviour
{
    [SerializeField]
    private Image baseImage;
    [SerializeField]
    private Image eyesImage;
    [SerializeField]
    private Image mouthImage;

    public Sprite Base
    {
        get => baseImage.sprite;
        set => baseImage.sprite = value;
    }

    public Sprite Eyes
    {
        get => eyesImage.sprite;
        set => eyesImage.sprite = value;
    }

    public Sprite Mouth
    {
        get => mouthImage.sprite;
        set => mouthImage.sprite = value;
    }

    public CanvasGroup CanvasGroup => canvasGroup;

    public float position;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    
    private void Awake()
    {
        rectTransform = transform as RectTransform;
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        UpdatePosition();
    }

    private void Update()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        float parentWidth = (rectTransform.parent as RectTransform).sizeDelta.x;
        float thisWidth = rectTransform.sizeDelta.x;
        float newX = Mathf.LerpUnclamped(thisWidth / 2, parentWidth - thisWidth / 2, position);
        Vector2 pos = rectTransform.anchoredPosition;
        pos.x = newX;
        rectTransform.anchoredPosition = pos;
    }
}
