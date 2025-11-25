using System;
using UnityEngine;
using UnityEngine.UI;

public class CardUIManager : MonoBehaviour
{
    [Header("Card Properties")]
    [SerializeField] private Card card;

    [Header("UI Elements")]
    [SerializeField] private Button button;
    [SerializeField] private Image icon;

    private void Awake()
    {
        button.onClick.AddListener(OnClickButton);
    }

    private void OnClickButton()
    {
        card.Flip();
    }
    
    public void SetCardIcon(Sprite sprite)
    {
        icon.sprite = sprite;
    }
}
