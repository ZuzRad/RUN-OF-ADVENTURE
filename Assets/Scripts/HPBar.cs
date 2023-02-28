using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public Slider Slider;
    public Color Low;
    public Color High;
    public Image sliderIMG;
    public Vector3 Offset;

    public void SetHealth(float currentHealth, float maxHealth)
    {
       
        Slider.gameObject.SetActive(currentHealth < maxHealth);
        Slider.value= currentHealth;
        Slider.maxValue = maxHealth;
        sliderIMG.color= Color.Lerp(Color.red, Color.green, Slider.normalizedValue);
       // Slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Low, High, Time.time);
    }

    void Update()
    {
        Slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + Offset);
    }
}
