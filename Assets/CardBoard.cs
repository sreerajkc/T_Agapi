using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardBoard : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private UIManager uiManager;

    [Header("Card Properties")]
    [SerializeField] private List<CardData> cardsData;
    [SerializeField] private Card cardPrefab;
    private RectTransform cardRectTransform;

    [Header("Card View Properties")]
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private float horizontalSpacing;
    [SerializeField] private float verticalSpacing;

    [SerializeField] private int maxRowsAtDefaultSize = 3;
    [SerializeField] private int maxColumnsAtDefaultSize = 5;

    private void Awake()
    {
        cardRectTransform = cardPrefab.GetComponent<RectTransform>();
    }

    public void GenerateCards(int rows, int columns)
    {
        int count = rows * columns;

        // With default card sizeDelta, screen can perfectly fit 3 rows. Need to scale when column changes
        float sizeMultiplierX = (float)maxColumnsAtDefaultSize / columns;

        // With default card sizeDelta, screen can perfectly fit 3 rows. Need to scale when row changes
        float sizeMultiplierY = (float)maxRowsAtDefaultSize / rows;

        //Taking the min to avoid cutting. if the multiplier sum is more than two then the scale always get scaled to 1;
        float sizeMultiplier = Mathf.Min(1f, sizeMultiplierX, sizeMultiplierY);

        Vector2 cardSizeDelta = cardRectTransform.sizeDelta * sizeMultiplier;
        Vector2 halfCardSizeDelta = cardSizeDelta * .5f;

        float horizontalSpacingTotal = horizontalSpacing * (columns - 1) * sizeMultiplier;
        float verticalSpacingTotal = verticalSpacing * (rows - 1) * sizeMultiplier;

        float cardsSizeDeltaXTotal = columns * cardSizeDelta.x;
        float cardsSizeDeltaYTotal = rows * cardSizeDelta.y;

        Vector2 cardPanelSizeDelta = new Vector2(cardsSizeDeltaXTotal + horizontalSpacingTotal, cardsSizeDeltaYTotal + verticalSpacingTotal);
        Vector2 halfCardPanelSizeDelta = cardPanelSizeDelta * .5f;

        //Starts spawning from top left
        Vector2 spawnStartPoint = rectTransform.anchoredPosition +
            new Vector2(-halfCardPanelSizeDelta.x + halfCardSizeDelta.x, halfCardPanelSizeDelta.y - halfCardSizeDelta.y);

        Vector2 currentSpawnPosition = spawnStartPoint;

        List<CardData> shuffledDuplicatedCardDataList = cardsData.Shuffle().RandomDuplicate(count);

        //Generates from top left
        for (int i = 0; i < count; i++)
        {
            if (i > 0 && i % columns == 0)
            {
                currentSpawnPosition.x = spawnStartPoint.x;
                currentSpawnPosition.y -= (cardSizeDelta.y + (verticalSpacing * sizeMultiplier));
            }

            Card card = Instantiate(cardPrefab, rectTransform);
            CardData randomCardData = shuffledDuplicatedCardDataList[i];
            card.Initialize(randomCardData.Id);

            CardUIManager cardUIManager = card.GetComponent<CardUIManager>();
            cardUIManager.SetCardIcon(randomCardData.Icon);

            RectTransform cardRectTransform = card.GetComponent<RectTransform>();
            cardRectTransform.sizeDelta = cardSizeDelta;
            cardRectTransform.anchoredPosition = currentSpawnPosition;

            currentSpawnPosition.x += cardSizeDelta.x + (horizontalSpacing * sizeMultiplier);
        }
    }

}
