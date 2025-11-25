using UnityEngine;

public class CardBoard : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private UIManager uiManager;

    [Header("Card Properties")]
    [SerializeField] private CardData[] cardData;
    [SerializeField] private Card cardPrefab;
    private RectTransform cardRectTransform;

    [Header("Card View Properties")]
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private float horizontalSpacing;
    [SerializeField] private float verticalSpacing;

    private void Awake()
    {
        cardRectTransform = cardPrefab.GetComponent<RectTransform>();
    }

    public void GenerateCards(int row, int column)
    {
        Vector2 cardSizeDelta = cardRectTransform.sizeDelta;
        Vector2 halfCardSizeDelta = cardSizeDelta * .5f;

        float horizontalSpacingTotal = horizontalSpacing * (column - 1);
        float verticalSpacingTotal = verticalSpacing * (row - 1);

        float cardsSizeDeltaXTotal = column * cardSizeDelta.x;
        float cardsSizeDeltaYTotal = row * cardSizeDelta.y;

        Vector2 cardPanelSizeDelta = new Vector2(cardsSizeDeltaXTotal + horizontalSpacingTotal, cardsSizeDeltaYTotal + verticalSpacingTotal);
        Vector2 halfCardPanelSizeDelta = cardPanelSizeDelta * .5f;

        //Starts spawning from top left
        Vector2 spawnStartPoint = rectTransform.anchoredPosition +
            new Vector2(-halfCardPanelSizeDelta.x + halfCardSizeDelta.x, halfCardPanelSizeDelta.y - halfCardSizeDelta.y);

        Vector2 currentSpawnPosition = spawnStartPoint;

        //Generates from top left
        for (int i = 0; i < row * column; i++)
        {
            if (i > 0 && i % row == 0)
            {
                currentSpawnPosition.x = spawnStartPoint.x;
                currentSpawnPosition.y -= (cardSizeDelta.y + verticalSpacing);
            }

            Card card = Instantiate(cardPrefab, rectTransform);
            card.GetComponent<RectTransform>().anchoredPosition = currentSpawnPosition;
            currentSpawnPosition.x += cardSizeDelta.x + horizontalSpacing;
        }

    }
}
