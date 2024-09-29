using UnityEngine;

public class Map : MonoBehaviour
{
    public Vector3 mapSize;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public float GetMapSize()
    {
        return mapSize.z;
    }
}
