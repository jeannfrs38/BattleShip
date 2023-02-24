using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10);

    private GamerController _gamerController;


    private void Start()
    {
        _gamerController = FindObjectOfType(typeof(GamerController)) as GamerController;
    }
    void Update()
    {
        //Seguir o player 
        if (_gamerController.start == true)
        {
            transform.position = player.transform.position + offset;
        }

    }
}
