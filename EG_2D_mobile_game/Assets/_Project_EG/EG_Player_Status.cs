using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EG_Player_Status : MonoBehaviour
{
    [Header("Health")]
    [SerializeField]
    private int i_Health = 50;

    [SerializeField]
    private Text t_Health;

    [Header("Defense")]
    [SerializeField]
    private int i_Defense = 0;

    [SerializeField]
    private Text t_Defense;

    [Header("Mask")]
    [SerializeField]
    private bool b_Mask = false;

    private Animator a_Animator;
    
    private void Start()
    {
        a_Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Set_UI_Auto();
    }

    private void Set_UI_Auto()
    {
        t_Health.text = i_Health.ToString();
        t_Defense.text = i_Defense.ToString();

        if (a_Animator.GetBool("Mask") != b_Mask)
        {
            a_Animator.SetBool("Mask", b_Mask);
            a_Animator.SetTrigger("Trig_Mask");
        }
    }
}
