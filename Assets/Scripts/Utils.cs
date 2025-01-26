using TMPro;
using UnityEngine;

public static class Utils
{
    public static void SetCharacterAlpha(this TMP_TextInfo textInfo, int charIndex, float alpha)
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

        textInfo.textComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
    }

    public static float GetCharacterAlpha(this TMP_TextInfo textInfo, int charIndex)
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
