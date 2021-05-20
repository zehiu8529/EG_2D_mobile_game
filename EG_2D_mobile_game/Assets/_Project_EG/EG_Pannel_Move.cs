using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EG_Pannel_Move : MonoBehaviour
{
    [Header("Data Get")]
    [SerializeField]
    private Isometric_Map cl_Map;

    [SerializeField]
    private GameObject g_Player;

    private EG_Player_Poision cl_Poision;
    private Isometric_Move cl_Move;

    [Header("Data Set")]
    [SerializeField]
    private Image i_Button_Up;

    [SerializeField]
    private Image i__Button_Down;

    [SerializeField]
    private Image i__Button_Left;

    [SerializeField]
    private Image i__Button_Right;

    [SerializeField]
    private Sprite sp_Move;

    [SerializeField]
    private Sprite sp_Action;

    [SerializeField]
    private Sprite sp_Danger;

    [SerializeField]
    private Sprite sp_Combat_Close;

    [SerializeField]
    private Sprite sp_Combat_Range;

    [SerializeField]
    private Sprite sp_Escape;

    [SerializeField]
    private Sprite sp_Nothing;

    private void Start()
    {
        cl_Move = g_Player.GetComponent<Isometric_Move>();
        cl_Poision = g_Player.GetComponent<EG_Player_Poision>();
    }

    private void Update()
    {
        Set_ButtonUI_Up();
        Set_ButtonUI_Down();
        Set_ButtonUI_Left();
        Set_ButtonUI_Right();
    }

    private void Set_ButtonUI_Up()
    {
        if (cl_Move.Get_CheckAvoid_Ground(cl_Map.v2_DirUp))
        {
            i_Button_Up.GetComponent<Image>().sprite = sp_Nothing;
        }
        else
        if (cl_Poision.Get_Check_Poision(cl_Map.v2_DirUp) && cl_Poision.Get_Check_TablePoision_Emty(cl_Map.v2_DirUp))
        //Poision on Table Check
        {
            i_Button_Up.GetComponent<Image>().sprite = sp_Nothing;
        }
        else
        if (cl_Move.Get_CheckExist_Fence(cl_Map.v2_DirUp))
        //Fence Check
        {
            i_Button_Up.GetComponent<Image>().sprite = sp_Nothing;
        }
        else
        if (cl_Poision.Get_Check_Poision(cl_Map.v2_DirUp) && !cl_Poision.Get_Check_TablePoision_Emty(cl_Map.v2_DirUp))
        //Poision on Table Check
        {
            i_Button_Up.GetComponent<Image>().sprite = sp_Action;
        }
        else
        {
            i_Button_Up.GetComponent<Image>().sprite = sp_Move;
        }
    }

    private void Set_ButtonUI_Down()
    {
        if (cl_Move.Get_CheckAvoid_Ground(cl_Map.v2_DirDown))
        {
            i__Button_Down.GetComponent<Image>().sprite = sp_Nothing;
        }
        else
        if (cl_Poision.Get_Check_Poision(cl_Map.v2_DirDown) && cl_Poision.Get_Check_TablePoision_Emty(cl_Map.v2_DirDown))
        //Poision on Table Check
        {
            i__Button_Down.GetComponent<Image>().sprite = sp_Nothing;
        }
        else
        if (cl_Move.Get_CheckExist_Fence(cl_Map.v2_DirDown))
        //Fence Check
        {
            i__Button_Down.GetComponent<Image>().sprite = sp_Nothing;
        }
        else
        if (cl_Poision.Get_Check_Poision(cl_Map.v2_DirDown) && !cl_Poision.Get_Check_TablePoision_Emty(cl_Map.v2_DirDown))
        //Poision on Table Check
        {
            i__Button_Down.GetComponent<Image>().sprite = sp_Action;
        }
        else
        {
            i__Button_Down.GetComponent<Image>().sprite = sp_Move;
        }
    }

    private void Set_ButtonUI_Left()
    {
        if (cl_Move.Get_CheckAvoid_Ground(cl_Map.v2_DirLeft))
        {
            i__Button_Left.GetComponent<Image>().sprite = sp_Nothing;
        }
        else
        if (cl_Poision.Get_Check_Poision(cl_Map.v2_DirLeft) && cl_Poision.Get_Check_TablePoision_Emty(cl_Map.v2_DirLeft))
        //Poision on Table Check
        {
            i__Button_Left.GetComponent<Image>().sprite = sp_Nothing;
        }
        else
        if (cl_Move.Get_CheckExist_Fence(cl_Map.v2_DirLeft))
        //Fence Check
        {
            i__Button_Left.GetComponent<Image>().sprite = sp_Nothing;
        }
        else
        if (cl_Poision.Get_Check_Poision(cl_Map.v2_DirLeft) && !cl_Poision.Get_Check_TablePoision_Emty(cl_Map.v2_DirLeft))
        //Poision on Table Check
        {
            i__Button_Left.GetComponent<Image>().sprite = sp_Action;
        }
        else
        {
            i__Button_Left.GetComponent<Image>().sprite = sp_Move;
        }
    }

    private void Set_ButtonUI_Right()
    {
        if (cl_Move.Get_CheckAvoid_Ground(cl_Map.v2_DirRight))
        {
            i__Button_Right.GetComponent<Image>().sprite = sp_Nothing;
        }
        else
        if (cl_Poision.Get_Check_Poision(cl_Map.v2_DirRight) && cl_Poision.Get_Check_TablePoision_Emty(cl_Map.v2_DirRight))
        //Poision on Table Check
        {
            i__Button_Right.GetComponent<Image>().sprite = sp_Nothing;
        }
        else
        if (cl_Move.Get_CheckExist_Fence(cl_Map.v2_DirRight))
        //Fence Check
        {
            i__Button_Right.GetComponent<Image>().sprite = sp_Nothing;
        }
        else
        if (cl_Poision.Get_Check_Poision(cl_Map.v2_DirRight) && !cl_Poision.Get_Check_TablePoision_Emty(cl_Map.v2_DirRight))
        //Poision on Table Check
        {
            i__Button_Right.GetComponent<Image>().sprite = sp_Action;
        }
        else
        {
            i__Button_Right.GetComponent<Image>().sprite = sp_Move;
        }
    }
}
