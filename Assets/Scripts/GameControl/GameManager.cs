using DG.Tweening;
using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private string firstChapterPath;

    [HideInInspector]
    public bool CanSkip = false;

    private Coroutine currentChapter;

    private void Start()
    {
        DOTween.SetTweensCapacity(1250, 50);
        currentChapter = StartCoroutine(StartChapter(firstChapterPath));
    }

    private IEnumerator StartChapter(string chapterPath)
    {
        yield return Director.Instance.LoadScript(chapterPath);
        Director.Instance.ReadScript();
        currentChapter = StartCoroutine(Director.Instance.RunScript());
        CanSkip = true;
    }

    public void ChangeScript(string path)
    {
        if (currentChapter != null)
        {
            StopCoroutine(currentChapter);
        }
        currentChapter = StartCoroutine(StartChapter(path));
    }
}
