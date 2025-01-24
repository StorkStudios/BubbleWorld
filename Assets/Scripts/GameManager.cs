using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Script script;

    void Start()
    {
        StartCoroutine(Director.Instance.LoadScript(script));
    }
}
