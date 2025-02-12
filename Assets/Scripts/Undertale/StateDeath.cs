using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Undertale
{
    public class StateDeath : MonoBaseState
    {
        public HeartController controller;
        public GameObject DeathUI;
        private float DeathTimer;
        public override void OnEnter()
        {
           DeathUI.SetActive(true);
        }

        public override void OnExit(MonoBaseState nextState)
        {
            
        }

        public override void OnFixedUpdate()
        {
         
        }

        public override void OnUpdate()
        {
            DeathTimer += Time.deltaTime;
            if (DeathTimer > 2)
            {
               
                if (Input.GetKeyDown(KeyCode.R) || Input.GetMouseButtonDown(0))
                { DeathTimer = -10;
                    GameManager.Instance.LoadSceneAsync(SceneManager.GetActiveScene().name);
                }
            }
        }
    }

}
