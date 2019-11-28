using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchAndThrow : MonoBehaviour
{
    [SerializeField] Transform hand;
    [SerializeField] float force = 10f;


    GameObject rubble;

    private void Update()
    {
        Throw();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (rubble != null)
            return;

        if (collision.CompareTag("Rock"))
        {
            rubble = collision.gameObject;
            rubble.transform.position = hand.position;
            rubble.transform.parent = hand;          
        }
    }

    private void Throw()
    {
        if (rubble == null)
            return;

        if(Input.GetMouseButtonDown(0))
        {
            rubble.AddComponent<Rigidbody2D>();
            rubble.GetComponent<Rigidbody2D>().gravityScale = 0f;
            rubble.transform.parent = null;
            StartCoroutine(ThrowTheRubble());
        }
    }

    IEnumerator ThrowTheRubble()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Rock"), LayerMask.NameToLayer("Default"), true);
        rubble.GetComponent<Rigidbody2D>().velocity = new Vector2(force*transform.localScale.x, 0f);
        rubble.GetComponent<Rubble>().isThrown = true;
        yield return new WaitForSeconds(1f);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Rock"), LayerMask.NameToLayer("Default"), false);
    }
}
