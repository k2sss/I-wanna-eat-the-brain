using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Undertale
{
    public class UTGameManager : BaseMonoManager<UTGameManager>
    {
        public HeartController player;
        public Transform heartStartPos;
        public HeartController GetPlayer()
        {
            if (player == null) throw new System.Exception("获取不到Player");
            return player;
        }
    }

}
