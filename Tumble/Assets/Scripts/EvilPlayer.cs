using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilPlayer : MonoBehaviour
{
    public Vector3 originalPosition = new Vector3(-26f, -78.5f, 0f);
    public bool isWalking = false;
    void Update()
    {
        if(isWalking)
        {
            transform.position += new Vector3(7f * Time.deltaTime, 0f);
        }
    }

    public void ResetPostion()
    {
        isWalking = false;
        transform.position = originalPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isWalking && collision.CompareTag("Player"))
        {
            GameManager.Respawn();
            ResetPostion();
        }
    }
}
