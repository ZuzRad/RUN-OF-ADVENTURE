using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    [Header("Armor")]
    public float armorPrice = 2;
    public float armorBuff = 2;
    public TextMeshProUGUI armorPriceText = null;
    public TextMeshProUGUI armorBuffText = null;

    [Header("Magic Boots")]
    public float magicBootsPrice = 1;
    public float magicBootsBuff = 0.5f;
    public TextMeshProUGUI magicBootsPriceText = null;
    public TextMeshProUGUI magicBootsBuffText = null;

    [Header("Sword")]
    public float swordPrice = 3;
    public float swordBuff = 2;
    public TextMeshProUGUI swordPriceText = null;
    public TextMeshProUGUI swordBuffText = null;

    [Header("Bow")]
    public float bowPrice = 3;
    public float bowBuff = 1f;
    public TextMeshProUGUI bowPriceText = null;
    public TextMeshProUGUI bowBuffText = null;

    [Header("HP Crystal")]
    public float crystalPrice = 2;
    public float crystalBuff = 2f;
    public TextMeshProUGUI crystalPriceText = null;
    public TextMeshProUGUI crystalBuffText = null;

    [Header("Boots")]
    public float bootsPrice = 4f;
    public float bootsBuff = 2f;
    public TextMeshProUGUI bootsPriceText = null;
    public TextMeshProUGUI bootsBuffText = null;

    [Header("Jump potion")]
    public float jumpPrice = 2f;
    public float jumpBuff = 0.5f;
    public TextMeshProUGUI jumpPriceText = null;
    public TextMeshProUGUI jumpBuffText = null;


    [Header("Info")]
    public TextMeshProUGUI infoText = null;
    public Button infoButton = null;
    public Image infoImage = null;

    private GameObject[] info;
    private GameObject[] page1;
    private GameObject[] page2;

    public void Start()
    {
        page1 = GameObject.FindGameObjectsWithTag("1Page");
        page2 = GameObject.FindGameObjectsWithTag("2Page");
        info = GameObject.FindGameObjectsWithTag("Info");
        HideObjects(info);
        HideObjects(page2);

        armorPriceText.text = armorPrice + "";
        armorBuffText.text = "+" + armorBuff + " Melee Damage Resistance";

        magicBootsPriceText.text = magicBootsPrice + "";
        magicBootsBuffText.text = "+" + magicBootsBuff + " Magic Damage Resistance";

        swordPriceText.text = swordPrice + "";
        swordBuffText.text = "+" + swordBuff + " Melee Attack Damage";

        bowPriceText.text = bowPrice+"";
        bowBuffText.text = "+" + bowBuff + " Range Attack Damage";

        crystalPriceText.text = crystalPrice + "";
        crystalBuffText.text = "+" + crystalBuff + " Max HP";

        jumpPriceText.text = jumpPrice + "";
        jumpBuffText.text = "Out of stock";

        bootsPriceText.text = bootsPrice + "";
        bootsBuffText.text = "Out of stock";
    }
    public void Page1()
    {
        HideObjects(page2);
        ShowObjects(page1);
    }
    public void Page2()
    {
        HideObjects(page1);
        ShowObjects(page2);
    }
    private void ShowObjects(GameObject[] page)
    {
        for (int i = 0; i < page.Length; i++)
            page[i].SetActive(true);
    }

    private void HideObjects(GameObject[] page)
    {
        for(int i = 0; i < page.Length; i++)
            page[i].SetActive(false);
    }

    public void HideInfo()
    {
        HideObjects(info);
    }

    public void LoadPauseStage()
    {
        SceneManager.LoadScene(2);
    }

    public void Buy(float item)
    {
        if (item == 1)
        {
            if(StaticPlayerStats.money >= armorPrice)
            {
                if (StaticItems.isArmorBought == false)
                {
                    StaticPlayerStats.money -= armorPrice;
                    StaticPlayerStats.meleeDamageResistance += armorBuff;
                    StaticEnemyStats.enemyMeleeDamage -= StaticPlayerStats.meleeDamageResistance;
                    StaticItems.isArmorBought = true;
                }
                else
                    HaveItem();
            }
            else
                NoMoney();
        }
        else if(item == 2)
        {
            if(StaticPlayerStats.money >= magicBootsPrice)
            {
                if (StaticItems.isMagicBootsBought == false)
                {
                    StaticPlayerStats.money -= magicBootsPrice;
                    StaticPlayerStats.magicDamageResistance += magicBootsBuff;
                    StaticEnemyStats.enemyRangeDamage -= StaticPlayerStats.magicDamageResistance;
                    StaticItems.isMagicBootsBought = true;
                }
                else
                    HaveItem();
            }
            else
                NoMoney();
        }
        else if(item==3)
        {
            if(StaticPlayerStats.money >= swordPrice)
            {
                    StaticPlayerStats.money -= swordPrice;
                    StaticPlayerStats.meleeDamage += swordBuff;
                    StaticItems.isSwordBought = true;
            }
            else
                NoMoney();
        }
        else if(item==4)
        {
            if(StaticPlayerStats.money >= bowPrice)
            {
                    StaticPlayerStats.money -= bowPrice;
                    StaticPlayerStats.rangeDamage += bowBuff;
                    StaticItems.IsBowBought = true;
            }
            else
                NoMoney();
        }
        else if (item == 5)
        {
            if (StaticPlayerStats.money >= crystalPrice)
            {
                StaticPlayerStats.money -= crystalPrice;
                StaticPlayerStats.maxHealth += crystalBuff;
            }
            else
                NoMoney();
        }
    }

    private void NoMoney()
    {
        ShowObjects(info);
        infoText.text = "You don't have enough money";
    }

    private void HaveItem()
    {
        ShowObjects(info);
        infoText.text = "You already have this item";
    }
}
