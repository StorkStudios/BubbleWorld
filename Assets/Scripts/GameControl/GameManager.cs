using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private string scriptPath;

    [HideInInspector]
    public bool CanSkip = false;

    private IEnumerator Start()
    {
        yield return Director.Instance.LoadScript(scriptPath);
        Director.Instance.ReadScript();
        StartCoroutine(Director.Instance.RunScript());
        CanSkip = true;
    }
}
