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

    public static void Respawn()
    {
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.transform.position = customRespawnPoint != null ? customRespawnPoint.transform.position : respawnPoint.transform.position;
    }

    public static void ResetRespawnPoint()
    {
        customRespawnPoint = null;
    }
}
