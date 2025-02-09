using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
namespace Undertale
{
    public class UTPeashooter : MonoBehaviour
    {
       public void Hurt()
        {
            Vector2 originPos = transform.position;
            transform.DOShakePosition(1, 1).OnComplete(()=>transform.position = originPos);
            SoundManager.Instance.PlaySound("Sounds/UT/Damage");
        }

    }

}
