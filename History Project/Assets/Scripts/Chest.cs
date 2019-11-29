using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] GameObject clutchButton;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            clutchButton.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        clutchButton.SetActive(false);

    }
}
