using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticItems : MonoBehaviour
{
   public static bool isArmorBought=false;
    public static bool IsArmorBought
    {
        get { return isArmorBought; }
        set { isArmorBought = value; }
    }

    public static bool isMagicBootsBought = false;
    public static bool IsMagicBootsBought
    {
        get { return isMagicBootsBought; }
        set { isMagicBootsBought = value; }
    }

    public static bool isSwordBought = false;
    public static bool IsSwordBought
    {
        get { return isSwordBought; }
        set { isSwordBought = value; }
    }

    public static bool isBowBought = false;
    public static bool IsBowBought
    {
        get { return isBowBought; }
        set { isBowBought = value; }
    }
}
