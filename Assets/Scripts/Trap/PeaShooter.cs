using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PeaShooter : Trap
{
    public GameObject[] bulletPrefabs;
    private int ShootCount = 0;
    public List<PeaShooterEvent> events;
    public float ShootSpeed;
    public Vector2 ShootDireciton;
    public Transform shootPos;
    public void Shoot(int index)
    {

        if (index < 0 || index >= bulletPrefabs.Length) return;
        ShootCount++;
        foreach (var e in events)
        {
            if (e.shootIndex == ShootCount)
            {
                e.shootEvent?.Invoke();
                return;
            }
        }
        ShootBullet(index);
    }
    public void ShootBullet(int index)
    {
        GameObject obj = ObjectPool.Instance.GetObject(bulletPrefabs[index]);
        Bullet b = obj.GetComponent<Bullet>();
        b.Set(ShootSpeed, ShootDireciton, 10);
        obj.transform.position = shootPos.transform.position;
    }
}
[System.Serializable]
public class PeaShooterEvent
{

    public int shootIndex;
    public UnityEvent shootEvent;
}