using UnityEngine;
using UnityEngine.SceneManagement;

public class SlimeController : MonoBehaviour
{
    bool playerInRange = false;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerInRange = false;
    }



    private void Update()
    {
        if (Input.GetButtonDown("SlimeInteract") && playerInRange)
        {
            CrossSceneData.mainScenePlayerX = player.transform.position.x;
            CrossSceneData.mainScenePlayerY = player.transform.position.y;
            CrossSceneData.battleSceneEnemy = enemy;
            
            SceneManager.LoadScene("BattleScene");
        }
    }
}
