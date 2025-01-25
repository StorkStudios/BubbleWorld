using DG.Tweening;
using UnityEngine;

public class CharacterPortraitsSection : MonoBehaviour
{
    [SerializeField]
    private CharacterPortrait characterPortraitPrefab;
    [SerializeField]
    private float fadeDuration;
    [SerializeField]
    private SerializedDictionary<string, CharacterData> characters;

    private SerializedDictionary<string, CharacterPortrait> characterPortraits = new SerializedDictionary<string, CharacterPortrait>();

    private void Start()
    {
        Director.Instance.ElementReadEvent += OnElementRead;
    }

    private void OnElementRead(string element, ChapterElement values)
    {
        switch (element)
        {
            case "Enter":
                SpawnCharacter(values as Enter);
                break;
        }
    }

    private void SpawnCharacter(Enter values)
    {
        CharacterData characterData = characters[values.characterId];

        CharacterPortrait characterPortrait = Instantiate(characterPortraitPrefab.gameObject, transform).GetComponent<CharacterPortrait>();
        characterPortrait.position = values.position;
        characterPortrait.Base = characterData.BaseSprite;
        characterPortrait.Eyes = characterData.Eyes[values.eyes];
        characterPortrait.Mouth = characterData.Mouth[values.mouth];

        characterPortrait.CanvasGroup.alpha = 0;
        characterPortrait.CanvasGroup.DOFade(1, fadeDuration);
    }
}
