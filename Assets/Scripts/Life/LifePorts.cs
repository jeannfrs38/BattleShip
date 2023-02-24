using NavMeshPlus.Components;
using UnityEngine;

public class LifePorts : MonoBehaviour
{
    public SpriteRenderer sprite;
    [SerializeField] private Sprite[] lifes;
    [SerializeField] private int life;
    [SerializeField] bool receivedDamage;
    public NavMeshSurface surface;

    public GameObject effectExplosion;

    private GamerController _gamerController;
    void Start()
    {
        _gamerController = FindObjectOfType(typeof(GamerController)) as GamerController;
    }


    void Update()
    {
        if (_gamerController.start == true)
        {
            if (receivedDamage == true && _gamerController.currentShips[0] == null && _gamerController.currentShips[1] == null)
            {
                Damage(1);
            }
            else
            {
                receivedDamage = false;
            }
            if (life == 0)
            {
                Instantiate(effectExplosion, transform.position, transform.rotation);
                Instantiate(effectExplosion, new Vector3(transform.position.x + 2, transform.position.y, transform.position.z), transform.rotation);
                Instantiate(effectExplosion, new Vector3(transform.position.x + -1, transform.position.y, transform.position.z), transform.rotation);
                surface.BuildNavMesh();
                Destroy(gameObject, 0.3f);
            }
        }

    }
    //Metodo para receber dano
    public void Damage(int dano)
    {
        receivedDamage = false;
        if (life > 0)
        {
            life -= dano;
            sprite.sprite = lifes[life];

        }

    }
    // Colissoes
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            receivedDamage = true;
        }
    }
}
