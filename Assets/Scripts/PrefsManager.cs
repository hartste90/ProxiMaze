using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PrefsManager
{
    public static string PREF_PLAYER_LEVEL_STARS = "player_level_stars";
    public static string PREF_PLAYER_UNLOCKED_LEVEL = "player_highest_unlocked_level";


    public static int GetPlayerUnlockedLevel()
    {
        Debug.LogFormat("GetPref: {0} -- {1}", PREF_PLAYER_UNLOCKED_LEVEL, PlayerPrefsPro.GetInt(PREF_PLAYER_UNLOCKED_LEVEL, 0));

        return PlayerPrefsPro.GetInt(PREF_PLAYER_UNLOCKED_LEVEL, 0);
    }

    public static void SetUnlockedLevel(int levelSet)
    {
        PlayerPrefsPro.SetInt(PREF_PLAYER_UNLOCKED_LEVEL, levelSet);
        PlayerPrefsPro.Save();
        Debug.LogFormat("SetPref: {0} -- {1}", PREF_PLAYER_UNLOCKED_LEVEL, levelSet);
    }

    public static int GetStarsForLevel(int level)
    {
        int[] levelStarsList = PlayerPrefsPro.GetIntArray(PREF_PLAYER_LEVEL_STARS, new int[50]);
        if (levelStarsList.Length >= level)
        {
            return levelStarsList[level];
        }
        else
        {
            int[] newLevelStarsList = new int[level + 10];
            int i = 0;
            for(; i < levelStarsList.Length; i++)
            {
                newLevelStarsList[i] = levelStarsList[i];
            }
            for(;i < level + 10; i++)
            {
                newLevelStarsList[i] = 0;
            }

            PlayerPrefsPro.SetIntArray(PREF_PLAYER_LEVEL_STARS, newLevelStarsList);
            PlayerPrefsPro.Save();
            return 0;
        }
    }

    public static void SetStarsForLevel(int level, int starsSet)
    {
        int[] levelStarsList = PlayerPrefsPro.GetIntArray(PREF_PLAYER_LEVEL_STARS, new int[50]);
        if (levelStarsList.Length >= level && levelStarsList[level] < starsSet)
        {
            levelStarsList[level] = starsSet;
            Debug.LogFormat("Setting1 stars for Level ({0}) --> {1}", level, starsSet);
        }
        else
        {
            int[] newLevelStarsList = new int[level + 10];
            int i = 0;
            for (; i < levelStarsList.Length; i++)
            {
                newLevelStarsList[i] = levelStarsList[i];
            }
            for (; i < level + 10; i++)
            {
                newLevelStarsList[i] = 0;
            }
            newLevelStarsList[level] = starsSet;
            levelStarsList = newLevelStarsList;
        }
        PlayerPrefsPro.SetIntArray(PREF_PLAYER_LEVEL_STARS, levelStarsList);
        PlayerPrefsPro.Save();
    }

    public static void ResetStarsForLevel(int level)
    {
        int[] levelStarsList = PlayerPrefsPro.GetIntArray(PREF_PLAYER_LEVEL_STARS, new int[50]);
        levelStarsList[level] = 0;
        PlayerPrefsPro.SetIntArray(PREF_PLAYER_LEVEL_STARS, levelStarsList);
        PlayerPrefsPro.Save();

    }

}
