using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    Props[] props;

    private void Awake()
    {
        props = FindObjectsOfType<Props>();
    }

}
