using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace Undertale
{
    public class FightUI : MonoBehaviour
    {
        public GameObject Sign;
        public float SingMoveSpeed = 2;

        public Transform RightLimit;
        public Transform leftLimit;
        private bool isPressed;
        public UTPeashooter target;
        public Animator sliceAnimator;
        public Animator targetAnimator;
        private Action OnFinish;
        private void Update()
        {
            SingMoveSpeed += 20 * Time.deltaTime;
            if (!isPressed)
            {
                if (Sign.transform.position.x > leftLimit.position.x)
                {

                    Sign.transform.Translate(new Vector2(-1 * Time.deltaTime * SingMoveSpeed, 0));

                }
                else
                {
                    OnPressed();
                }
            }
            if (!isPressed && Input.GetKeyDown(KeyCode.Z))
            {
                OnPressed();
            }
        }
        public void AddFinishListener(Action action)
        {
            OnFinish += action;
        }
        public void Init()
        {
            OnFinish = null;//Çå¿ÕÎ¯ÍÐ
            isPressed = false;
            SingMoveSpeed = 2;
            gameObject.SetActive(true);
            Sign.transform.position = RightLimit.transform.position;
            targetAnimator.Play("Enter");
        }
        public void OnPressed()
        {
            isPressed = true;
            sliceAnimator.Play("Slice");
            SoundManager.Instance.PlaySound("Sounds/UT/Slice");
          
            GameManager.Instance.Delay(0.5f, () => target.Hurt());

            GameManager.Instance.Delay(1, () => targetAnimator.Play("Quit"));
            GameManager.Instance.Delay(1.4f, () =>
            {
                gameObject.SetActive(false);
                OnFinish?.Invoke();
            });
        }

    }

}
