using UnityEngine;

[CreateAssetMenu(menuName = "SO/Leve Data")]
public class LevelData : ScriptableObject
{
    [Header("Grid Properties")]
    [SerializeField] private int rows;
    [SerializeField] private int columns;

    public int Rows=> rows;
    public int Columns => columns;    
}

