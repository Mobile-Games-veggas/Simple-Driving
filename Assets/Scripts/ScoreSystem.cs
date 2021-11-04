using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private float scoreMultiplayer;

    private float score;

    // Update is called once per frame
    void Update()
    {
        score += Time.deltaTime * scoreMultiplayer;

        scoreText.text = Mathf.FloorToInt(score).ToString();
    }
}
