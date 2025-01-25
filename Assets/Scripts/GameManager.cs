using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private ChapterData script;

    [HideInInspector]
    public bool CanSkip = false;

    private IEnumerator Start()
    {
        yield return Director.Instance.LoadScript(script);
        Director.Instance.ReadScript();
        yield return Director.Instance.RunScript();
        CanSkip = true;
    }
}
