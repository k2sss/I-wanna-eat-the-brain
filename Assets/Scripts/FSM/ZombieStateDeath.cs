using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.UI.Image;

public class ZombieStateDeath : MonoBaseState
{
    public GameObject RagDollPrefab;
    public GameObject originView;
    public Rigidbody2D originRb;
   
    public bool isDead;
    public ParticleSystem DeathParticle;
    public GameObject deathUI;
    public AudioClip deathSound,deathSound2;
    public List<AudioClip> deathMusics;
    private bool isLoadingScene;
    
    public override void OnEnter()
    {
        if (isDead) return;
        isDead = true;
        GameObject ragdoll = Instantiate(RagDollPrefab);
        ragdoll.transform.position = transform.position;
        originView.SetActive(false);
        originRb.isKinematic = true;
        originRb.velocity = Vector3.zero;
        SoundManager.Instance.PlaySound(deathSound);
        if (deathSound2) SoundManager.Instance.PlaySound(deathSound2);
        SoundManager.Instance.PlayRandomMusic(deathMusics.ToArray(),1,false);
        Invoke(nameof(Restart), 8);
        DeathParticle.Emit(50);
        Instantiate(deathUI);
    }
    public void Restart()
    {
        if(isLoadingScene) return;

        isLoadingScene = true;
        
        GameManager.Instance.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }
  
    public override void OnExit()
    {
       
    }

    public override void OnFixedUpdate()
    {
       
    }

    public override void OnUpdate()
    {
       if (Input.GetMouseButtonDown(0)||Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
    }
}
