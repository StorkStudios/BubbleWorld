using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ChoiceButton : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI label;

    public event System.Action Clicked;

    public string Label
    {
        get => label.text;
        set => label.text = value;
    }

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => Clicked?.Invoke());
    }
}
