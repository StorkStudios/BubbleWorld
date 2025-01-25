using UnityEngine;

[CreateAssetMenu(fileName = "ChapterData", menuName = "Scriptable Objects/Chapter data")]
public class ChapterData : ScriptableObject
{
    [SerializeField]
    private string path;
    public string Path => System.IO.Path.Combine(Application.streamingAssetsPath, path);
}
