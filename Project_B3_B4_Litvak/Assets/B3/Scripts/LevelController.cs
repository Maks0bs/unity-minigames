using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelController : MonoBehaviour
{
    float timer = 0.0f;
    float highscore = 0.0f;
    [SerializeField] string levelName;
    [SerializeField] TextMeshProUGUI scoreDisplay;
    [SerializeField] TextMeshProUGUI highscoreDisplay;
    void Start()
    {
        highscore = PlayerPrefs.GetFloat($"{levelName}_highscore");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        scoreDisplay.text = timer.ToString("F2");
        if (timer > highscore)
        {
            highscore = timer;
            PlayerPrefs.SetFloat($"{levelName}_highscore", timer);
        }
        highscoreDisplay.text = $"Highscore: {highscore.ToString("F2")}";
    }
}
