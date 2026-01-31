using UnityEngine;
using UnityEngine.UI;

public class UIMaskHandler : MonoBehaviour
{
    public Sprite onSprite;
    public Sprite offSprite;
    public bool isOn = false;

    private Image imageComponent;

    void Awake()
    {
        imageComponent = GetComponent<Image>();
        if (imageComponent == null)
        {
            Debug.LogError("UIMaskHandler requires an Image component on the same GameObject.");
        }
        UpdateImage();
    }

    public void SetState(bool state)
    {
        isOn = state;
        UpdateImage();
    }

    public void ToggleState()
    {
        isOn = !isOn;
        UpdateImage();
    }

    private void UpdateImage()
    {
        if (imageComponent != null)
        {
            imageComponent.sprite = isOn ? onSprite : offSprite;
        }
    }
}
