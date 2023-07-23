using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
    private static GameObject respawnPoint = GameObject.FindGameObjectWithTag("RespawnPoint");
    public static GameObject RespawnPoint { get { return respawnPoint; }}

    private static GameObject customRespawnPoint = null;
    public static GameObject CustomRespawnPoint { get { return customRespawnPoint; } set { customRespawnPoint = value; } }

    private static GameObject player = GameObject.FindGameObjectWithTag("Player");
    public static GameObject Player { get { return player; } }

    private static GameObject heroineEntrance = GameObject.FindGameObjectWithTag("HeroineEntrance");
    public static GameObject HeroineEntrance { get { return heroineEntrance; } }

    public static GameObject evilPlayer = GameObject.FindGameObjectWithTag("EvilPlayer");

    public static void Respawn()
    {
        if(respawnPoint != null)
            respawnPoint = GameObject.FindGameObjectWithTag("RespawnPoint");

        if(evilPlayer != null)
        {
            evilPlayer.GetComponent<EvilPlayer>().ResetPosition();
        }
        else
        {
            evilPlayer = GameObject.FindGameObjectWithTag("EvilPlayer");
        }
            
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.transform.position = customRespawnPoint != null ? customRespawnPoint.transform.position : respawnPoint.transform.position;
    }

    public static void ResetRespawnPoint()
    {
        customRespawnPoint = null;
    }
}
