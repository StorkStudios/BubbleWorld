using DG.Tweening;
using System.Collections.Generic;
using System.Reflection;
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
    [SerializeField]
    private SerializedDictionary<string, SpeechBubble> speechBubbles;
    [SerializeField]
    private RectTransform portraitParent;

    private readonly Dictionary<string, CharacterPortrait> characterPortraits = new Dictionary<string, CharacterPortrait>();

    private RectTransform rectTransform;

    private bool isMinigame = false;

    private void Awake()
    {
        rectTransform = transform as RectTransform;
    }

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
            case "Speech":
                OnSpeech(values as Speech);
                break;
            case "MinigameStart":
                OnMinigameStart();
                break;
            case "MinigameEnd":
                OnMinigameEnd();
                break;
        }
    }

    private void SpawnCharacter(Enter values)
    {
        CharacterData characterData = characters[values.characterId];

        CharacterPortrait portrait = Instantiate(characterPortraitPrefab.gameObject, portraitParent).GetComponent<CharacterPortrait>();
        portrait.position = values.position;
        portrait.Base = characterData.BaseSprite;
        portrait.Eyes = characterData.Eyes[values.eyes];
        portrait.Mouth = characterData.Mouth[values.mouth];
        portrait.rectTransform.sizeDelta = characterData.Size;

        portrait.CanvasGroup.alpha = 0;
        portrait.CanvasGroup.DOFade(1, values.duration ?? fadeDuration);

        characterPortraits[values.characterId] = portrait;
        speechBubbles[values.characterId] = portrait.GetComponentInChildren<SpeechBubble>();
    }

    private void DespawnCharacter(Exit values)
    {
        GameObject characterToDestroy = characterPortraits[values.characterId].gameObject;
        characterPortraits[values.characterId].CanvasGroup.DOFade(0, values.duration ?? fadeDuration).OnComplete(() => Destroy(characterToDestroy));
        characterPortraits.Remove(values.characterId);
        speechBubbles.Remove(values.characterId);
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

    private void OnSpeech(Speech values)
    {
        if (!isMinigame)
        {
            return;
        }

        if (PlayerInputAdapter.Instance.SkipHeld)
        {
            Director.Instance.DirectorStepEvent?.Invoke("bulech");
            return;
        }

        string charId = values.characterId ?? "Gracz";

        foreach (KeyValuePair<NullableObject<string>, SpeechBubble> pair in speechBubbles)
        {
            if (pair.Key.Item == charId)
            {
                var bubble = pair.Value;
                
                void OnSpeechBubbleFinished()
                {
                    Director.Instance.DirectorStepEvent?.Invoke("bulech");
                    bubble.Finished -= OnSpeechBubbleFinished;
                }

                bubble.Finished += OnSpeechBubbleFinished;
                bubble.Show(values.voiceline, values.duration ?? 1);
            }
            else
            {
                pair.Value.Hide();
            }
        }
    }

    private void OnMinigameStart()
    {
        isMinigame = true;

        rectTransform.DOSizeDelta(new Vector2(-450, rectTransform.sizeDelta.y - 12), fadeDuration);
        rectTransform.DOAnchorPos(new Vector2(-450 / 2, rectTransform.anchoredPosition.y + 12 / 2), fadeDuration);
    }

    private void OnMinigameEnd()
    {
        isMinigame = false;

        rectTransform.DOSizeDelta(new Vector2(0, rectTransform.sizeDelta.y + 12), fadeDuration);
        rectTransform.DOAnchorPos(new Vector2(-450 / 2, rectTransform.anchoredPosition.y - 12 / 2), fadeDuration);

        foreach (SpeechBubble speechBubble in speechBubbles.Values)
        {
            speechBubble.Hide();
        }
    }
}
