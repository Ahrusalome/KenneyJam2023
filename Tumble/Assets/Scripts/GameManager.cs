using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManager
{
    // public static static GameManager Instance;
    // void Awake() {
    //     if (Instance == null) {
    //         Instance = this;
    //         DontDestroyOnLoad(gameObject);
    //     }
    //     else {
    //         Destroy(gameObject);
    //     }
    // }

    private static GameObject respawnPoint = GameObject.FindGameObjectWithTag("RespawnPoint");
    static void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        respawnPoint = GameObject.FindGameObjectWithTag("RespawnPoint");
        Debug.Log("oui");
    }

    private static GameObject customRespawnPoint = null;
    public static  GameObject CustomRespawnPoint { get { return customRespawnPoint; } set { customRespawnPoint = value; } }

    private static GameObject player = GameObject.FindGameObjectWithTag("Player");
    public static  GameObject Player { get { return player; } }

    private static GameObject heroineEntrance = GameObject.FindGameObjectWithTag("HeroineEntrance");
    public static  GameObject HeroineEntrance { get { return heroineEntrance; } }

    public static  GameObject evilPlayer = GameObject.FindGameObjectWithTag("EvilPlayer");


    public static  void Respawn()
    {
        evilPlayer.GetComponent<EvilPlayer>().ResetPosition();
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.transform.position = customRespawnPoint != null ? customRespawnPoint.transform.position : respawnPoint.transform.position;
    }

    public static  void ResetRespawnPoint()
    {
        customRespawnPoint = null;
    }
}
