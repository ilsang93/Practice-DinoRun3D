using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    enum State
    {
        Idle, // 대기상태 - 애니메이션 정지
        Run // Dino에게 달려온다 - 애니메이션 작동
    }

    private Action OnStateChange;
    private Animator animator;
    private State enemyState = State.Idle;
    private GameObject target;

    [SerializeField] private float detectRadius;
    [SerializeField] private float speed;

    void Start()
    {
        animator = GetComponent<Animator>();
        SetState(State.Idle);
    }

    void Update()
    {
        DetectDino();
        GoToDino();
    }

    private void SetState(State state)
    {
        enemyState = state;
        switch (enemyState)
        {
            case State.Idle:
                animator.speed = 0f;
                break;
            case State.Run:
                animator.speed = 1f;
                break;
            default:
                break;
        }
        OnStateChange?.Invoke();
    }

    private void DetectDino() // Dino를 찾는 함수. 항상 Update에서 작동되고 있다.
    {
        if (target != null) return;

        Collider[] hitCols = Physics.OverlapSphere(transform.position, detectRadius);
        foreach (Collider col in hitCols)
        {
            if (col.CompareTag("Raptor"))
            {
                if (!col.GetComponent<Raptor>().IsTarget)
                {
                    target = col.gameObject;
                    col.GetComponent<Raptor>().IsTarget = true;
                    StartGoToDino();
                }
            }
        }
    }

    private void StartGoToDino() // 찾았을 때 작동하는 함수 Dino에게 달려드는 함수
    {
        SetState(State.Run);
    }

    private void GoToDino()
    {
        if (enemyState != State.Run) return;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        if (Vector3.Distance(target.transform.position, transform.position) <= 0.5f)
        {
            target.GetComponent<Raptor>().IsTarget = false;
            SoundManager.instance.PlaySfxOneshot(SfxEnum.DinoDie);
            target.transform.parent.GetComponent<DinoContoller>().RemoveRaptor();
            Destroy(gameObject);
        }
    }
}
