using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Text curLifeTimeText;

    [SerializeField] private int maxLifeTime;

    private int curLifeTime;

    private int frameCounter = 0;
    private bool isPlaying = true;

    private void Start() 
    {
        curLifeTime = maxLifeTime;
        UpdateCurLifeTime();
    }
    private void FixedUpdate()
    {
        if (frameCounter > 49 && isPlaying == true) 
        {
            curLifeTime--;
            UpdateCurLifeTime();
            frameCounter = 0;
            if(curLifeTime < 1) 
            {
                GameOver();
            }
        }
        frameCounter++;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Border"))
        {
            GameOver();
        }

        if (collision.gameObject.CompareTag("Water")) 
        {
            Destroy(collision.gameObject);
            curLifeTime = maxLifeTime;
            UpdateCurLifeTime();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) 
        {
            playerController.PlayLandingSound();
        }
    }

    private void UpdateCurLifeTime() 
    { 
        curLifeTimeText.text = "Remaining time: " + curLifeTime.ToString();
    }

    private void GameOver() 
    {
        isPlaying = false;
        gameObject.SetActive(false);
        menu.SetActive(true);
        playerController.SetGameState(isPlaying);
    }
}
