using UnityEngine;

public class DestroyEffect : MonoBehaviour
{

    public void Finish()
    {
        Destroy(gameObject, 0.2f);
    }
}
