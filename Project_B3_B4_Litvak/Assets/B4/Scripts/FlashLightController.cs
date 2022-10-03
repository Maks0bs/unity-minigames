using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightController : MonoBehaviour
{
    [SerializeField] GameObject fpsController;
    [SerializeField] GameObject character;
    [SerializeField] AudioClip flashlightToggleSound;
    AudioSource audioSource;
    Light flashlight;
    void Awake()
    {
        audioSource = fpsController.GetComponent<AudioSource>();
        flashlight = GetComponent<Light>();
        flashlight.enabled = false;
    }

    void Update()
    {
        //transform.LookAt(character.transform);
        //transform.rotation = character.transform.rotation;
        if (Input.GetButtonDown("FlashLightToggle"))
        {
            audioSource.PlayOneShot(flashlightToggleSound, 1f);
            flashlight.enabled = !flashlight.enabled;
        }
    }
}
