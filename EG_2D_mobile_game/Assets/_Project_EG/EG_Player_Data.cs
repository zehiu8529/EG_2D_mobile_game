using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EG_Player_Data : MonoBehaviour
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

    [Header("Poision Blue")]
    [SerializeField]
    private int i_Poision_Blue = 0;

    [SerializeField]
    private Text t_Poision_Blue;

    [Header("Poision Red")]
    [SerializeField]
    private int i_Poision_Red = 0;

    [SerializeField]
    private Text t_Poision_Red;

    [Header("Poision Green")]
    [SerializeField]
    private int i_Poision_Green = 0;

    [SerializeField]
    private Text t_Poision_Green;

    [Header("Mask")]
    [SerializeField]
    private bool b_Mask = false;

    private EG_Player_Poision cl_Poision;

    private Animator a_Animator;
    
    private void Start()
    {
        cl_Poision = GetComponent<EG_Player_Poision>();

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
        t_Poision_Blue.text = i_Poision_Blue.ToString();
        t_Poision_Red.text = i_Poision_Red.ToString();
        t_Poision_Green.text = i_Poision_Green.ToString();

        if (a_Animator.GetBool("Mask") != b_Mask)
        {
            a_Animator.SetBool("Mask", b_Mask);
            a_Animator.SetTrigger("Trig_Mask");
        }
    }

    //Button Table Poision

    public void Button_TablePoision_Up()
    {
        if (cl_Poision.Get_Check_Poision_Up())
        {
            if (cl_Poision.Get_GameObject_TablePoision_Up().GetComponent<EG_Table_Poision>() != null)
            {
                int i_PoisonTake_Blue = 0, i_PoisionTake_Red = 0, i_PoisionTake_Green = 0;

                cl_Poision.Get_GameObject_TablePoision_Up().GetComponent<EG_Table_Poision>().Set_Act_PoisionTake(
                    out i_PoisonTake_Blue, out i_PoisionTake_Red, out i_PoisionTake_Green);

                this.i_Poision_Blue += i_PoisonTake_Blue;
                this.i_Poision_Red += i_PoisionTake_Red;
                this.i_Poision_Green += i_PoisionTake_Green;
            }
        }
    }

    public void Button_TablePoision_Down()
    {
        if (cl_Poision.Get_Check_Poision_Down())
        {
            if (cl_Poision.Get_GameObject_TablePoision_Down().GetComponent<EG_Table_Poision>() != null)
            {
                int i_PoisonTake_Blue = 0, i_PoisionTake_Red = 0, i_PoisionTake_Green = 0;

                cl_Poision.Get_GameObject_TablePoision_Down().GetComponent<EG_Table_Poision>().Set_Act_PoisionTake(
                    out i_PoisonTake_Blue, out i_PoisionTake_Red, out i_PoisionTake_Green);

                this.i_Poision_Blue += i_PoisonTake_Blue;
                this.i_Poision_Red += i_PoisionTake_Red;
                this.i_Poision_Green += i_PoisionTake_Green;
            }
        }
    }

    public void Button_TablePoision_Left()
    {
        if (cl_Poision.Get_Check_Poision_Left())
        {
            if (cl_Poision.Get_GameObject_TablePoision_Left().GetComponent<EG_Table_Poision>() != null)
            {
                int i_PoisonTake_Blue = 0, i_PoisionTake_Red = 0, i_PoisionTake_Green = 0;

                cl_Poision.Get_GameObject_TablePoision_Left().GetComponent<EG_Table_Poision>().Set_Act_PoisionTake(
                    out i_PoisonTake_Blue, out i_PoisionTake_Red, out i_PoisionTake_Green);

                this.i_Poision_Blue += i_PoisonTake_Blue;
                this.i_Poision_Red += i_PoisionTake_Red;
                this.i_Poision_Green += i_PoisionTake_Green;
            }
        }
    }

    public void Button_TablePoision_Right()
    {
        if (cl_Poision.Get_Check_Poision_Right())
        {
            if (cl_Poision.Get_GameObject_TablePoision_Right().GetComponent<EG_Table_Poision>() != null)
            {
                int i_PoisonTake_Blue = 0, i_PoisionTake_Red = 0, i_PoisionTake_Green = 0;

                cl_Poision.Get_GameObject_TablePoision_Right().GetComponent<EG_Table_Poision>().Set_Act_PoisionTake(
                    out i_PoisonTake_Blue, out i_PoisionTake_Red, out i_PoisionTake_Green);

                this.i_Poision_Blue += i_PoisonTake_Blue;
                this.i_Poision_Red += i_PoisionTake_Red;
                this.i_Poision_Green += i_PoisionTake_Green;
            }
        }
    }
}
