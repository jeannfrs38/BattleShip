using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float speedRotation;
    [SerializeField] private int life;

    [SerializeField] private bool receivedDamage;
    [SerializeField] private bool collisionChaser;
    [SerializeField] private bool collisionLife;

    // Variaveis para executar o tiro
    [SerializeField] private Transform[] aims;
    [SerializeField] private GameObject[] bullet;
    public GameObject effectExplosion;

    //Sprites life e ship
    public SpriteRenderer spriteRship;
    public SpriteRenderer spriteRlife;
    [SerializeField] private Sprite[] shipSprite;
    [SerializeField] private Sprite[] lifeSprite;

    private GamerController _gamerController;



    private void Start()
    {
        _gamerController = FindObjectOfType(typeof(GamerController)) as GamerController;

    }
    private void Update()
    {
        if (_gamerController.start == true)
        {
            if (life > 0)
            {
                //Movimentacao
                if (Input.GetKey(KeyCode.UpArrow))
                {

                    transform.Translate(Vector3.up * speed * Time.deltaTime);

                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {

                    transform.Rotate(Vector3.forward * speedRotation * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {

                    transform.Rotate(Vector3.forward * -speedRotation * Time.deltaTime);
                }
                //Tiro frontal
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    AtaqueFront();

                }
                //Tiro lateral
                if (Input.GetKeyDown(KeyCode.X))
                {
                    AtaqueRight();
                    AtaqueLeft();
                }

                if (collisionChaser == true || receivedDamage == true)
                {
                    Instantiate(effectExplosion, transform.position, transform.rotation);
                    collisionChaser = false;
                    receivedDamage = false;
                    Damage(1);

                }
                if (collisionLife == true)
                {
                    Healt(1);
                    collisionLife = false;
                }

            }

            else
            {

                Instantiate(effectExplosion, new Vector3(transform.position.x, transform.position.y + -0.5f, transform.position.z), transform.rotation);
                Instantiate(effectExplosion, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), transform.rotation);
                Destroy(gameObject, 0.5f);
                _gamerController.GameOver();
            }
        }


    }
    //Metodos para executar os tiros
    public void AtaqueFront()
    {
        GameObject bullet1 = Instantiate(bullet[0], aims[0].position, transform.rotation);

    }
    public void AtaqueRight()
    {
        GameObject bullet1 = Instantiate(bullet[1], aims[1].position, transform.rotation);
        GameObject bullet2 = Instantiate(bullet[1], aims[2].position, transform.rotation);
        GameObject bullet3 = Instantiate(bullet[1], aims[3].position, transform.rotation);

    }
    public void AtaqueLeft()
    {
        GameObject bullet1 = Instantiate(bullet[2], aims[4].position, transform.rotation);
        GameObject bullet2 = Instantiate(bullet[2], aims[5].position, transform.rotation);
        GameObject bullet3 = Instantiate(bullet[2], aims[6].position, transform.rotation);

    }
    //Metodo para receber dano
    public void Damage(int damage)
    {
        if (life > 0)
        {
            life -= damage;
            spriteRlife.sprite = lifeSprite[life];
            spriteRship.sprite = shipSprite[life];

        }
    }

    //Metodo para receber vida
    public void Healt(int healt)
    {
        if (life >= 1 && life < 5)
        {
            life += healt;
            spriteRlife.sprite = lifeSprite[life];
            spriteRship.sprite = shipSprite[life];

        }
    }

    // Collisoes
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chaser"))
        {
            collisionChaser = true;
        }
        if (collision.gameObject.CompareTag("BulletEnemy"))
        {
            receivedDamage = true;
        }
        if (collision.gameObject.CompareTag("Life"))
        {
            collisionLife = true;
        }
    }



}
