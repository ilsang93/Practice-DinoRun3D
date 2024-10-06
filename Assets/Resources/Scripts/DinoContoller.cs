using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DinoContoller : MonoBehaviour
{
    [SerializeField] private GameObject raptorPrefab;
    [SerializeField] private TextMeshPro dinoCountText;
    [SerializeField] private Transform colPos;
    public Vector3 ColPos
    {
        get
        {
            return colPos.position;
        }
    }
    [SerializeField] private float colRadius;
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
        DoorCheck();
    }

    private void DoorCheck()
    {
        Collider[] hitCols = Physics.OverlapSphere(ColPos, colRadius);
        foreach (Collider col in hitCols)
        {
            if (col.CompareTag("Door"))
            {
                col.GetComponent<SelectDoors>().Excute(this, transform.position.x > 0);
            }

            if (col.CompareTag("Goal")) {
                print("골인");
            }
        }
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

    public void AddRaptor()
    {
        if (raptors.Count > Constants.MAX_DINO_COUNT)
        {
            raptors.Add(null);
            return;
        }
        GameObject raptor = Instantiate(raptorPrefab, Vector3.zero, new Quaternion(0, 180, 0, 0), transform);
        raptors.Add(raptor);
        SortRaptor();
    }

    public void TimesRaptor(int param)
    {
        int originCount = raptors.Count;
        int targetCount = raptors.Count * param;

        for (int i = originCount; i < targetCount; i++)
        {
            if (raptors.Count > Constants.MAX_DINO_COUNT)
            {
                raptors.Add(null);
                continue;
            }
            GameObject raptor = Instantiate(raptorPrefab, Vector3.zero, new Quaternion(0, 180, 0, 0), transform);
            raptors.Add(raptor);
        }
        SortRaptor();
    }

    public void RemoveRaptor()
    {
        if (raptors.Count < 1) return;
        GameObject targetRaptor = raptors[^1];
        raptors.Remove(targetRaptor);
        Destroy(targetRaptor);
        SortRaptor();
    }

    public void DivisionRaptor(int param)
    {
        if (raptors.Count < 1) return;
        int originCount = raptors.Count;
        int targetCount = raptors.Count / param;

        for (int i = 0; i < originCount - targetCount; i++)
        {
            GameObject targetRaptor = raptors[^1];
            raptors.Remove(targetRaptor);
            Destroy(targetRaptor);
        }
        SortRaptor();
    }

    private void SortRaptor()
    {

        float angleStep = 360f / (raptors.Count > 9 ? 9 : raptors.Count) * Constants.DINO_SORT_RATIO;

        for (int i = 0; i < raptors.Count; i++)
        {
            if (i >= Constants.MAX_DINO_COUNT)
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(ColPos, colRadius);
    }
}
