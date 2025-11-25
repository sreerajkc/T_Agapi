using UnityEngine;

[CreateAssetMenu(menuName = "SO/Level Data")]
public class LevelData : ScriptableObject
{
    //Property
    [Header("Grid Properties")]
    [SerializeField] private int rows;
    [SerializeField] private int columns;


    //Fields
    public int Rows=> rows;
    public int Columns => columns;    
}

