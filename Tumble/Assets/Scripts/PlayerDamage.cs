using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if(collision.CompareTag("Danger"))
            {
                // GameManager.RespawnPoint = null;
                // GameManager.CustomRespawnPoint = null;
                GameManager.Player = null;  
                GameManager.Respawn();
            }

        }
    }
}
