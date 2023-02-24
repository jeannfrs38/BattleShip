using UnityEngine;
public class BulletRight : Bullet
{
    //Balas laterais alterando a direcao
    private void Update()
    {
        Move(Vector2.right);
        Destroy(gameObject, 0.5f);
    }
}
