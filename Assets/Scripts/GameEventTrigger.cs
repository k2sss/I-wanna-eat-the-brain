using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Events;
public class GameEventTrigger : MonoBehaviour
{
    public BoxCollider2D _collider;
    public bool isTriggered;
    private bool isPlayerEnter;
    public UnityEvent gameEvent;
    public UnityEvent onPlayerOut;
    private void OnDrawGizmos()
    {
        if (_collider == null) return;
        Gizmos.color = isTriggered ? Color.red : Color.white;

        // ��ȡ��ײ����λ�úͳߴ�
        Vector2 colliderPosition = _collider.bounds.center;
        Vector2 colliderSize = _collider.bounds.size;

        // �� Scene ��ͼ�л��� BoxCollider2D
        Gizmos.DrawWireCube(colliderPosition, colliderSize);

    }
    private void Start()
    {
        _collider.isTrigger = true;
    }
    public void EnableTrigger()
    {
        isTriggered = false;
    }
    public void DisableTrigger()
    {
        isTriggered = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isTriggered)
        {
            isPlayerEnter = true;
           isTriggered = true;
            gameEvent?.Invoke();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") &&isPlayerEnter)
        {
            isPlayerEnter = false;
            onPlayerOut?.Invoke();
        }
    }
    public void PlaySound(AudioClip clip)
    {
        SoundManager.Instance.PlaySound(clip);
    }
}
