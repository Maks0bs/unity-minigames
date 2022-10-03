using System.Collections;
using UnityEngine;

public class AutoDestroyBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyGameObject(gameObject, 5.0f));
    }

    IEnumerator DestroyGameObject(GameObject o, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(o);
    }

}
