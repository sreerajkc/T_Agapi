using JetBrains.Annotations;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Card Properties")]
    [SerializeField] private CardData[] cardDatas;

    [Header("Card Board Properties")]
    [SerializeField] private CardBoard cardBoard;
    [SerializeField] private int rows;
    [SerializeField] private int columns;

    private int? firstFlippedCardId = null;
    private int? secondFlippedCardId = null;

    private bool isPairMatching = false;

    private void OnEnable()
    {
        Card.OnFlipped += OnCardFlipped;
    }

    private void OnDisable()
    {
        Card.OnFlipped -= OnCardFlipped;
    }

    private void Start()
    {
        cardBoard.GenerateCards(rows, columns);
    }

    private void OnCardFlipped(Card flippedCard)
    {
        int flippedCardId = flippedCard.Id;

        if (firstFlippedCardId == null)
        {
            firstFlippedCardId = flippedCardId;
        }
        else if (secondFlippedCardId ==null)
        {
            secondFlippedCardId = flippedCardId;

            if (firstFlippedCardId == secondFlippedCardId)
            {
                isPairMatching = true;
            }
            else
            {
                isPairMatching = false;
            }

            firstFlippedCardId = null;
            secondFlippedCardId = null;
        }
    }

}
