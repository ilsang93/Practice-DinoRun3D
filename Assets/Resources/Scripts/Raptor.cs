using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raptor : MonoBehaviour
{
    private bool isTarget = false;
    public bool IsTarget
    {
        get
        {
            return isTarget;
        }
        set
        {
            isTarget = value;
        }
    }
}
