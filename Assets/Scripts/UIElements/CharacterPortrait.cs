using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class CharacterPortrait : MonoBehaviour
{
    public float position;

    private RectTransform rectTransform;
    
    private void Awake()
    {
        rectTransform = transform as RectTransform;
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
