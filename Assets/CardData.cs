using UnityEngine;

[CreateAssetMenu(menuName = "Cards SO/Card Data")]
public class CardData : ScriptableObject
{
    [SerializeField] private int id;
    [SerializeField] private Sprite icon;

    public int Id => id;
    public Sprite Icon => icon;
}
