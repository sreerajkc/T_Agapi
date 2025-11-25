using JetBrains.Annotations;
using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Level Properties")]
    [SerializeField] private LevelData[] leveDatas;
    private int currentLevel;

    [Header("Card Properties")]
    private Card[] generatedCards;
    private int totalPairToMatch;
    private Card firstFlippedCard = null;
    private Card secondFlippedCard = null;

    [Header("Card Board Properties")]
    [SerializeField] private CardBoard cardBoard;

    [Header("Game Properties")]
    private int score;
    private int combo;

    public int Score => score;
    public int Combo => combo;
    public int CurrentLevel => currentLevel;

    public static event Action<Card, Card> OnPairMatched;
    public static event Action<Card, Card> OnPairUnmatched;
    public static event Action OnLevelInitialized;
    public static event Action OnWin;

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
        InitializeLevel();
    }

    private void InitializeLevel()
    {
        currentLevel = Data.GetCurrentLevel();
        LevelData levelData = leveDatas[currentLevel];

        generatedCards = cardBoard.GenerateCards(levelData.Rows, levelData.Columns);

        int totalCount = levelData.Rows * levelData.Columns;
        StartCoroutine(UnflipAllCardsRoutine(totalCount * .1f));
        totalPairToMatch = totalCount / 2;

        OnLevelInitialized?.Invoke();
    }

    private void OnCardFlipped(Card flippedCard)
    {
        if (firstFlippedCard == null)
        {
            firstFlippedCard = flippedCard;
        }
        else if (secondFlippedCard == null)
        {
            secondFlippedCard = flippedCard;

            if (firstFlippedCard.Id == secondFlippedCard.Id)
            {
                MatchPair(firstFlippedCard, secondFlippedCard);
            }
            else
            {
                UnmatchPairs(firstFlippedCard, secondFlippedCard);
            }

            firstFlippedCard = null;
            secondFlippedCard = null;
        }
    }

    private void UnmatchPairs(Card firstCard, Card SecondCard)
    {
        combo = 0;
        OnPairUnmatched?.Invoke(firstCard, SecondCard);

        StartCoroutine(UnmatchPairRoutine(firstCard, SecondCard));
    }

    public IEnumerator UnmatchPairRoutine(Card firstCard, Card SecondCard)
    {
        yield return new WaitForSeconds(1);

        firstCard.Unflip();
        SecondCard.Unflip();
    }

    public void MatchPair(Card firstCard, Card SecondCard)
    {
        StartCoroutine(MatchPairRoutine(firstCard, SecondCard));
    }

    public IEnumerator MatchPairRoutine(Card firstCard, Card SecondCard)
    {
        yield return new WaitForSeconds(1);

        UpdateScores();
        OnPairMatched?.Invoke(firstCard, SecondCard);



        float elapsed = 0;
        float duration = .5f;
        while (elapsed <= duration)
        {
            firstCard.transform.localScale -= Vector3.one * (1 / duration * Time.deltaTime);
            SecondCard.transform.localScale -= Vector3.one * (1 / duration * Time.deltaTime);

            elapsed += Time.deltaTime;
            yield return null;
        }

        firstCard.gameObject.SetActive(false);
        SecondCard.gameObject.SetActive(false);

    }

    private void UpdateScores()
    {
        score += 1 + combo;
        totalPairToMatch--;
        combo++;

        if (totalPairToMatch == 0)
        {
            Win();
        }
    }

    public IEnumerator UnflipAllCardsRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);

        for (int i = 0; i < generatedCards.Length; i++)
        {
            generatedCards[i].Unflip();
        }
    }

    private void Win()
    {
        Data.SetCurrentLevel(currentLevel + 1);
        OnWin?.Invoke();
    }
}
