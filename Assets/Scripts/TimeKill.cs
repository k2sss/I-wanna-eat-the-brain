using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeKill : MonoBehaviour
{
    public float killTime;
    private float timer;
    public bool isPoolObjectable;
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > killTime)
        {
            timer = 0;
            Kill();
        }
    }

    public void Kill()
    {
        if(!isPoolObjectable)
        Destroy(gameObject);
        else
            ObjectPool.Instance.PushObject(gameObject);
    }
    private void OnDisable()
    {
        timer = 0;
    }
}
