using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    public GameObject efectBullet;

    private void Update()
    {
        Move(Vector2.up);
        Destroy(gameObject, 0.6f);
    }
    //Movimentacao da bala de canhao
    public void Move(Vector2 direction)
    {
        transform.Translate(direction * speed * Time.deltaTime);

    }

    //Colisoes
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(efectBullet, transform.position, transform.rotation);
        Destroy(gameObject);

    }
}
