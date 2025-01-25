using System;
using UnityEngine;

public class GameOverScreenController : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI text;

    private void Start()
    {
        Director.Instance.ElementReadEvent += OnElementRead;
        gameObject.SetActive(false);
    }

    private void OnElementRead(string name, ChapterElement element)
    {
        if (name == "GameOver")
        {
            gameObject.SetActive(true);
            GameOver gameOver = element as GameOver;
            text.text = gameOver.text;
        }
    }
}
