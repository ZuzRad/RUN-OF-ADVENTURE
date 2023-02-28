using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = true;
    }
    public void StartGame()
    {
        Reset();
        SceneManager.LoadScene(3);
    }

    public void Tutorial()
    {
        Reset();
        SceneManager.LoadScene(7);
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void Reset()
    {
        StaticPlayerStats.maxHealth = 30f;
        StaticPlayerStats.currentHealth = 30f;
        StaticPlayerStats.meleeDamageResistance = 0;
        StaticPlayerStats.magicDamageResistance = 0;
        StaticPlayerStats.meleeDamage = 8f;
        StaticPlayerStats.rangeDamage = 4f;
        StaticPlayerStats.money = 0;
        StaticData.actualStage = 3;
    }
}
