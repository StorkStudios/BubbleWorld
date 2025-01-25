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
    private float moveSpeed;
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
            case "Update":
                UpdateCharacter(values as Update);
                break;

        }
    }

    private void SpawnCharacter(Enter values)
    {
        CharacterData characterData = characters[values.characterId];

        CharacterPortrait portrait = Instantiate(characterPortraitPrefab.gameObject, transform).GetComponent<CharacterPortrait>();
        portrait.position = values.position;
        portrait.Base = characterData.BaseSprite;
        portrait.Eyes = characterData.Eyes[values.eyes];
        portrait.Mouth = characterData.Mouth[values.mouth];

        portrait.CanvasGroup.alpha = 0;
        portrait.CanvasGroup.DOFade(1, fadeDuration);

        characterPortraits[values.characterId] = portrait;
    }

    private void DespawnCharacter(Exit values)
    {
        GameObject characterToDestroy = characterPortraits[values.characterId].gameObject;
        characterPortraits[values.characterId].CanvasGroup.DOFade(0, fadeDuration).OnComplete(() => Destroy(characterToDestroy));
        characterPortraits.Remove(values.characterId);
    }

    private void UpdateCharacter(Update values)
    {
        CharacterPortrait portrait = characterPortraits[values.characterId];
        CharacterData characterData = characters[values.characterId];

        if (values.position.HasValue)
        {
            DOTween.To(() => portrait.position, x => portrait.position = x, values.position.Value, moveSpeed).SetSpeedBased();
        }

        if (values.eyes.HasValue)
        {
            portrait.Eyes = characterData.Eyes[values.eyes.Value];
        }
        if (values.mouth.HasValue)
        {
            portrait.Mouth = characterData.Mouth[values.mouth.Value];
        }
    }
}
