using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    public Action OnPress_A;
    public Action OnPress_D;
    public Action OnPressDown_Space;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            OnPress_A?.Invoke();
        }
        if (Input.GetKey(KeyCode.D))
        {
            OnPress_D?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            OnPressDown_Space?.Invoke();
        }
    }
}
