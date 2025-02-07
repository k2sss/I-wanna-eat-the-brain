using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class GameEventTrigger : MonoBehaviour
{
    public BoxCollider2D _collider;
    public bool isTriggered;
    public UnityEvent gameEvent;
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true;
            gameEvent?.Invoke();
        }
    }
}
