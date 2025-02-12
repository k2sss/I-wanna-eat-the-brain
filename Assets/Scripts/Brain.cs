using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Brain : MonoBehaviour
{
    public string nextSceneName;
    private AudioClip[] eatips;
    private bool isWin;
    private void Start()
    {
        eatips = new AudioClip[2];
        eatips[0] = Resources.Load<AudioClip>("Sounds/chomp");
        eatips[1] = Resources.Load<AudioClip>("Sounds/chomp2");

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //限制只触发一次
        if (isWin) return;
        isWin = true;

        if (collision.CompareTag("Player"))
        {
            PlayerController pc = collision.GetComponent<PlayerController>();
            if (!pc.fsm.CompareState((int)ZombieStateName.Death))
            {
                SoundManager.Instance.PlayRandomSound(eatips);
                GameManager.Instance.LoadSceneAsync(nextSceneName);
            }
        }
    }
}
