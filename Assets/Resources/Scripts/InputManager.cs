using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance { get; private set; }
    public Action OnPress_A;
    public Action OnPress_D;
    public Action OnPressDown_Space;
    public Action OnPressDown_Backspace;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
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
        if (Input.GetKeyDown(KeyCode.Backspace)) {
            OnPressDown_Backspace?.Invoke();
        }
    }
}
