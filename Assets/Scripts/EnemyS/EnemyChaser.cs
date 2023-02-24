using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
public class EnemyChaser : MonoBehaviour
{
    [SerializeField] private int life;
    [SerializeField] private float speed;

    public Transform target;
    public Rigidbody2D rb;
    public CapsuleCollider2D colliderCaps;

    public NavMeshAgent agent;

    public GameObject effectExplosionChaser;

    [SerializeField] private bool ReceivedDamage;
    [SerializeField] private bool colliderPlayer;

    public SpriteRenderer spriteLife;
    public SpriteRenderer spriteShip;
    [SerializeField] private Sprite[] lifeSprite;
    [SerializeField] private Sprite[] shipSprite;



    private GamerController _gamerController;


    void Start()
    {
        _gamerController = FindObjectOfType(typeof(GamerController)) as GamerController;
        colliderCaps = GetComponent<CapsuleCollider2D>();

        if (_gamerController.start == true)
        {
            target = FindObjectOfType<Player>().transform;
            agent = GetComponent<NavMeshAgent>();
            spriteShip = GetComponent<SpriteRenderer>();
            rb = GetComponent<Rigidbody2D>();

            // para ultilizar o navMesh em 2d e nao ocorrer bugs
            agent.updateRotation = false;
            agent.updateUpAxis = false;
        }

    }


    void Update()
    {
        if (_gamerController.start == true)
        {
            if (life > 0)
            {
                if (colliderPlayer == false)
                {
                    //Movimentacao do inimigo chaser

                    //transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                    agent.SetDestination(target.transform.position);
                }
                if (ReceivedDamage == true)
                {
                    Damage(1);
                }
                if (colliderPlayer == true)
                {
                    colliderCaps.IsDestroyed();
                    Damage(life);
                    Instantiate(effectExplosionChaser, transform.position, transform.rotation);
                    colliderPlayer = false;
                    Destroy(gameObject, 1f);
                }
            }
            else
            {
                colliderCaps.IsDestroyed();
                Instantiate(effectExplosionChaser, transform.position, transform.rotation);
                Destroy(gameObject, 1f);
            }
        }


    }

    private void LateUpdate()
    {
        if (_gamerController.start == true)
        {
            Rotation();

        }
    }
    //Metodo para rotacionar inimigo mirando no player
    public void Rotation()
    {
        Vector2 lookdir = target.transform.position - transform.position;
        float angle = Mathf.Atan2(lookdir.y, lookdir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    //Recerber Dano
    public void Damage(int damage)
    {
        if (life > 0)
        {
            life -= damage;
            spriteLife.sprite = lifeSprite[life];
            spriteShip.sprite = shipSprite[life];
            ReceivedDamage = false;

        }
    }
    //Colissoes
    private void OnCollisionEnter2D(Collision2D collider)
    {

        if (collider.gameObject.CompareTag("Bullet"))
        {
            ReceivedDamage = true;
        }
        if (collider.gameObject.CompareTag("Player"))
        {
            colliderPlayer = true;
        }
    }
}
