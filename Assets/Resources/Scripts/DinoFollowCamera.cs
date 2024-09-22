using UnityEngine;

public class DinoFollowCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Vector3 offset;

    private void Start()
    {
        offset = target.position - transform.position;
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, target.position.z - offset.z);
    }
}
