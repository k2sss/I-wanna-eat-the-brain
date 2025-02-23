using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UmbreallaLeaf : Trap
{
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 100);
        }
    }
}
