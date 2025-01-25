using UnityEngine;

public class StoryVariablesManager : Singleton<StoryVariablesManager>
{
    [SerializeField]
    [ReadOnly]
    private SerializedDictionary<string, int> variables = new SerializedDictionary<string, int>();
    public SerializedDictionary<string, int> Variables => variables;
}
