using System;
using UnityEngine;


public class Card : MonoBehaviour
{
    //Fields
    public int Id { get; private set;}
    public int InstanceId { get; private set; }
    public bool IsFlipped { get; private set; }

    //Events
    public static event Action<Card> OnInitialized;
    public static event Action<Card> OnFlipped;
    public static event Action<Card> OnUnflipped;

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

}
