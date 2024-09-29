using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DinoContoller : MonoBehaviour
{
    [SerializeField] private GameObject raptorPrefab;
    [SerializeField] private TextMeshPro dinoCountText;
    private readonly List<GameObject> raptors = new();

    private void Start()
    {
        raptors.Add(Instantiate(raptorPrefab, Vector3.zero, new Quaternion(0, 180, 0, 0), transform));

        InputManager.Instance.OnPress_A += MoveLeft;
        InputManager.Instance.OnPress_D += MoveRight;
        InputManager.Instance.OnPressDown_Space += AddRaptor;
        InputManager.Instance.OnPressDown_Backspace += RemoveRaptor;
    }

    private void Update()
    {
        if (raptors.Count <= 0)
        {
            dinoCountText.text = "0";
            return;
        }
        dinoCountText.text = raptors.Count.ToString();
        Run();
    }

    private void Run()
    {
        transform.Translate(Constants.MOVE_FRONT_SPEED * Time.deltaTime * Vector3.forward);
    }

    private void MoveLeft()
    {
        if (transform.position.x <= Constants.MOVE_DISTANCE * Vector3.left.x) return;
        // transform.Translate(Constants.MOVE_HORIZON_SPEED * Time.deltaTime * Vector3.left);
        transform.position += Constants.MOVE_HORIZON_SPEED * Time.deltaTime * Vector3.left;
    }

    private void MoveRight()
    {
        if (transform.position.x >= Constants.MOVE_DISTANCE * Vector3.right.x) return;
        // transform.Translate(Constants.MOVE_HORIZON_SPEED * Time.deltaTime * Vector3.right);
        transform.position += Constants.MOVE_HORIZON_SPEED * Time.deltaTime * Vector3.right;
    }

    private void AddRaptor()
    {
        GameObject raptor = Instantiate(raptorPrefab, Vector3.zero, new Quaternion(0, 180, 0, 0), transform);
        raptors.Add(raptor);
        SortRaptor();
    }

    private void RemoveRaptor()
    {
        if (raptors.Count < 1) return;
        GameObject targetRaptor = raptors[^1];
        raptors.Remove(targetRaptor);
        Destroy(targetRaptor);
        SortRaptor();
    }

    private void SortRaptor()
    {

        float angleStep = 360f / (raptors.Count > 9 ? 9 : raptors.Count) * Constants.DINO_SORT_RATIO;

        for (int i = 0; i < raptors.Count; i++)
        {
            if (i >= 9)
            {
                raptors[i].SetActive(false);
                continue;
            }
            float angle = i * angleStep;

            float angleRad = angle * Mathf.Deg2Rad;

            float x = Mathf.Cos(angleRad) * Constants.DINO_SORT_RADIUS;
            float z = Mathf.Sin(angleRad) * Constants.DINO_SORT_RADIUS;

            raptors[i].transform.localPosition = new Vector3(x, 0, z);
        }
    }
}
