using UnityEngine;
using UnityEngine.UI;

public class IngredientsSection : MonoBehaviour
{
    [SerializeField]
    private Button earlGrayButton;
    [SerializeField]
    private Button mintButton;
    [SerializeField]
    private Button matchaButton;
    [SerializeField]
    private Button passionFruitButton;
    [SerializeField]
    private Button strawberryButton;
    [SerializeField]
    private Button milkButton;
    [SerializeField]
    private Button chocolateButton;
    [SerializeField]
    private Button almondButton;
    [SerializeField]
    private Button bananaButton;

    public event System.Action<Button, TeaBase> BaseSelected;
    public event System.Action<Button, TeaSyroup> SyroupSelected;
    public event System.Action<Button, TeaJelly> JellySelected;

    private void Awake()
    {
        earlGrayButton.onClick.AddListener(() => BaseSelected?.Invoke(earlGrayButton, TeaBase.EarlGray));
        mintButton.onClick.AddListener(() => BaseSelected?.Invoke(mintButton, TeaBase.Mint));
        matchaButton.onClick.AddListener(() => BaseSelected?.Invoke(matchaButton, TeaBase.Matcha));
        passionFruitButton.onClick.AddListener(() => SyroupSelected?.Invoke(passionFruitButton, TeaSyroup.PassionFruit));
        strawberryButton.onClick.AddListener(() => SyroupSelected?.Invoke(strawberryButton, TeaSyroup.Strawberry));
        milkButton.onClick.AddListener(() => SyroupSelected?.Invoke(milkButton, TeaSyroup.Milk));
        chocolateButton.onClick.AddListener(() => JellySelected?.Invoke(chocolateButton, TeaJelly.Chocolate));
        almondButton.onClick.AddListener(() => JellySelected?.Invoke(almondButton, TeaJelly.Almond));
        bananaButton.onClick.AddListener(() => JellySelected?.Invoke(bananaButton, TeaJelly.Banana));
    }

    public void SetClickableBase(bool value)
    {
        earlGrayButton.interactable = value;
        mintButton.interactable = value;
        matchaButton.interactable = value;
    }

    public void SetClickableSyroup(bool value)
    {
        passionFruitButton.interactable = value;
        strawberryButton.interactable = value;
        milkButton.interactable = value;
    }

    public void SetClickableJellies(bool value)
    {
        chocolateButton.interactable = value;
        almondButton.interactable = value;
        bananaButton.interactable = value;
    }
}
