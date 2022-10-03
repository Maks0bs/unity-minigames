using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static float PLAYER_SPEED = 5.0f;
    public static float PLAYER_SIDE_SPEED = 5.0f;
    public static float PLAYER_JUMP_FORCE = 6.0f;
    public static float DASH_MULTIPLIER = 2.0f;
    public static float FAIL_Y_THRESHOLD = -30.0f;

    Rigidbody rb;
    HashSet<GameObject> collidesWithSet = new HashSet<GameObject>();
    float speedZ = PLAYER_SPEED;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.forward * PLAYER_SPEED;
    }

    private void OnCollisionEnter(Collision collision)
    {
        collidesWithSet.Add(collision.gameObject);
    }

    private void OnCollisionExit(Collision collision)
    {
        collidesWithSet.Remove(collision.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speedZ);
        //rb.position += Vector3.forward * PLAYER_SPEED;
        if (rb.position.y < FAIL_Y_THRESHOLD)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetButtonDown("BackToMenu"))
        {
            SceneManager.LoadScene("MainSceneB3");
        }

        float h = Input.GetAxisRaw("HorizontalMovement");
        rb.AddForce(Vector3.right * h * PLAYER_SIDE_SPEED);

        if (Input.GetButtonDown("Dash"))
        {
            speedZ *= DASH_MULTIPLIER;
            rb.velocity *= DASH_MULTIPLIER;
        }
        if (Input.GetButtonUp("Dash"))
        {
            speedZ /= DASH_MULTIPLIER;
            rb.velocity /= DASH_MULTIPLIER;
        }

        if (Input.GetButtonDown("Jump") && (collidesWithSet.Count > 0))
        {
            rb.AddForce(Vector3.up * PLAYER_JUMP_FORCE, ForceMode.Impulse);
        }
    }
}
