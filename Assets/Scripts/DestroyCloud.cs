using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCloud : MonoBehaviour
{
    public float life;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            Debug.Log("FGFF");
            Wait();
        }
    }

    private void Wait()
    {
        life -= 0.5f;

        if (life <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.layer == 7)
        {
            Wait();
        }
    }
}
