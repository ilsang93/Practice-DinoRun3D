using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] private GameObject[] mapPrefabs;
    [SerializeField] private int mapLength = 5;
    private float nowLength = -5;

    private void Start()
    {
        CreateMap();
    }

    private void CreateMap()
    {
        for (int i = 0; i < mapLength; i++)
        {
            int targetIdx = Random.Range(0, 3);
            targetIdx = targetIdx == 3 ? 2 : targetIdx;
            float targetPos;
            targetPos = nowLength + mapPrefabs[targetIdx].GetComponent<Map>().mapSize.z / 2;
            GameObject tempObj = Instantiate(mapPrefabs[targetIdx], new Vector3(0, 0, targetPos), Quaternion.identity, transform);
            switch (targetIdx)
            {
                case 0:
                    tempObj.GetComponentInChildren<MeshRenderer>().material.color = Color.red;
                    break;
                case 1:
                    tempObj.GetComponentInChildren<MeshRenderer>().material.color = Color.green;
                    break;
                case 2:
                    tempObj.GetComponentInChildren<MeshRenderer>().material.color = Color.blue;
                    break;
            }
            nowLength += mapPrefabs[targetIdx].GetComponent<Map>().mapSize.z;
        }
    }
}
