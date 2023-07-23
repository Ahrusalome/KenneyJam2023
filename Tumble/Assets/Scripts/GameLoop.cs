using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    [SerializeField] private float speed = 7;
    [SerializeField] private float jumpHeight = 10;
    [SerializeField] private float airControlSlowDown = 1f;
    [SerializeField] private float runHoldDecrease = 5f;
    [SerializeField] private bool isEntranceActive = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Loop();
    }

    public void Loop()
    {
        ParameterChanger.ChangePlayerParameter(speed, jumpHeight, airControlSlowDown, runHoldDecrease);
        GameManager.HeroineEntrance.SetActive(isEntranceActive);
        GameManager.CustomRespawnPoint = null;
        GameManager.Respawn();
    }
}
