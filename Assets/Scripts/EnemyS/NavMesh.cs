using NavMeshPlus.Components;
using UnityEngine;

public class NavMesh : MonoBehaviour
{
    public NavMeshSurface surface;
    private GamerController _gamerController;
    public bool surfaceAtive;
    void Start()
    {
        _gamerController = FindObjectOfType(typeof(GamerController)) as GamerController;
        surface = GetComponent<NavMeshSurface>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_gamerController.ports[0] == null || _gamerController.ports[1] == null)
        {
            surfaceAtive = true;
        }
        if (surfaceAtive == true)
        {
            surface.BuildNavMesh();
            surfaceAtive = false;
        }
    }
}
