using TMPro;
using UnityEngine;

public abstract class BlockBase : MonoBehaviour, IBlock
{
    [SerializeField]
    protected BlockDataSO blockData;
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private TextMeshPro _text;
    protected virtual void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    protected virtual void OnValidate()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _text = GetComponentInChildren<TextMeshPro>();

        if (_spriteRenderer != null)
        {
            _spriteRenderer.sprite = blockData.Icon;
        }
        if (_text != null)
        {
            _text.text = blockData.GetRendomName();
        }
    }
    public abstract void Break(GameObject target);

    public bool CheckBreak(bool isDash)
    {
        switch(blockData.Type)
        {
            case BlockType.DontBreak: return false;
            case BlockType.DashBreak: return isDash?true:false;
            case BlockType.Break: return true;
            default: return true;
        }
    }

    public abstract void Collision(GameObject target);
}
