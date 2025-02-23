using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfRotate : MonoBehaviour
{
    public float RotateSpeed;
    private void Update()
    {
        transform.Rotate(new Vector3 (0, 0, RotateSpeed)*Time.deltaTime);
    }
}
