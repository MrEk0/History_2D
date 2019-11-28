using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    [SerializeField] GameObject winPanel;
    Props[] props;
    int propsCount;

    private void Awake()
    {
        props = FindObjectsOfType<Props>();
        propsCount = props.Length;
    }

    private void Update()
    {
        if(propsCount==0)
        {
            Time.timeScale = 0f;
            winPanel.SetActive(true);
        }
    }

    public void DestroyProps()
    {
        propsCount--;
    }

}
