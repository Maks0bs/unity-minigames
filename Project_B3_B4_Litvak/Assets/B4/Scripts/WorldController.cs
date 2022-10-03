using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;

public class WorldController : MonoBehaviour
{
    [SerializeField] List<Material> skyboxes;
    [SerializeField] GameObject character;
    Skybox skyboxComponent;
    int index = 0;


    private void Awake()
    {
        skyboxComponent = character.GetComponent<Skybox>();
    }
    void Update()
    {
        if (Input.GetButtonDown("BackToMenu"))
        {
            SceneManager.LoadScene("MainSceneB4");
        }
        if (Input.GetButtonDown("SkyboxChange"))
        {
            index = (index + 1) % skyboxes.Count;
            skyboxComponent.material = skyboxes[index];
        }
    }
}
