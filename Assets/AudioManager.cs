using System;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Properties")]
    [SerializeField] private AudioSource audioSource;

    [Header("Audio Clip Properties")]
    [SerializeField] private AudioClip cardFlipSfx;
    [SerializeField] private AudioClip pairMatchSfx;
    [SerializeField] private AudioClip pairUnmatchSfx;

    private void OnEnable()
    {
        Card.OnFlipped += PlayCardFlipSfx;
        GameManager.OnPairMatched += PlayPairMatchSfx;
        GameManager.OnPairUnmatched += PlayPairUnmatchSfx;
    }
    private void OnDisable()
    {
        Card.OnFlipped -= PlayCardFlipSfx;
        GameManager.OnPairMatched -= PlayPairMatchSfx;
        GameManager.OnPairUnmatched -= PlayPairUnmatchSfx;
    }

    private void PlayCardFlipSfx(Card card)
    {
        PlaySfx(cardFlipSfx);
    }

    private void PlayPairMatchSfx(Card card1, Card card2)
    {
        PlaySfx(pairMatchSfx);

    }

    private void PlayPairUnmatchSfx(Card card1, Card card2)
    {
        PlaySfx(pairUnmatchSfx);
    }


    private void PlaySfx(AudioClip audioClip)
    {
        audioSource.PlayOneShot(cardFlipSfx);
    }

    

}
