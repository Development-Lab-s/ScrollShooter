using System;
using UnityEngine;

public class TitlePAni : MonoBehaviour
{
    public Action<bool> isTrigger;

    private readonly int IsState = Animator.StringToHash("IsState");
    
    private Animator _aniCompo;

    [SerializeField]private SideButtonClick click;

    private void Awake()
    {
        _aniCompo = GetComponent<Animator>();
        click = FindAnyObjectByType<SideButtonClick>();
    }
    public void ConverSion(bool istrigger)
    {
        click.isOpen = istrigger;
        _aniCompo.SetBool(IsState,istrigger);
    }
}
