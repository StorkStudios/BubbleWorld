using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreenController : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI text;

    [SerializeField]
    private GameObject dialogueBox;

    [SerializeField]
    private string mainMenuScene;

    private void Start()
    {
        Director.Instance.ElementReadEvent += OnElementRead;
        gameObject.SetActive(false);
        dialogueBox.SetActive(true);
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
            dialogueBox.SetActive(false);
            GameOver gameOver = element as GameOver;
            text.text = gameOver.text;
        }
    }
}
