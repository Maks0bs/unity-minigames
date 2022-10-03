using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI powerupText;
    int score = 0;
    bool canMove = true;
    float powerupTimeLeft = 0.0f;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            if(powerupTimeLeft > 0.01f)
            {
                if (collision.gameObject.name != "Grass")
                {
                    Destroy(collision.gameObject);
                }
            }
            else
            {
                canMove = false;
                Invoke(nameof(RestartGame), 1f);
            }
            
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ScoreLine"))
        {
            score += 1;
            scoreText.text = $"{score}";
        }
        if (collision.CompareTag("Powerup"))
        {
            powerupTimeLeft += 10.0f;
            Destroy(collision.gameObject);
        }
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Update()
    {
        if (canMove && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * 150f);
            rb.angularVelocity = 0f;
            rb.SetRotation(45f);
            rb.AddTorque(-5f);
        }
        powerupTimeLeft = Mathf.Max(0.0f, powerupTimeLeft - Time.deltaTime);
        if (powerupTimeLeft > 0.01f)
        {
            powerupText.text = $"PowerUp: {powerupTimeLeft.ToString("F2")}";
        }
        else
        {
            powerupText.text = "";
        }
        
    }
}
