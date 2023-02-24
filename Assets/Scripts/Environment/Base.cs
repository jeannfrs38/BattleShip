using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private int life;
    [SerializeField] private bool receivedDamage;

    //Sprite de vida
    [SerializeField] private Sprite[] lifeSprite;
    public SpriteRenderer spriteLife;

    public GameObject effectExplosion;

    public GamerController _gamerController;
    void Start()
    {

        _gamerController = FindObjectOfType(typeof(GamerController)) as GamerController;
    }

    void Update()
    {
        if (_gamerController.start == true)
        {
            if (life > 0)
            {
                if (receivedDamage == true)
                {
                    Damage(1);
                }
            }
            else
            {
                _gamerController.Win();
                Destroy(gameObject, 0.5f);
            }
        }
    }
    //Metodo  para receber dano
    public void Damage(int damage)
    {
        if (life > 0)
        {
            life -= damage;
            spriteLife.sprite = lifeSprite[life];
            receivedDamage = false;
        }
    }
    //Colisoes
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            receivedDamage = true;

        }
    }
}
