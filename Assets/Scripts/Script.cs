using UnityEngine;

[CreateAssetMenu(fileName = "Script", menuName = "Scriptable Objects/Script")]
public class Script : ScriptableObject
{
    [SerializeField]
    private string path;
    public string Path => System.IO.Path.Combine(Application.streamingAssetsPath, path);
}
