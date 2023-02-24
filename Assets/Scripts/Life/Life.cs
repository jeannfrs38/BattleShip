using UnityEngine;

public class Life : MonoBehaviour
{
    public GameObject enemy;
    [SerializeField] private Vector3 offset;
    private GamerController _gamerController;
    void Start()
    {
        _gamerController = FindObjectOfType(typeof(GamerController)) as GamerController;
    }


    void Update()
    {
        if (_gamerController.start == true)
        {
            transform.position = enemy.transform.position + offset;
        }


    }
}
