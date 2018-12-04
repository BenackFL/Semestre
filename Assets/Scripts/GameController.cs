﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState { Idle, Playing, Ended, Ready};

public class GameController : MonoBehaviour
{


    [Range(0f, 0.2f)]
    public float parallaxSpeed = 0.02f;
    public RawImage background;
    public RawImage plataform;
    public GameObject uiIdle;
    public GameObject uiScore;
    public Text pointsText;
    public Text recordText;

    public GameState gameState = GameState.Idle;

    public GameObject player;
    public GameObject enemyGenerator;

    public float scaleTime = 6f;
    public float scaleInc = .25f;


    private AudioSource musicPlayer;
    private int points = 0;

    // Use this for initialization
    void Start()
    {
        musicPlayer = GetComponent<AudioSource>();
        recordText.text = "BEST: " + GetMaxScore().ToString();

    }

    // Update is called once per frame
    void Update()
    {
        bool userAction = Input.GetKeyDown("up") || Input.GetMouseButtonDown(0);

        //Empezar Juego
        if (gameState == GameState.Idle && userAction)
        {
            gameState = GameState.Playing;
            uiIdle.SetActive(false);
            uiScore.SetActive(true);

            player.SendMessage("UpdateState", "PlayerRun");
            enemyGenerator.SendMessage("StartGenerator");
            musicPlayer.Play();
            InvokeRepeating("GameTimeScale", scaleTime, scaleTime);

        }
        //Juego iniciado
        else if (gameState == GameState.Playing)
        {
            Parallax();
        }

        //Juego preparado para reiniciarse
        else if (gameState == GameState.Ready)
        {
            if (userAction)
            {
                RestartGame();
            }
        }
    }

    void Parallax()
    {

        float finalSpeed = parallaxSpeed * Time.deltaTime;
        background.uvRect = new Rect(background.uvRect.x + finalSpeed, 0f, 1f, 1f);
        plataform.uvRect = new Rect(plataform.uvRect.x + finalSpeed * 4, 0f, 1f, 1f);
    
    }

    public void RestartGame()
    {
        ResetTimeScale();
        SceneManager.LoadScene("Juego");
    }

    void GameTimeScale()
    {
        Time.timeScale += scaleInc;
        Debug.Log("ritmo incrementado: " + Time.timeScale.ToString());
    }

    public void ResetTimeScale(float newTimeScale = 1f)
    {
        CancelInvoke("GameTimeScale");
        Time.timeScale = newTimeScale; ;
        Debug.Log("Ritmo restablecido" + Time.timeScale.ToString());
    }

    public void IncreasePoints()
    {
        points++;
        pointsText.text = points.ToString();
        if (points >= GetMaxScore())
        {
            recordText.text = "BEST: " + points.ToString();
            SaveScore(points);

        }

    }

    public int GetMaxScore()
    {
        return PlayerPrefs.GetInt("Max Points", 0);
    }

    public void SaveScore(int currentPoints)
    {
        PlayerPrefs.SetInt("Max Points", currentPoints);
    }

}