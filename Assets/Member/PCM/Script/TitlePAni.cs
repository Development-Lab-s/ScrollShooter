using System;
using UnityEngine;

public class TitlePAni : MonoBehaviour
{
    public Action<bool> isTrigger;

    private readonly int IsState = Animator.StringToHash("IsState");
    
    private Animator _aniCompo;

    private void Awake()
    {
        _aniCompo = GetComponent<Animator>();
    }
    public void ConverSion(bool istrigger)
    {
        _aniCompo.SetBool(IsState,istrigger);
    }
}
