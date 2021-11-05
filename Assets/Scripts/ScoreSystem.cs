using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private float scoreMultiplayer;

    public const string HighScoreKey = "HightScore";

    private float score;

    // Update is called once per frame
    void Update()
    {
        score += Time.deltaTime * scoreMultiplayer; // счет

        scoreText.text = Mathf.FloorToInt(score).ToString(); // округление
    }
    
    private void OnDestroy() 
    {
        int currentHighScore = PlayerPrefs.GetInt(HighScoreKey,0); // получаем прежнее макс значение

        if(score > currentHighScore)
        {
            PlayerPrefs.SetInt(HighScoreKey,Mathf.FloorToInt(score)); // сохранение, если результат больше
        }
    }
}
