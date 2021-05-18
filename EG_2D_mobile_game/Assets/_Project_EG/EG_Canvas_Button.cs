using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EG_Canvas_Button : MonoBehaviour
{
    [Header("Data Get")]
    [SerializeField]
    private Isometric_Map cl_Map;

    [SerializeField]
    private Isometric_Move cl_Move;

    [Header("Data Set")]
    [SerializeField]
    private Image g_Button_Up;

    [SerializeField]
    private Image g_Button_Down;

    [SerializeField]
    private Image g_Button_Left;

    [SerializeField]
    private Image g_Button_Right;

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

    private void Update()
    {
        Set_ButtonUI_Up();
        Set_ButtonUI_Down();
        Set_ButtonUI_Left();
        Set_ButtonUI_Right();
    }

    private void Set_ButtonUI_Up()
    {
        if (cl_Move.Get_CheckAllow_Move(cl_Map.v2_DirUp))
        {
            g_Button_Up.GetComponent<Image>().sprite = sp_Nothing;
        }
        else
        {
            g_Button_Up.GetComponent<Image>().sprite = sp_Move;
        }
    }

    private void Set_ButtonUI_Down()
    {
        if (cl_Move.Get_CheckAllow_Move(cl_Map.v2_DirDown))
        {
            g_Button_Down.GetComponent<Image>().sprite = sp_Nothing;
        }
        else
        {
            g_Button_Down.GetComponent<Image>().sprite = sp_Move;
        }
    }

    private void Set_ButtonUI_Left()
    {
        if (cl_Move.Get_CheckAllow_Move(cl_Map.v2_DirLeft))
        {
            g_Button_Left.GetComponent<Image>().sprite = sp_Nothing;
        }
        else
        {
            g_Button_Left.GetComponent<Image>().sprite = sp_Move;
        }
    }

    private void Set_ButtonUI_Right()
    {
        if (cl_Move.Get_CheckAllow_Move(cl_Map.v2_DirRight))
        {
            g_Button_Right.GetComponent<Image>().sprite = sp_Nothing;
        }
        else
        {
            g_Button_Right.GetComponent<Image>().sprite = sp_Move;
        }
    }
}
