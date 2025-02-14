using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeroGravity : MonoBehaviour
{
    public Rigidbody2D rb;
    public AudioClip music;
    public void SetZeroGravity()
    {
        Physics2D.gravity = Vector2.zero;
        rb.drag = 1;

        SoundManager.Instance.PlayMusic(music);
    }
    private void Update()
    {
        if (rb.transform.localScale.x < 2)
        {
            rb.transform.localScale += Vector3.one * Time.deltaTime *(2-rb.transform.localScale.x)*0.1f;
        }
    }
}
