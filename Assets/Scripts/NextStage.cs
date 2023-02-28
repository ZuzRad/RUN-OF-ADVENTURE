using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextStage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(StaticData.actualStage);
        if (collision.gameObject.layer == 7)
        {
            StaticData.actualStage++;
            SceneManager.LoadScene(StaticData.actualStage);
        }
    }
}
