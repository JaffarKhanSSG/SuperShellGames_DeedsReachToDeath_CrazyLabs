using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static bool IsSwitchOn(string prefName)
    {
        if (PlayerPrefs.GetString(prefName) == "OFF")
            return false;
        return true;
    }
    public static void SetSwitchOnTo(string prefName,string val)
    {
        PlayerPrefs.SetString(prefName, val);
    }
    public static void Vibrate()
    {
        if (!GameData.IsSwitchOn("Vibrate")) return;
        Handheld.Vibrate();
    }
    public static int GetCoins()
    {
        return PlayerPrefs.GetInt("Coins");
    }
    public static void SetCoins(int val)
    {
        PlayerPrefs.SetInt("Coins", val);
    }
    public static int GetLevel()
    {
        return PlayerPrefs.GetInt("Level",1);
    }
    public static void SetLevel(int val)
    {
        PlayerPrefs.SetInt("Level", val);
    }
    public static bool IsHelpPageShowned(int id)
    {
        if(PlayerPrefs.GetString("IsHelpPageShowned"+id)=="true")
            return true;
        return false;
    }
    public static void SetIsHelpPageShowned(int id,string val)
    {
        PlayerPrefs.SetString("IsHelpPageShowned" + id, val); 
           
    }
}
