using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuControllerB4 : MonoBehaviour
{

    public void LoadWorld1()
    {
        SceneManager.LoadScene("World1SceneB4");
    }

    public void LoadWorld2()
    {
        Debug.Log("Load world 2 (placeholder)");
    }
}
