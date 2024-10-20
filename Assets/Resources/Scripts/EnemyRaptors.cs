using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyRaptors : MonoBehaviour
{
    [SerializeField] private GameObject enemyRaptorPrefab;
    [SerializeField] private TextMeshPro countText;

    private List<GameObject> enemies = new();


    void Start()
    {
        CreateEnemyRaptors();
    }

    void Update()
    {
        if (transform.childCount <= 1)
        {
            Destroy(gameObject);
        }
    }

    private void CreateEnemyRaptors()
    {
        float enemyRaptorNumber = Random.Range(1, Constants.MAX_ENEMY_COUNT);

        for (int i = 0; i < enemyRaptorNumber; i++)
        {
            enemies.Add(Instantiate(enemyRaptorPrefab, transform));
        }
        
        countText.text = enemyRaptorNumber.ToString();

        if (enemies.Count == 1)
        {
            enemies[0].transform.localPosition = Vector3.zero;
            return;
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            if (i >= Constants.MAX_ENEMY_COUNT)
            {
                continue;
            }
            float currentRadius = Constants.DINO_SORT_RADIUS_INITIAL + (Constants.DINO_SORT_RADIUS_GROWTH * i * Constants.DINO_SORT_RADIUS_MULTIPLIER);
            float currentAngle = Constants.DINO_SORT_ANGLE_INCREMENT * i;

            float x = Mathf.Cos(currentAngle * Mathf.Deg2Rad) * currentRadius;
            float z = Mathf.Sin(currentAngle * Mathf.Deg2Rad) * currentRadius;

            enemies[i].transform.localPosition = new Vector3(x, 0, z);
        }


    }
}
