using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeKill : MonoBehaviour
{
    public float killTime;
    public bool isPoolObjectable;
    void Start()
    {
        Invoke(nameof(Kill), killTime);
    }

    public void Kill()
    {
        if(!isPoolObjectable)
        Destroy(gameObject);
        else
            ObjectPool.Instance.PushObject(gameObject);
    }
}
