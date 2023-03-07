using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticPlayerStats : MonoBehaviour
{
    public static float magicDamageResistance = 0;
    public static float MagicDamageResistance
    {
        get { return magicDamageResistance; }
        set { magicDamageResistance = value; }
    }

    public static float meleeDamageResistance = 0;
    public static float MeleeDamageResistance
    {
        get { return meleeDamageResistance; }
        set { meleeDamageResistance = value; }
    }

    public static float maxHealth = 30f;
    public static float MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    public static float currentHealth = 30f;
    public static float CurrentHealth
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }

    public static float meleeDamage = 8f;
    public static float MeleDamage
    {
        get { return meleeDamage; }
        set { meleeDamage = value; }
    }

    public static float rangeDamage = 4f;
    public static float RangeDamage
    {
        get { return rangeDamage; }
        set { rangeDamage = value; }
    }

    public static float money = 0f;
    public static float Money
    {
        get { return money; }
        set { money = value; }
    }
}
