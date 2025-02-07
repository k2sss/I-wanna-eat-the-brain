using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Trap : MonoBehaviour
{
    private Animator _animator;
    public bool isHarmless;
    public bool isDestroyable;
    public float destroyTime = 100;

    protected virtual void Awake()
    { 
        _animator = GetComponent<Animator>();
        if(!isHarmless)
        gameObject.tag = "Killer";
        Collider2D c = gameObject.GetComponent<Collider2D>();
        if (c != null)
        c.isTrigger = true;
        
    }
    protected virtual void Start()
    {
       
        if (isDestroyable)
            GameManager.Instance.Delay(destroyTime, () => Destroy(gameObject));
    }
    public void PlayAnimation(string Anim)
    {
        _animator.Play(Anim);
    }
    public void SetBoolTrue(string boolName)
    {
        _animator.SetBool(boolName, true);
    }
    public void PlaySound(AudioClip clip)
    {
        SoundManager.Instance.PlaySound(clip);
    }
    public void AppearWithDelay(float delayTime)
    {
        GameManager.Instance.Delay(delayTime, () => gameObject.SetActive(true));
    }
    public void MoveY(string args)
    {
        float[] fargs = FloatArgExplainer(args);
        //参数一代表duration
        //参数二代表位移量
        if (fargs.Length == 2)
        {
            transform.DOMoveY(transform.position.y + fargs[1], fargs[0]);
        }
    }
    public float[] FloatArgExplainer(string args)
    {
        try
        {
            var contents = args.Trim().Split(',');
            float[] go = new float[contents.Length];
            for (int i = 0; i < contents.Length; i++)
            {
                go[i] = float.Parse(contents[i]);
            }
            return go;
        }
        catch (System.Exception e)
        {
            Debug.LogError(e.Message);
            return null;
        }
    }

}
