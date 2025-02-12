using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
namespace Undertale
{
    public class Round2 : Round
    {
        public StatePurpleHeart purpleHeartState;
        public GameObject bullet;
        public float BulletSpeed;
        public override void StartAttack()
        {
           
            HeartController controller= UTGameManager.Instance.GetPlayer();
            controller.fsm.SwitchState(4);
            //InitAttack
            for (int i = 0; i < 10; i++)
            {
                SetBullet(i, 1, new Vector3(-17, purpleHeartState.OriginY + purpleHeartState.YSpace, 0));
                SetBullet(i, -1, new Vector3(17, purpleHeartState.OriginY, 0));
                SetBullet(i, 1, new Vector3(-17, purpleHeartState.OriginY - purpleHeartState.YSpace, 0));
            }


            base.StartAttack();
        }
        public void SetBullet(float time, int direction, Vector3 GeneratePos)
        {
            if (Random.Range(0, 100) < 15) return;

            BulletAttack attack = new BulletAttack()
            {
                GeneratePos = GeneratePos,
                Time = time,
                AttackPrefab = bullet,
                direction = new Vector2(direction, 0),
                bulletDir = new Vector2(1, 0),
                Speed = BulletSpeed+Random.Range(-2,3),
                DisappearTime = 7

            };
            attackList.Add(attack);
        }
    }

}
