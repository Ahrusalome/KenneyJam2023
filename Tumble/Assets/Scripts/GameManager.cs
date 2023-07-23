using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManager
{

    private static GameObject respawnPoint = GameObject.FindGameObjectWithTag("RespawnPoint");
    public static  GameObject RespawnPoint { get { return respawnPoint; } set { respawnPoint = value; } }

    private static GameObject customRespawnPoint = null;
    public static  GameObject CustomRespawnPoint { get { return customRespawnPoint; } set { customRespawnPoint = value; } }

    public static GameObject player = GameObject.FindGameObjectWithTag("Player");
    public static  GameObject Player { get { return player; } set { player = value; } }

    private static GameObject heroineEntrance = GameObject.FindGameObjectWithTag("HeroineEntrance");
    public static  GameObject HeroineEntrance { get { return heroineEntrance; } }

    public static  GameObject evilPlayer = GameObject.FindGameObjectWithTag("EvilPlayer");


    public static  void Respawn()
    {
        if(respawnPoint == null)
            respawnPoint = GameObject.FindGameObjectWithTag("RespawnPoint");

        if(evilPlayer != null)
        {
            evilPlayer.GetComponent<EvilPlayer>().ResetPosition();
        }
        else
        {
            evilPlayer = GameObject.FindGameObjectWithTag("EvilPlayer");
        }
        if(player == null)
            player = GameObject.FindGameObjectWithTag("Player");
            
        player.transform.position = customRespawnPoint != null ? customRespawnPoint.transform.position : respawnPoint.transform.position;
    }

    public static  void ResetRespawnPoint()
    {
        customRespawnPoint = null;
    }
}
