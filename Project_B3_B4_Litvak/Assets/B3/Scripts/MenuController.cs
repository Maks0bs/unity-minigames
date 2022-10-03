using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    
    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level1SceneB3");
    }

    public void LoadLevel2()
    {
        Debug.Log("Load level 2 (placeholder)");
    }
}
