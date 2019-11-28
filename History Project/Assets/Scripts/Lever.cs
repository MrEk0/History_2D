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
    //float alpha;
    Color color;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        hidenTilemap = hidenPlace.GetComponent<Tilemap>();
        //alpha = hidenPlace.GetComponent<Tilemap>().color.a;
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
        StartCoroutine(TileFade());
    }

    IEnumerator TileFade()
    {
        while (!Mathf.Approximately(color.a, 0))
        {
            //alpha = Mathf.MoveTowards(alpha, 0f, Time.deltaTime / fadeTime);
            color.a = Mathf.MoveTowards(color.a, 0f, Time.deltaTime / fadeTime);
            //hidenPlace.GetComponent<Tilemap>().color = new Color(color.r, color.g, color.b, alpha);
            hidenTilemap.color = color;
            yield return null;
        }
        hidenPlace.SetActive(false);
    }
}
