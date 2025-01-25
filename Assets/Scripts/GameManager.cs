using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Script script;

    private IEnumerator Start()
    {
        yield return Director.Instance.LoadScript(script);
        Director.Instance.ReadScript();
        yield return Director.Instance.RunScript();
    }
}
