using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsSection : MonoBehaviour
{
    [SerializeField]
    private Button doneButton;
    [SerializeField]
    private Button resetButton;
    [SerializeField]
    private HoldButton mixButton;
    [SerializeField]
    private List<Image> images;
    [SerializeField]
    private Color baseColor;
    [SerializeField]
    private List<Color> colors = new List<Color>();
    [SerializeField]
    private RangeBoundariesFloat range;
    [SerializeField]
    private float animationSpeed;

    public event System.Action Done;
    public event System.Action Reset;
    public event System.Action<bool> MixButtonStateChanged;

    public TeaMixage? currentMixage = null;

    private void Awake()
    {
        doneButton.onClick.AddListener(OnDone);
        resetButton.onClick.AddListener(OnReset);
        mixButton.buttonDownUpdate.AddListener(OnButtonDownUpdate);
        mixButton.onClick.AddListener(OnMix);
        mixButton.buttonDown.AddListener(OnMixButtonDown);

        foreach (Image image in images)
        {
            image.color = baseColor;
        }
    }

    private void OnDone()
    {
        Done?.Invoke();
    }

    private void OnReset()
    {
        Reset?.Invoke();
    }

    private void OnMix()
    {
        mixButton.interactable = false;
        MixButtonStateChanged?.Invoke(false);
    }

    public void OnMixButtonDown()
    {
        MixButtonStateChanged?.Invoke(true);
    }

    public void OnButtonDownUpdate(float timeElapsed)
    {
        if (timeElapsed == 0)
        {
            foreach (Image image in images)
            {
                image.DOColor(baseColor, animationSpeed).SetSpeedBased();
            }
            return;
        }
        if (0 < timeElapsed && !currentMixage.HasValue)
        {
            images[0].DOKill();
            images[0].DOColor(colors[0], animationSpeed).SetSpeedBased();
            currentMixage = TeaMixage.Light;
        }
        if (range.Min < timeElapsed && currentMixage == TeaMixage.Light)
        {
            images[1].DOKill();
            images[1].DOColor(colors[1], animationSpeed).SetSpeedBased();
            currentMixage = TeaMixage.Medium;
        }
        if (range.Max < timeElapsed && currentMixage == TeaMixage.Medium)
        {
            images[2].DOKill();
            images[2].DOColor(colors[2], animationSpeed).SetSpeedBased();
            currentMixage = TeaMixage.Hard;
        }
    }

    public void SetClickable(bool value)
    {
        doneButton.interactable = value;
        resetButton.interactable = value;
        mixButton.interactable = value;
    }
}
