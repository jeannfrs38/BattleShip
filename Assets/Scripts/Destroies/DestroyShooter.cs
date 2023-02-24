using UnityEngine;

public class DestroyShooter : MonoBehaviour
{
    public GameObject child;
    public GameObject life;

    void Update()
    {
        if (child == null)
        {
            Instantiate(life, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
