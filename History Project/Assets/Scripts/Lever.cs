using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Lever : MonoBehaviour
{
    [SerializeField] GameObject button;
    [SerializeField] GameObject hidenPlace;

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && button!=null)
        {
            button.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && button != null)
        {
            button.SetActive(false);
        }
    }

    public void PushButton()
    {
        animator.SetTrigger("ButtonePushed");
    }

    public void OpenHidenPlace()
    {
        Destroy(button);
        hidenPlace.SetActive(false);
    }
}
