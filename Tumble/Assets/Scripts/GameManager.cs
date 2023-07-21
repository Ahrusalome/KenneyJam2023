using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
    private static GameObject respawnPoint = GameObject.FindGameObjectWithTag("RespawnPoint");
    public static GameObject RespawnPoint
    {
        get { return respawnPoint; }
    }

    public static void Respawn(GameObject player)
    {
        player.transform.position = respawnPoint.transform.position;
    }
}
