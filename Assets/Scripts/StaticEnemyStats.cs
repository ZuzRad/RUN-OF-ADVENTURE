using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemyStats : MonoBehaviour
{
    public static float enemyMaxHealth = 15f;
    public static float EnemyMaxHealth
    {
        get { return enemyMaxHealth; }
        set { enemyMaxHealth = value; }
    }


    public static float enemyMeleeDamage = 3f;
    public static float EnemyMeleeDamage
    {
        get { return enemyMeleeDamage; }
        set { enemyMeleeDamage = value; }
    }


    public static float enemyRangeDamage = 1f;
    public static float EnemyRangeDamage
    {
        get { return enemyRangeDamage; }
        set { enemyRangeDamage = value; }
    }
}
