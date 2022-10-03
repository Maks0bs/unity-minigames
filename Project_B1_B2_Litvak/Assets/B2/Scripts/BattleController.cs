using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleController : MonoBehaviour
{
    GameObject enemy;
    Animator anim;
    void Start()
    {
        enemy = Instantiate(CrossSceneData.battleSceneEnemy, new Vector3(0, 2.0f, 0), Quaternion.identity);
        anim = enemy.GetComponent<Animator>();
    }

    public void OnHitClick()
    {
        anim.SetTrigger("hit");
    }

    public void OnAttackClick()
    {
        anim.SetTrigger("attack");
    }

    public void OnSpecialClick()
    {
        anim.SetTrigger("special");
    }

    public void OnRunClick()
    {
        SceneManager.LoadScene("MainSceneB2");
    }
}
