using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneGameController : MonoBehaviour
{
    [SerializeField] GameObject topObstacle;
    [SerializeField] GameObject topObstacleAdvanced;
    [SerializeField] GameObject bottomObstacle;
    [SerializeField] GameObject bottomObstacleAdvanced;
    [SerializeField] GameObject scoreLine;
    [SerializeField] GameObject scoreLineAdvanced;
    [SerializeField] GameObject powerUp;
    int obstacleCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnObstacle());
    }

    IEnumerator SpawnObstacle()
    {
        Vector3 posBottom = new Vector3(8, -1, 0);
        Vector3 posTop = new Vector3(8, 2, 0);
        Vector3 posPowerUp = new Vector3(8, 0.6f, 0);
        while (true)
        {

            if (obstacleCount % 6 == 0 && obstacleCount > 0)
            {
                Instantiate(powerUp, posPowerUp, Quaternion.identity);
            }

            if (obstacleCount % 10 == 0 && obstacleCount > 0)
            {
                if (obstacleCount % 20 == 0)
                {
                    Instantiate(topObstacleAdvanced, posTop, Quaternion.identity);
                }
                else
                {
                    Instantiate(bottomObstacleAdvanced, posBottom, Quaternion.identity);
                }
                Instantiate(scoreLineAdvanced, posBottom, Quaternion.identity);
            }
            else
            {
                if (obstacleCount % 2 == 0)
                {
                    Instantiate(bottomObstacle, posBottom, Quaternion.identity);
                }
                else
                {
                    Instantiate(topObstacle, posTop, Quaternion.identity);
                }
                Instantiate(scoreLine, posBottom, Quaternion.identity);
            }

            obstacleCount += 1;
            yield return new WaitForSeconds(1.5f);
        }
        
    }
}
