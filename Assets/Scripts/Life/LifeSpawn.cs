using UnityEngine;

public class LifeSpawn : MonoBehaviour
{

    [SerializeField] private bool collisionPlayer;
    private GamerController _gamerController;
    private void Start()
    {
        _gamerController = FindObjectOfType(typeof(GamerController)) as GamerController;
    }
    private void Update()
    {
        if (_gamerController.start == true)
        {
            if (collisionPlayer == true)
            {
                Destroy(gameObject);
                collisionPlayer = false;
            }
            else
            {
                Destroy(gameObject, 10f);
            }
        }
    }
    //Deteccao de colisao
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collisionPlayer = true;
        }
    }
}
