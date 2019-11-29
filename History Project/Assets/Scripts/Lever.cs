using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Lever : MonoBehaviour
{
    [SerializeField] GameObject button;
    [SerializeField] GameObject hidenPlace;
    [SerializeField] float fadeTime = 2f;

    Animator animator;
    Tilemap hidenTilemap;
    Color color;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        hidenTilemap = hidenPlace.GetComponent<Tilemap>();
        color = hidenTilemap.color;
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
        if ( button != null)
        {
            button.SetActive(false);
        }
    }

    public void OpenHidenPlace()//animation event
    {
        Destroy(button);
        StartCoroutine(TileFade());
    }

    IEnumerator TileFade()
    {
        while (!Mathf.Approximately(color.a, 0))
        {
            color.a = Mathf.MoveTowards(color.a, 0f, Time.deltaTime / fadeTime);
            hidenTilemap.color = color;
            yield return null;
        }
        hidenPlace.SetActive(false);
    }
}
