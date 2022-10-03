using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpaceGameController : MonoBehaviour
{
    [SerializeField] List<GameObject> enemies;
    [SerializeField] List<GameObject> enemyBosses;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;
    public int score = 0;
    public int highScore = 0;
    bool spawnBoss = true;
    private void Start()
    {
        StartCoroutine(SpawnEnemy());
        highScore = PlayerPrefs.GetInt("HighScore");
    }

    private void Update()
    {
        if (spawnBoss && (score > 0) && (score % 10 == 0))
        {
            GameObject boss = enemyBosses[Random.Range(0, enemyBosses.Count)];
            SpawnEnemyEntity(boss);
            spawnBoss = false;
        }
        else if (!((score > 0) && (score % 10 == 0)))
        {
            spawnBoss = true;
        }
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScore = score;
        }
        scoreText.text = $"{score}";
        highScoreText.text = $"High Score: {highScore}";
    }

    void SpawnEnemyEntity(GameObject enemy)
    {
        float x = Random.Range(-3.0f, 6.5f);
        float y = Random.Range(-3.5f, 3.5f);
        Vector2 pos = new Vector2(x, y);
        Quaternion q = Quaternion.Euler(0, 0, -90f);
        GameObject enemyGameObject = Instantiate(enemy, pos, q);
        float dir = Random.Range(0, 2) > 0 ? -1.0f : 1.0f;
        enemyGameObject.GetComponent<EnemyController>().moveDirection *= dir;
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(2.0f);
        while (true)
        {
            GameObject enemy = enemies[Random.Range(0, enemies.Count)];
            SpawnEnemyEntity(enemy);
            yield return new WaitForSeconds(3.0f);
        }
    }
}
