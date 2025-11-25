using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Card UI Data")]
    [SerializeField] private CardData[] cardDatas;

    [Header("Card UI Elements")]
    [SerializeField] private RectTransform cardBoardRectTransform;

    [Header("Core UI Elements")]
    [SerializeField] private Canvas canvas;

    private void OnEnable()
    {
        Card.OnInitialized += OnCardInitialized;
    }

    private void OnDisable()
    {
        Card.OnInitialized -= OnCardInitialized;
    }

    private void OnCardInitialized(Card card)
    {
        for (int i = 0; i < cardDatas.Length; i++)
        {
            CardData cardData = cardDatas[i];

            if (cardData.Id == card.Id)
            {
                CardUIManager cardUIManager = card.GetComponent<CardUIManager>();
                cardUIManager.SetCardIcon(cardData.Icon);
                cardUIManager.GetComponent<RectTransform>().parent = cardBoardRectTransform;
            }
        }
    }
}
