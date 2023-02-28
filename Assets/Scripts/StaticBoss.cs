using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticBoss : MonoBehaviour
{
    public static float bossMaxHealth = 50f;
    public static float BossMaxHealth
    {
        get { return bossMaxHealth; }
        set { bossMaxHealth = value; }
    }


    public static float bossMeleeDamage = 5f;
    public static float BossMeleeDamage
    {
        get { return bossMeleeDamage; }
        set { bossMeleeDamage = value; }
    }


    public static float bossRangeDamage = 1f;
    public static float BossRangeDamage
    {
        get { return bossRangeDamage; }
        set { bossRangeDamage = value; }
    }
}
