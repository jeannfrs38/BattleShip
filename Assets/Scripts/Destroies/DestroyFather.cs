using UnityEngine;

public class DestroyFather : MonoBehaviour
{
    public GameObject child;

    void Update()
    {
        if (child == null)
        {
            Destroy(gameObject);
        }
    }
}
