using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class BackgroundHandler : MonoBehaviour
{
    [SerializeField]
    private SerializedDictionary<string, Sprite> backgrounds = new SerializedDictionary<string, Sprite>();

    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
        Director.Instance.ElementReadEvent += OnElementRead;
    }

    private void OnElementRead(string element, ChapterElement values)
    {
        switch (element)
        {
            case "Background":
                ChangeBackground((values as Background).Name);
                break;
            case "GameOver":
                ChangeBackground((values as GameOver).background);
                break;
        }
    }

    private void ChangeBackground(string background)
    {
        image.sprite = backgrounds[background];
    }
}
