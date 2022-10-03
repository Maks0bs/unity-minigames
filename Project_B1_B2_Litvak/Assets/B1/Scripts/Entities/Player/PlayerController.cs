using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    float PLAYER_SPEED = 10.0f;

    Rigidbody2D rb;
    SpriteRenderer sr;
    AudioSource audioSource;
    [SerializeField] AudioClip basicShootSound;
    [SerializeField] GameObject audioHolder;
    [SerializeField] AudioClip specialShootSound;
    [SerializeField] AudioClip destroySound;
    [SerializeField] GameObject destroyParticle;
    [SerializeField] GameObject basicLaser;
    [SerializeField] GameObject specialLaser;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyProjectile"))
        {
            audioSource.mute = true;
            GameObject ah = Instantiate(audioHolder);
            AudioSource audioSourceTemp = ah.GetComponent<AudioSource>();
            audioSourceTemp.PlayOneShot(destroySound, 0.5f);
            Instantiate(destroyParticle, rb.position, Quaternion.identity);
            Destroy(collision.gameObject);
            sr.enabled = false;
            StartCoroutine(RestartGame(1.5f));
        }
    }

    IEnumerator RestartGame(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Update()
    {
        float v = Input.GetAxisRaw("VerticalMovement");
        Vector2 dir = Vector2.up * v * Time.deltaTime * PLAYER_SPEED;
        rb.MovePosition(rb.position + dir);
       
        if (Input.GetButtonDown("FireBasicLaser"))
        {
            Vector2 pos = rb.position + new Vector2(sr.bounds.size.x * 0.5f, 0f);
            Instantiate(basicLaser, pos, Quaternion.Euler(0, 0, -90f));
            audioSource.PlayOneShot(basicShootSound, 0.5f);
        }

        if (Input.GetButtonDown("FireSpecialLaser"))
        {
            Vector2 pos = rb.position + new Vector2(sr.bounds.size.x * 0.5f, 0f);
       
            GameObject laser = Instantiate(specialLaser, pos, Quaternion.Euler(0, 0, -90f));
            PlayerLaserSpecialController plsc = laser.GetComponent<PlayerLaserSpecialController>();
            plsc.amplitude = 2f;
            plsc.speed = 3.0f;

            laser = Instantiate(specialLaser, pos, Quaternion.Euler(0, 0, -90f));
            plsc = laser.GetComponent<PlayerLaserSpecialController>();
            plsc.amplitude = 2f;
            plsc.sinAngle = Mathf.PI;
            plsc.speed = 3.0f;

            laser = Instantiate(specialLaser, pos, Quaternion.Euler(0, 0, -90f));
            plsc = laser.GetComponent<PlayerLaserSpecialController>();
            plsc.amplitude = 0.9f;
            plsc.speed = 5.0f;

            laser = Instantiate(specialLaser, pos, Quaternion.Euler(0, 0, -90f));
            plsc = laser.GetComponent<PlayerLaserSpecialController>();
            plsc.amplitude = 0.9f;
            plsc.sinAngle = Mathf.PI;
            plsc.speed = 5.0f;

            audioSource.PlayOneShot(specialShootSound, 0.5f);
        }

    }
}
