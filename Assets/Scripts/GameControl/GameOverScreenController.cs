using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreenController : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI text;

    [SerializeField]
    private string mainMenuScene;

    private void Start()
    {
        Director.Instance.ElementReadEvent += OnElementRead;
        gameObject.SetActive(false);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
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
