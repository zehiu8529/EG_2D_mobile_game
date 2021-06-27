using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EG_MoveUI : MonoBehaviour
{
    #region Public Varible

    /// <summary>
    /// Tag for other Isometric Map Manager
    /// </summary>
    [Header("Isometric Map Tag")]
    [SerializeField]
    private string s_MapManager_Tag = "IsometricMap";

    /// <summary>
    /// Map Manager
    /// </summary>
    [SerializeField]
    private Isometric_MapManager cl_MapManager;

    /// <summary>
    /// Tag for other Isometric Map Manager
    /// </summary>
    [Header("Client Manager Tag")]
    [SerializeField]
    private string s_SocketManager_Tag = "ClientManager";

    [SerializeField]
    private Socket_ClientManager cl_ClientManager;

    /// <summary>
    /// Tag for other Isometric Object to Find
    /// </summary>
    [Header("Isometric Client Tag")]
    [SerializeField]
    private string s_Client_Tag = "IsometricClient";

    /// <summary>
    /// Client GameObject
    /// </summary>
    [SerializeField]
    private Isometric_MoveControl cl_ClientControl;

    //Button

    [Header("Move Button UI")]
    [SerializeField]
    private Image i_Button_Up;

    [SerializeField]
    private Image i_Button_Down;

    [SerializeField]
    private Image i_Button_Left;

    [SerializeField]
    private Image i_Button_Right;

    //Sample

    [Header("Move Button Sample")]
    [SerializeField]
    private Sprite s_Sample_Nope;

    [SerializeField]
    private Sprite s_Sample_Move;

    [SerializeField]
    private Sprite s_Sample_Get;

    #endregion

    private void Start()
    {
        if (cl_MapManager == null)
        {
            if (s_MapManager_Tag != "")
            {
                cl_MapManager = GameObject.FindGameObjectWithTag(s_MapManager_Tag).GetComponent<Isometric_MapManager>();
            }
        }

        if (cl_ClientManager == null)
        {
            if (s_SocketManager_Tag != "")
            {
                cl_ClientManager = GameObject.FindGameObjectWithTag(s_SocketManager_Tag).GetComponent<Socket_ClientManager>();
            }
        }
    }

    private void Update()
    {
        if (cl_ClientManager.Get_Socket_Start())
        {
            if (cl_ClientControl == null)
            {
                if (s_Client_Tag != "")
                {
                    GameObject g_FindGameObject = GameObject.FindGameObjectWithTag(s_Client_Tag);

                    if(g_FindGameObject != null)
                    {
                        cl_ClientControl = g_FindGameObject.GetComponent<Isometric_MoveControl>();
                    }
                }
            }
            else
            {
                Set_Auto_MoveUI();
            }
        }
    }

    /// <summary>
    /// Auto Move UI Button
    /// </summary>
    private void Set_Auto_MoveUI()
    {
        Set_MoveUI_Up();
        Set_MoveUI_Down();
        Set_MoveUI_Left();
        Set_MoveUI_Right();
    }

    private void Set_MoveUI_Up()
    {
        if (!cl_ClientControl.Get_CheckMove_Dir_InsideMap(new Class_Vector().v2_Isometric_DirUp))
        {
            i_Button_Up.GetComponent<Image>().sprite = s_Sample_Nope;
        }
        else
        if (!cl_ClientControl.Get_CheckMove_Dir_Fence(new Class_Vector().v2_Isometric_DirUp))
        {
            i_Button_Up.GetComponent<Image>().sprite = s_Sample_Nope;
        }
        else
        if (!cl_ClientControl.Get_CheckMove_Dir_Object(new Class_Vector().v2_Isometric_DirUp)) 
        {
            if (!cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Up()).GetComponent<EG_ClientTable>().Get_Table_Get_Already())
            {
                i_Button_Up.GetComponent<Image>().sprite = s_Sample_Get;
            }
            else
            {
                i_Button_Up.GetComponent<Image>().sprite = s_Sample_Nope;
            }
        }
        else
        {
            i_Button_Up.GetComponent<Image>().sprite = s_Sample_Move;
        }
    }

    private void Set_MoveUI_Down()
    {
        if (!cl_ClientControl.Get_CheckMove_Dir_InsideMap(new Class_Vector().v2_Isometric_DirDown))
        {
            i_Button_Down.GetComponent<Image>().sprite = s_Sample_Nope;
        }
        else
        if (!cl_ClientControl.Get_CheckMove_Dir_Fence(new Class_Vector().v2_Isometric_DirDown))
        {
            i_Button_Down.GetComponent<Image>().sprite = s_Sample_Nope;
        }
        else
        if (!cl_ClientControl.Get_CheckMove_Dir_Object(new Class_Vector().v2_Isometric_DirDown))
        {
            if (!cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Down()).GetComponent<EG_ClientTable>().Get_Table_Get_Already())
            {
                i_Button_Down.GetComponent<Image>().sprite = s_Sample_Get;
            }
            else
            {
                i_Button_Down.GetComponent<Image>().sprite = s_Sample_Nope;
            }
        }
        else
        {
            i_Button_Down.GetComponent<Image>().sprite = s_Sample_Move;
        }
    }

    private void Set_MoveUI_Left()
    {
        if (!cl_ClientControl.Get_CheckMove_Dir_InsideMap(new Class_Vector().v2_Isometric_DirLeft))
        {
            i_Button_Left.GetComponent<Image>().sprite = s_Sample_Nope;
        }
        else
        if (!cl_ClientControl.Get_CheckMove_Dir_Fence(new Class_Vector().v2_Isometric_DirLeft))
        {
            i_Button_Left.GetComponent<Image>().sprite = s_Sample_Nope;
        }
        else
        if (!cl_ClientControl.Get_CheckMove_Dir_Object(new Class_Vector().v2_Isometric_DirLeft))
        {
            if (!cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Left()).GetComponent<EG_ClientTable>().Get_Table_Get_Already())
            {
                i_Button_Left.GetComponent<Image>().sprite = s_Sample_Get;
            }
            else
            {
                i_Button_Left.GetComponent<Image>().sprite = s_Sample_Nope;
            }
        }
        else
        {
            i_Button_Left.GetComponent<Image>().sprite = s_Sample_Move;
        }
    }

    private void Set_MoveUI_Right()
    {
        if (!cl_ClientControl.Get_CheckMove_Dir_InsideMap(new Class_Vector().v2_Isometric_DirRight))
        {
            i_Button_Right.GetComponent<Image>().sprite = s_Sample_Nope;
        }
        else
        if (!cl_ClientControl.Get_CheckMove_Dir_Fence(new Class_Vector().v2_Isometric_DirRight))
        {
            i_Button_Right.GetComponent<Image>().sprite = s_Sample_Nope;
        }
        else
        if (!cl_ClientControl.Get_CheckMove_Dir_Object(new Class_Vector().v2_Isometric_DirRight))
        {
            if (!cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Right()).GetComponent<EG_ClientTable>().Get_Table_Get_Already())
            {
                i_Button_Right.GetComponent<Image>().sprite = s_Sample_Get;
            }
            else
            {
                i_Button_Right.GetComponent<Image>().sprite = s_Sample_Nope;
            }
        }
        else
        {
            i_Button_Right.GetComponent<Image>().sprite = s_Sample_Move;
        }
    }
}
