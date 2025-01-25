using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPortraitsSection : MonoBehaviour
{
    [SerializeField]
    private CharacterPortrait characterPortraitPrefab;
    [SerializeField]
    private float fadeDuration;
    [SerializeField]
    private SerializedDictionary<string, CharacterData> characters;

    private readonly Dictionary<string, CharacterPortrait> characterPortraits = new Dictionary<string, CharacterPortrait>();

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
            case "Exit":
                DespawnCharacter(values as Exit);
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

        characterPortraits[values.characterId] = characterPortrait;
    }

    private void DespawnCharacter(Exit values)
    {
        GameObject characterToDestroy = characterPortraits[values.characterId].gameObject;
        characterPortraits[values.characterId].CanvasGroup.DOFade(0, fadeDuration).OnComplete(() => Destroy(characterToDestroy));
        characterPortraits.Remove(values.characterId);
    }
}
