using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] List<GameObject> spawnableLights;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Vector3 pos = new Vector3(
                Random.Range(-10f, 10f),
                Random.Range(-10f, 10f),
                0
            );
            int prefabIndex = Random.Range(0, spawnableLights.Count);
            Instantiate(spawnableLights[prefabIndex], pos, Quaternion.identity);
        }
    }
}
