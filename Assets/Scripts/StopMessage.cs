using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StopMessage : MonoBehaviour
{
    public TextMeshProUGUI message = null;
    public GameObject image=null;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer == 7)
        {
            image.SetActive(false);
            message.text = "";

        }
    }
}
