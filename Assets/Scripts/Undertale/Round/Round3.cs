using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Undertale
{
    public class Round3 : Round
    {
        public GameObject Prefab;
        public float Strength;
        public float Originoffset;

        public override void StartAttack()
        {
            float time =0f;
            for (int i = 0; i < 30; i++)
            {
                time += 0.1f;
                float  offset = Originoffset + Strength * (Mathf.Sin(time) - 0.5f);
                Attack a = new Attack() { AttackPrefab = Prefab, direction = new Vector2(-1, 0), GeneratePos = new Vector3(17, offset, 0), Time = time };
                attackList.Add(a);
            }
            time += 2;
            for (int i = 0; i < 30; i++)
            {
                time += 0.1f;
                float offset = Originoffset + Strength * (Mathf.Sin(time*2f) - 0.5f);
                Attack a = new Attack() { AttackPrefab = Prefab, direction = new Vector2(1, 0), GeneratePos = new Vector3(-17, offset, 0), Time = time };
                attackList.Add(a);
            }
            base.StartAttack();
        }
        public override void EndTheAttack()
        {

            base.EndTheAttack();
        }
    }

}

