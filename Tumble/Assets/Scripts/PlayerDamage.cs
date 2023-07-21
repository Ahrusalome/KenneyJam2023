using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("test");
        if (collision != null)
        {
            if(collision.CompareTag("Danger"))
            {
                GameManager.Respawn(gameObject);
            }
        }
    }
}
