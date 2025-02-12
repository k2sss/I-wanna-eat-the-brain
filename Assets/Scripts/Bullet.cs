using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public Vector2 direction;
    public float duration;
    private float timer;
    public virtual void Set(float speed, Vector2 direciton, float duration)
    {
        this.speed = speed;
        this.direction = direciton;
        this.duration = duration;
        timer = 0;
    }
   

    // Update is called once per frame
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > duration)
        {
            timer = 0;
            ObjectPool.Instance.PushObject(gameObject);
        }
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
