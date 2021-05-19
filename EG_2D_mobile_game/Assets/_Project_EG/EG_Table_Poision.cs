using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EG_Table_Poision : MonoBehaviour
{
    private bool b_PoisionEmty = false;
    private bool b_PoisionEmty_Cur = false;

    [SerializeField]
    private int i_PosionAdd_Blue = 1;

    [SerializeField]
    private int i_PoisionAdd_Red = 1;

    [SerializeField]
    private int i_PoisionAdd_Green = 1;

    private Animator a_Animator;

    private void Start()
    {
        a_Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(!b_PoisionEmty_Cur && b_PoisionEmty_Cur != b_PoisionEmty)
        {
            a_Animator.SetTrigger("Take");
            b_PoisionEmty_Cur = b_PoisionEmty;
        }
        else
        if (b_PoisionEmty_Cur && b_PoisionEmty_Cur != b_PoisionEmty)
        {
            a_Animator.SetTrigger("Fill");
            b_PoisionEmty_Cur = b_PoisionEmty;
        }
    }

    public void Set_Act_PoisionTake(out int i_PoisionAdd_Blue, out int i_OisionAdd_Red, out int i_PoisionAdd_Green)
    {
        if (!b_PoisionEmty)
        {
            i_PoisionAdd_Blue = this.i_PosionAdd_Blue;
            i_OisionAdd_Red = this.i_PoisionAdd_Red;
            i_PoisionAdd_Green = this.i_PoisionAdd_Green;
            b_PoisionEmty = true;
        }
        else
        {
            i_PoisionAdd_Blue = -this.i_PosionAdd_Blue;
            i_OisionAdd_Red = -this.i_PoisionAdd_Red;
            i_PoisionAdd_Green = -this.i_PoisionAdd_Green;
            b_PoisionEmty = false;
        }
    }
}
