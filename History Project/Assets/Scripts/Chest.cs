using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] GameObject clutchButton;

    //public GameObject player { get; set; }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            clutchButton.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            clutchButton.SetActive(false);
        }
    }

    //private void Update()
    //{
    //    if (player)
    //        transform.position = player.transform.position;
    //}
}
