using System;
using UnityEngine;

/// <summary>
/// Class for core card logic
/// </summary>
public class Card : MonoBehaviour
{
    private int id;

    public int Id => id;

    public static event Action<Card> OnInitialized;
    public static event Action<Card> OnFlipped;


    public void Initialize(int id)
    {
        this.id = id;
        OnInitialized?.Invoke(this);
    }

    public void Flip()
    {
        OnFlipped?.Invoke(this);
    }

}
