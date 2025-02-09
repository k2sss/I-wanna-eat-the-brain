using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulet_Explode : Bullet
{
    public GameObject ExplodeObject;
    public AudioClip explodeSound;
    public float explodeTime;

    public override void Set(float speed, Vector2 direciton, float duration)
    {
        base.Set(speed, direciton, duration); 
        Invoke(nameof(Explode), explodeTime);
    }
    public void Explode()
    {
        SoundManager.Instance.PlaySound(explodeSound);
        ObjectPool.Instance.PushObject(gameObject);
        GameObject go = Instantiate(ExplodeObject);
        go.transform.position = transform.position;
    }
}
