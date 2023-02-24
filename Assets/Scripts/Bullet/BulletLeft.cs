using UnityEngine;
public class BulletLeft : Bullet
{
    //Mudar direcao da bala de canhao balas laterais
    private void Update()
    {
        Move(Vector2.left);
        Destroy(gameObject, 0.5f);
    }


}
