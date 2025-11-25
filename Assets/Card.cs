using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Class for core card logic
/// </summary>
public class Card : MonoBehaviour
{
    public int Id { get; private set;}
    public int InstanceId { get; private set; }
    public bool IsFlipped { get; private set; }

    public static event Action<Card> OnInitialized;

    public static event Action<Card> OnFlipped;
    public static event Action<Card> OnUnflipped;
    public static event Action<Card> OnMatched;

    public void Initialize(int id,int instanceId)
    {
        Id = id;
        InstanceId = instanceId;

        OnInitialized?.Invoke(this);
    }

    public void Flip()
    {
        IsFlipped = true;
        OnFlipped?.Invoke(this);
    }

    public void Unflip()
    {
        IsFlipped= false;
        OnUnflipped?.Invoke(this);
    }

    public void Match()
    {
        OnMatched?.Invoke(this);
    }

}
