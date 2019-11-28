using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Props : MonoBehaviour
{
    [SerializeField] GameObject Score;
    [SerializeField] GameObject game;
    [SerializeField] int points = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            game.GetComponent<WinCondition>().DestroyProps();
            Score.GetComponent<Score>().IncreaseScore(points);
        }
    }
}
