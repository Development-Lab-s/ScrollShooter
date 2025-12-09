using System;
using Unity.VisualScripting;
using UnityEngine;
using YGPacks.PoolManager;

public abstract class PopupWindowBase: MonoBehaviour, IRecycle
{
    public RectTransform RectCompo { get; protected set; }
    public Action<IRecycle> Destroyed { get; set; }

    public void Activate(Vector3 startPos)
    {
        transform.position = startPos;
        gameObject.SetActive(true);
    }
    public abstract void OnPopup();
}
