using DG.Tweening;
using System.Collections;
using System.Globalization;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class DialogueBox : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI speechText;
    [SerializeField]
    private TextMeshProUGUI characterName;

    [Header("Animation")]
    [SerializeField]
    private float fadeDuration;
    [SerializeField]
    private float textFadeSpeed;
    [SerializeField]
    private float textSequenceSpeed;

    private float FadeSpeed => 1 / fadeDuration;

    public bool Hidden
    {
        get => hidden;
        set
        {
            if (hidden == value)
            {
                return;
            }

            canvasGroup.DOKill();

            float target = value ? 0 : 1;
            canvasGroup.DOFade(target, FadeSpeed).SetSpeedBased();

            hidden = value;
        }
    }

    private bool hidden = false;

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private IEnumerator Start()
    {
        HideCurrentDialogue();
        yield return new WaitForSeconds(1);
        ShowDialogue(new DialogueValues("bulech", "Lorem ipsum dolor sit amen bulech bulech bulech bulech"));
    }

    private void Update()
    {
        speechText.UpdateVertexData();
    }

    public void ShowDialogue(DialogueValues values)
    {
        characterName.text = values.characterName;

        speechText.DOKill();
        characterName.DOKill();

        speechText.alpha = 0;
        characterName.alpha = 0;

        TMP_TextInfo textInfo = speechText.GetTextInfo(values.voiceline);
        int k = 1;
        for (int i = 0; i < textInfo.characterCount; i++)
        {
            if (char.IsWhiteSpace(speechText.text[i]))
            {
                k++;
            }

            int j = i;

            Tween tween = DOTween.To(
                () => GetCharacterAlpha(textInfo, j),
                x => SetCharacterAlpha(textInfo, j, x),
                1,
                textFadeSpeed)
                .SetDelay(k / textSequenceSpeed)
                .SetSpeedBased();
        }
        characterName.DOFade(1, textFadeSpeed).SetSpeedBased();

    }

    public void HideCurrentDialogue()
    {
        speechText.DOKill();
        characterName.DOKill();

        speechText.DOFade(0, textFadeSpeed).SetSpeedBased();
        characterName.DOFade(0, textFadeSpeed).SetSpeedBased();
    }

    private void SetCharacterAlpha(TMP_TextInfo textInfo, int charIndex, float alpha)
    {
        if (!textInfo.characterInfo[charIndex].isVisible)
        {
            return;
        }

        int materialIndex = textInfo.characterInfo[charIndex].materialReferenceIndex;
        Color32[] vertexColors = textInfo.meshInfo[materialIndex].colors32;
        int vertexIndex = textInfo.characterInfo[charIndex].vertexIndex;

        byte a = (byte)Mathf.Clamp((int)(alpha * 255), 0, 255);

        vertexColors[vertexIndex + 0].a = a;
        vertexColors[vertexIndex + 1].a = a;
        vertexColors[vertexIndex + 2].a = a;
        vertexColors[vertexIndex + 3].a = a;
    }

    private float GetCharacterAlpha(TMP_TextInfo textInfo, int charIndex)
    {
        if (!textInfo.characterInfo[charIndex].isVisible)
        {
            return 0;
        }
        int materialIndex = textInfo.characterInfo[charIndex].materialReferenceIndex;
        Color32[] vertexColors = textInfo.meshInfo[materialIndex].colors32;
        int vertexIndex = textInfo.characterInfo[charIndex].vertexIndex;
        return vertexColors[vertexIndex].a;
    }
}
