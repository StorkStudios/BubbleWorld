using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable Objects/CharacterData")]
public class CharacterData : ScriptableObject
{
    public enum EyesPose
    {
        Forward,
        Down,
        Closed,
        Angry
    }

    public enum MouthPose
    {
        Neutral,
        Smile,
        Sad,
        Angry
    }

    [SerializeField]
    private Sprite baseSprite;
    public Sprite BaseSprite => baseSprite;

    [SerializeField]
    private SerializedDictionary<EyesPose, Sprite> eyes;
    public SerializedDictionary<EyesPose, Sprite> Eyes => eyes;

    [SerializeField]
    private SerializedDictionary<MouthPose, Sprite> mouth;
    public SerializedDictionary<MouthPose, Sprite> Mouth => mouth;

    [SerializeField]
    private Vector2 size;
    public Vector2 Size => size;

}
