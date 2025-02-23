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
    public GameEventTrigger[] triggers;

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
    public void EnableTrigger(int index)
    {
        if (index < 0 || index >= triggers.Length)
        {
            return;
        }
        triggers[index].EnableTrigger();
    }
    public void PlayAnimation(string Anim)
    {
        var args = Anim.Trim().Split(',');
        if(args.Length == 1)
        _animator.Play(Anim);
        else
        {
            try
            {
                float delayTime = float.Parse(args[1]);
                GameManager.Instance.Delay(delayTime, () => _animator.Play(args[0]));
            }
            catch { }

        }

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
        //参数三代表延迟时间
        if (fargs.Length == 2)
        {
            transform.DOMoveY(transform.position.y + fargs[1], fargs[0]);
        }
        else
        if (fargs.Length == 3)
        {
            GameManager.Instance.Delay(fargs[2], () => transform.DOMoveY(transform.position.y + fargs[1], fargs[0]));
           
        }
    }
    public void MoveX(string args)
    {
        float[] fargs = FloatArgExplainer(args);
        //参数一代表duration
        //参数二代表位移量
        
        if (fargs.Length == 2)
        {
            transform.DOMoveX(transform.position.x + fargs[1], fargs[0]);
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
    public void Harmful()
    {
        gameObject.tag = "Killer";
    }
    
}
