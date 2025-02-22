using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeroGravity : MonoBehaviour
{
    public Rigidbody2D rb;
    public AudioClip music;
    public void SetZeroGravity()
    {
        g = Physics2D.gravity;
        Physics2D.gravity = Vector2.zero;
        rb.drag = 1;

        SoundManager.Instance.PlayMusic(music);

        enabled=true;

        col.enabled = false;
        col2.enabled = false;
    }

    public float timmer = 36.5f;

    Vector2 g;

    private void Update()
    {
        timmer -= Time.deltaTime;
        if (timmer < 0)
        {
            col.enabled = true;
            enabled = false;
            Physics2D.gravity = g;
        }
        if (rb.transform.localScale.x < 3.5f)
        {
            rb.transform.localScale += Vector3.one * Time.deltaTime *(3.5f-rb.transform.localScale.x)*0.1f;
        }
    }

    public Collider2D col,col2;


}
