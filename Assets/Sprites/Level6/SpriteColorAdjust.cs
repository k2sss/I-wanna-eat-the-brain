using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteAlways]
[RequireComponent(typeof(SpriteRenderer))]
public class SpriteColorAdjust : MonoBehaviour
{
    [Range(0,255)]
    public float threshold = 0.5f;
    public Color replaceColor = Color.white;

    private SpriteRenderer spriteRenderer;
    private MaterialPropertyBlock propBlock;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        propBlock = new MaterialPropertyBlock();
        ApplyProperties();
    }

    void OnValidate()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        propBlock = new MaterialPropertyBlock();
        ApplyProperties();
    }

    private void Update()
    {
        ApplyProperties();
        if (threshold > oldValue)
        {
            oldValue = threshold;
            if(Application.isPlaying)
                SoundManager.Instance.PlayRandomSound(clips);
        }
        else if (threshold != oldValue)
        {
            oldValue = threshold;
        }
    }
    public AudioClip[] clips;

    public float oldValue = 0;

    void ApplyProperties()
    {
        if (spriteRenderer == null) return;

        spriteRenderer.GetPropertyBlock(propBlock);
        propBlock.SetFloat("_Threshold", threshold/ 255f);
        propBlock.SetColor("_ReplaceColor", replaceColor);
        spriteRenderer.SetPropertyBlock(propBlock);
    }
}
