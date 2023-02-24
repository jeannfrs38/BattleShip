using System.Collections;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    enum State
    {
        Patrolling,
        Stopped,
        Rotate,
        Ataque,
    }
    [SerializeField]
    State state;

    [SerializeField] private bool collisionPlayer;
    [SerializeField] private bool collisionWall;
    [SerializeField] private bool ReceivedDamage;
    [SerializeField] private bool death;


    public Rigidbody2D rb;

    public SpriteRenderer spriteLife;
    public SpriteRenderer spriteShip;
    [SerializeField] private Sprite[] lifeSprite;
    [SerializeField] private Sprite[] shipSprite;

    public GameObject direction;
    public GameObject bullet;
    public GameObject target;
    public GameObject effectExplosion;



    [SerializeField] private int life;

    [SerializeField] private float coldown;
    [SerializeField] private float startRotation;
    [SerializeField] private float durationPatrolling;
    [SerializeField] private float startDuration;
    [SerializeField] private float startDurationAtack;
    [SerializeField] private float durationAtack;

    private GamerController _gamerController;





    void Start()
    {

        _gamerController = FindObjectOfType(typeof(GamerController)) as GamerController;
        rb = GetComponent<Rigidbody2D>();
        spriteShip = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (_gamerController.start == true)
        {
            if (life > 0)
            {
                death = false;
                // Maquina de estado do Shooter
                switch (state)
                {
                    default:
                    case State.Stopped:
                        if (collisionPlayer == true)
                        {
                            state = State.Ataque;
                            durationAtack = 0;
                        }
                        else
                        {
                            startRotation = Time.time;
                            state = State.Rotate;
                        }
                        break;
                    case State.Patrolling:

                        if (Time.time >= durationPatrolling + startDuration || collisionWall == true)
                        {
                            state = State.Stopped;
                        }
                        if (collisionPlayer == true)
                        {
                            state = State.Stopped;
                        }
                        else
                        {
                            if (transform.rotation.z >= -90)
                            {
                                transform.Translate(Vector3.up * Time.deltaTime * 2);
                            }
                            if (transform.rotation.z >= 90)
                            {
                                transform.Translate(Vector3.down * Time.deltaTime * 2);
                            }

                        }
                        break;

                    case State.Rotate:

                        if (Time.time >= coldown + startRotation)
                        {

                            state = State.Patrolling;
                            startDuration = Time.time;
                            collisionWall = false;

                        }
                        if (collisionPlayer == true)
                        {
                            state = State.Stopped;
                        }
                        else
                        {
                            transform.Rotate(Vector3.back * Time.deltaTime * 45);
                        }
                        break;

                    case State.Ataque:
                        collisionWall = false;

                        Rotation();

                        if (Time.time >= startDurationAtack + durationAtack)
                        {

                            Instantiate(bullet, direction.transform.position, transform.rotation);
                            durationAtack = Time.time;
                        }
                        break;
                }
                if (ReceivedDamage == true)
                {
                    Damage(1);
                }
            }
            else if (life <= 0)
            {

                Death();
            }

        }

    }
    // Rotacionar olhado para o Player
    public void Rotation()
    {
        Vector2 lookdir = target.transform.position - transform.position;
        float angle = Mathf.Atan2(lookdir.y, lookdir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    //Metodo para receber dano
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
    //Metodo para quando morrer
    public void Death()
    {
        Instantiate(effectExplosion, new Vector3(transform.position.x, transform.position.y + -0.5f, transform.position.z), transform.rotation);
        Instantiate(effectExplosion, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), transform.rotation);
        Destroy(gameObject, 1f);
    }

    //Coroutina para pegar o player quando entra no range mas nao pegar automaticamente
    IEnumerator GetPlayer(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        collisionPlayer = true;
    }
    //Coroutina para perde  target do player quando sair do range mas nao sair automaticamente
    IEnumerator LoserPlayer(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        collisionPlayer = false;
        state = State.Stopped;
    }

    //Deteccao de colisao
    private void OnCollisionEnter2D(Collision2D collider)
    {
        collisionWall = true;
        if (collider.gameObject.CompareTag("Bullet"))
        {
            ReceivedDamage = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(GetPlayer(0.5f));
            target = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(LoserPlayer(0.2f));
        }
    }

}

