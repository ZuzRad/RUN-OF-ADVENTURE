 using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialMesseges : MonoBehaviour
{
    public TextMeshProUGUI message = null;
    public string text = null;
    public GameObject image = null;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            image.SetActive(true);
            message.text = text; 
        }
    }
}
