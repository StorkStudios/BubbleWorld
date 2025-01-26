using UnityEngine;

[CreateAssetMenu(fileName = "TeaIconMap", menuName = "Scriptable Objects/TeaIconMap")]
public class TeaIconMap : ScriptableObject
{
    [SerializeField]
    private SerializedDictionary<TeaBase, Sprite> teaBases;
    public SerializedDictionary<TeaBase, Sprite> TeaBases => teaBases;

    [SerializeField]
    private SerializedDictionary<TeaSyroup, Sprite> teaSyroups;
    public SerializedDictionary<TeaSyroup, Sprite> TeaSyroups => teaSyroups;

    [SerializeField]
    private SerializedDictionary<TeaJelly, Sprite> teaJellies;
    public SerializedDictionary<TeaJelly, Sprite> TeaJellies => teaJellies;
}
