using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CardUIManager : MonoBehaviour
{
    //Properties
    [Header("Card Properties")]
    [SerializeField] private Card card;

    [Header("UI Elements")]
    [SerializeField] private Image cardImage;
    [SerializeField] private Image icon;
    [SerializeField] private Button button;

    private void OnEnable()
    {
        Card.OnUnflipped += OnAnyCardUnflipped;
        Card.OnFlipped += OnAnyCardFlipped;
        GameManager.OnPairMatched += OnAnyPairMatched;
    }


    private void OnDisable()
    {
        Card.OnUnflipped -= OnAnyCardUnflipped;
        Card.OnFlipped -= OnAnyCardFlipped;
        GameManager.OnPairMatched -= OnAnyPairMatched;
    }

    private void Awake()
    {
        button.onClick.AddListener(OnClickButton);
        button.enabled = false;
    }

    private void OnClickButton()
    {
        if (!card.IsFlipped)
        {
            card.Flip();
        }
    }

    public void SetIcon(Sprite sprite)
    {
        icon.sprite = sprite;
    }
    private void OnAnyPairMatched(Card firstCard, Card secondCard)
    {
        // If any pair matched, all other card button should be enabled
        if (firstCard.InstanceId != card.InstanceId && secondCard.InstanceId != card.InstanceId)
        {
            button.enabled = true;
        }
    }

    private void OnAnyCardFlipped(Card flippedCard)
    {
        if (card.InstanceId == flippedCard.InstanceId)
        {
            button.enabled = false;
            StartCoroutine(FlipAnimationRoutine(-1));
        }
    }

    private void OnAnyCardUnflipped(Card unflippedCard)
    {
        if (card.InstanceId == unflippedCard.InstanceId)
        {
            StartCoroutine(FlipAnimationRoutine(1));
        }
        else
        {
            button.enabled = true;
        }

    }

    private IEnumerator FlipAnimationRoutine(int direction)
    {
        float elapsed = 0;
        float duration = .5f;
        float halfDuration = duration * .5f;
        bool isDirectionChanged = false;

        Vector3 cardImageEulerAngles = cardImage.rectTransform.eulerAngles;

        while (elapsed <= duration)
        {
            if (!isDirectionChanged && elapsed > halfDuration)
            {
                if (direction == 1)
                {
                    direction = -1;
                    cardImage.sprite = UIManager.CardUnflippedSprite;
                    icon.enabled = false;
                }
                else
                {
                    direction = 1;
                    cardImage.sprite = UIManager.CardFlippedSprite;
                    icon.enabled = true;
                }

                isDirectionChanged = true;
            }

            cardImageEulerAngles.y += direction * (90f / halfDuration) * Time.deltaTime;
            cardImage.rectTransform.eulerAngles = cardImageEulerAngles;


            elapsed += Time.deltaTime;
            yield return null;
        }

        if (!card.IsFlipped)
        {
            button.enabled = true;
        }
    }
}
