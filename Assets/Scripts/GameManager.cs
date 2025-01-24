using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Script script;

    private bool scriptExecuted = false;

    private void Start()
    {
        StartCoroutine(Director.Instance.LoadScript(script));
    }

    private void Update()
    {
        if (Director.Instance.ScriptLoaded && !scriptExecuted)
        {
            scriptExecuted = true;
            Director.Instance.ReadScript();
            Director.Instance.RunScript();
        }
    }
}
