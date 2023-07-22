using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParameterChanger : MonoBehaviour
{
    [SerializeField] private GameObject respawnPoint;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.CustomRespawnPoint = respawnPoint;
    }

    public static void ChangePlayerParameter(float _speed, float _jumpheight, float _airControlSlowDown)
    {
        PlayerMovement player = GameManager.Player.GetComponent<PlayerMovement>();

        player.Speed = _speed;
        player.JumpHeight = _jumpheight;
        player.AirControlSlowDown = _airControlSlowDown;
    }
}