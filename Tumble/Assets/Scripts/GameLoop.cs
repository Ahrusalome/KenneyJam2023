using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoop : MonoBehaviour
{
    [SerializeField] private float speed = 7;
    [SerializeField] private float jumpHeight = 10;
    [SerializeField] private float airControlSlowDown = 1f;
    [SerializeField] private float runHoldDecrease = 5f;
    [SerializeField] private bool isEntranceActive = true;
    [SerializeField] private AudioClip clip;
    [SerializeField] private string nextScene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Loop();
        SoundManager.Instance.PlayMusic(clip);
        SceneManager.LoadScene(nextScene);
    }
        public void Loop()
    {
        ParameterChanger.ChangePlayerParameter(speed, jumpHeight, airControlSlowDown, runHoldDecrease);
        GameManager.HeroineEntrance.SetActive(isEntranceActive);
        GameManager.CustomRespawnPoint = null;

        GameManager.Respawn();
    }
}
