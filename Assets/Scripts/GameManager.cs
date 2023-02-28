using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Player")]
    private Player playerObject;
    public TextMeshProUGUI playerStats = null;

    [Header("Time")]
    public float startTime = 3f;
    public TextMeshProUGUI startTimetext = null;


    private GameObject[] enemies;
    private SoundManager soundManager;

    private void Update()
    {
        StatsText();
        StartTimeText();
        Lost();
        Win();
        WinGame();
    }

    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex != 1)
        {
            playerObject = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            
        }
        if (SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 0)
        {
            Cursor.visible = true;
        }
        else
        {
            Cursor.visible = false;
        }
            soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        
    }

    private void StatsText()
    {
        if (StaticPlayerStats.currentHealth < 0)
        {
            playerStats.text = "Health: 0/" + StaticPlayerStats.maxHealth + "\n" + "Money: " + StaticPlayerStats.money;
        }
        else
        {
            playerStats.text = "HP: " + StaticPlayerStats.currentHealth + "/" + StaticPlayerStats.maxHealth + "\n" + "Money: " + StaticPlayerStats.money+ "\n" +
                                "BowDmg: " + StaticPlayerStats.rangeDamage+"\n"+"MeleeDmg: "+ StaticPlayerStats.meleeDamage + "\n"+
                                "MagicRes: " + StaticPlayerStats.magicDamageResistance + "\n" +
                                "Armor: " + StaticPlayerStats.meleeDamageResistance;
        }
    }

    private void StartTimeText()
    {
        if (SceneManager.GetActiveScene().buildIndex >2)
        {
            startTime -= Time.deltaTime;
            startTimetext.text = "Time: " + Mathf.Clamp(Mathf.CeilToInt(startTime), 0, int.MaxValue).ToString();
        }
        else
        {
            startTimetext.text = "";
        }

        if (startTime <= 0)
        {
            startTimetext.text = "";
        }
    }

    private void Lost()
    {
        if (playerObject == null && SceneManager.GetActiveScene().buildIndex != 1)
        {
            StaticPlayerStats.money = 0;
            startTimetext.text = "You Lost!";
            StartCoroutine(WinWait());
        }
    }

    private void Win()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0 && SceneManager.GetActiveScene().buildIndex >2)
        {
            soundManager.PlaySound(SoundManager.Sounds.Win);
            startTimetext.text = "Stage Clear!";
            StartCoroutine(WinWait());

        }
    }

    private void WinGame()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0 && SceneManager.GetActiveScene().buildIndex == 6)
        {
            soundManager.PlaySound(SoundManager.Sounds.Win);
            startTimetext.text = "YOU WIN!";

            StartCoroutine(WinGameWait());
            Reset();
        }
        

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
    private IEnumerator WinGameWait()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0);
    }

    private IEnumerator WinWait()
    {
        yield return new WaitForSeconds(3);
        if (enemies.Length == 0)
        {
            if(SceneManager.GetActiveScene().buildIndex == 7)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                SceneManager.LoadScene(2);
            }
            
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

}
