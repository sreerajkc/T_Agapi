using UnityEngine;

public class Data
{
    public static void SetCurrentLevel(int level)
    {
        PlayerPrefs.SetInt("game_level", level);
    }

    public static int GetCurrentLevel()
    {
       return PlayerPrefs.GetInt("game_level", 1);
    }

    public static void Reset()
    {
        PlayerPrefs.DeleteAll();
    }
}
