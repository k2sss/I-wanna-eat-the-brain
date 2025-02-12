using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Undertale
{
    public class Round1 : Round
    {
        public GameObject Prefab;
        public override void StartAttack()
        {
            for (int i = 0; i < 8; i++)
            {
                int offset = Random.Range(0, 3);
                Attack a = new Attack() { AttackPrefab = Prefab, direction = new Vector2(-1, 0), GeneratePos = new Vector3(17, offset, 0), Time = i*1.5f };
                Attack b = new Attack() { AttackPrefab = Prefab, direction = new Vector2(1, 0), GeneratePos = new Vector3(-17, offset, 0), Time = i * 1.5f };
                attackList.Add(a);
                attackList.Add(b);
            }


            UTGameManager.Instance.GetPlayer().fsm.SwitchState(1);
            base.StartAttack();
        }
    }

}
