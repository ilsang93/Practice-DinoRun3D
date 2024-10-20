using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] private StageSO[] Stages;
    [SerializeField] private GameObject[] mapPrefabs;
    [SerializeField] private int mapLength = 5;
    [SerializeField] private GameObject doorPrefab;
    [SerializeField] private GameObject enemiesPrefab;
    [SerializeField] private Transform doorsParents;
    [SerializeField] private Transform enemiesParents;
    [SerializeField] private float doorsOffset;
    [SerializeField] private float enemiesOffset;
    [SerializeField] private GameObject goalPrefab;
    private float nowLength = -5;

    private void Start()
    {
        mapLength += PlayerPrefs.GetInt("Stage") + 1;
        CreateMap();
    }

    // private void CreateStage()
    // {
    //     int currentStageIndex = GameManager.instance.GetStage();

    //     StageSO stage = Stages[currentStageIndex];
    //     CreateMap(stage.maps);
    // }


    private void CreateMap(Map[] maps)
    {
        for (int i = 0; i < maps.Length; i++)
        {
            int targetIdx = Random.Range(0, 3);
            targetIdx = targetIdx == 3 ? 2 : targetIdx;
            float targetPos;
            targetPos = nowLength + maps[targetIdx].mapSize.z / 2;
            Map tempObj = Instantiate(maps[targetIdx], new Vector3(0, 0, targetPos), Quaternion.identity, transform);
            // switch (targetIdx)
            // {
            //     case 0:
            //         tempObj.GetComponentInChildren<MeshRenderer>().material.color = Color.red;
            //         break;
            //     case 1:
            //         tempObj.GetComponentInChildren<MeshRenderer>().material.color = Color.green;
            //         break;
            //     case 2:
            //         tempObj.GetComponentInChildren<MeshRenderer>().material.color = Color.blue;
            //         break;
            // }
            nowLength += maps[targetIdx].mapSize.z;
        }
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
            // switch (targetIdx)
            // {
            //     case 0:
            //         tempObj.GetComponentInChildren<MeshRenderer>().material.color = Color.red;
            //         break;
            //     case 1:
            //         tempObj.GetComponentInChildren<MeshRenderer>().material.color = Color.green;
            //         break;
            //     case 2:
            //         tempObj.GetComponentInChildren<MeshRenderer>().material.color = Color.blue;
            //         break;
            // }
            nowLength += mapPrefabs[targetIdx].GetComponent<Map>().mapSize.z;
        }

        GameManager.instance.SetTotalDistance(nowLength);

        int doorCount = (int)(nowLength / doorsOffset);
        for (int i = 0; i < doorCount; i++)
        {
            Instantiate(doorPrefab, new Vector3(0, 0, (i + 1) * doorsOffset), Quaternion.identity, doorsParents);
        }

        int enemyCount = (int)(nowLength / enemiesOffset);
        for (int i = 0; i < enemyCount; i++)
        {
            Instantiate(enemiesPrefab, new Vector3(0, 0, (i + 1) * enemiesOffset), Quaternion.identity, enemiesParents);
        }

        Instantiate(goalPrefab, new Vector3(0, 0, nowLength), Quaternion.identity, doorsParents);
    }
}
