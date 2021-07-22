using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EG_UIMove : MonoBehaviour
{
    #region Public Varible

    [Header("Isometric Map Tag")]
    [SerializeField]
    private Isometric_MapManager cl_MapManager;

    [Header("Client Manager Tag")]
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

    [SerializeField]
    private Sprite s_Sample_Craft;

    #endregion

    private EG_UICraft cl_UICraft;

    private void Start()
    {
        cl_UICraft = GetComponent<EG_UICraft>();
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
        if (cl_UICraft.Get_CraftShow())
        {
            i_Button_Up.GetComponent<Image>().sprite = s_Sample_Nope;
        }
        else
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
            //Poision
            if (cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Up()).GetComponent<EG_ClientTable_Poision>() != null)
            {
                if (!cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Up()).GetComponent<EG_ClientTable_Poision>().Get_Table_Get_Already())
                {
                    i_Button_Up.GetComponent<Image>().sprite = s_Sample_Get;
                }
                else
                {
                    i_Button_Up.GetComponent<Image>().sprite = s_Sample_Nope;
                }
            }
            else
            //Item
            if (cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Up()).GetComponent<EG_ClientTable_Item>() != null)
            {
                if (!cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Up()).GetComponent<EG_ClientTable_Item>().Get_Table_Get_Already())
                {
                    i_Button_Up.GetComponent<Image>().sprite = s_Sample_Get;
                }
                else
                {
                    i_Button_Up.GetComponent<Image>().sprite = s_Sample_Nope;
                }
            }
            else
            //Craft
            if (cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Up()).GetComponent<EG_ClientTable_Craft>() != null)
            {
                i_Button_Up.GetComponent<Image>().sprite = s_Sample_Craft;
            }
        }
        else
        {
            i_Button_Up.GetComponent<Image>().sprite = s_Sample_Move;
        }
    }

    private void Set_MoveUI_Down()
    {
        if (cl_UICraft.Get_CraftShow())
        {
            i_Button_Down.GetComponent<Image>().sprite = s_Sample_Nope;
        }
        else
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
            //Poision
            if (cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Down()).GetComponent<EG_ClientTable_Poision>() != null)
            {
                if (!cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Down()).GetComponent<EG_ClientTable_Poision>().Get_Table_Get_Already())
                {
                    i_Button_Down.GetComponent<Image>().sprite = s_Sample_Get;
                }
                else
                {
                    i_Button_Down.GetComponent<Image>().sprite = s_Sample_Nope;
                }
            }
            else
            //Item
            if (cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Down()).GetComponent<EG_ClientTable_Item>() != null)
            {
                if (!cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Down()).GetComponent<EG_ClientTable_Item>().Get_Table_Get_Already())
                {
                    i_Button_Down.GetComponent<Image>().sprite = s_Sample_Get;
                }
                else
                {
                    i_Button_Down.GetComponent<Image>().sprite = s_Sample_Nope;
                }
            }
            else
            //Craft
            if (cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Down()).GetComponent<EG_ClientTable_Craft>() != null)
            {
                i_Button_Down.GetComponent<Image>().sprite = s_Sample_Craft;
            }
        }
        else
        {
            i_Button_Down.GetComponent<Image>().sprite = s_Sample_Move;
        }
    }

    private void Set_MoveUI_Left()
    {
        if (cl_UICraft.Get_CraftShow())
        {
            i_Button_Left.GetComponent<Image>().sprite = s_Sample_Nope;
        }
        else
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
            //Poision
            if (cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Left()).GetComponent<EG_ClientTable_Poision>() != null)
            {
                if (!cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Left()).GetComponent<EG_ClientTable_Poision>().Get_Table_Get_Already())
                {
                    i_Button_Left.GetComponent<Image>().sprite = s_Sample_Get;
                }
                else
                {
                    i_Button_Left.GetComponent<Image>().sprite = s_Sample_Nope;
                }
            }
            else
            //Item
            if (cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Left()).GetComponent<EG_ClientTable_Item>() != null)
            {
                if (!cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Left()).GetComponent<EG_ClientTable_Item>().Get_Table_Get_Already())
                {
                    i_Button_Left.GetComponent<Image>().sprite = s_Sample_Get;
                }
                else
                {
                    i_Button_Left.GetComponent<Image>().sprite = s_Sample_Nope;
                }
            }
            else
            //Craft
            if (cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Left()).GetComponent<EG_ClientTable_Craft>() != null)
            {
                i_Button_Left.GetComponent<Image>().sprite = s_Sample_Craft;
            }
        }
        else
        {
            i_Button_Left.GetComponent<Image>().sprite = s_Sample_Move;
        }
    }

    private void Set_MoveUI_Right()
    {
        if (cl_UICraft.Get_CraftShow())
        {
            i_Button_Right.GetComponent<Image>().sprite = s_Sample_Nope;
        }
        else
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
            //Poision
            if (cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Right()).GetComponent<EG_ClientTable_Poision>() != null)
            {
                if (!cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Right()).GetComponent<EG_ClientTable_Poision>().Get_Table_Get_Already())
                {
                    i_Button_Right.GetComponent<Image>().sprite = s_Sample_Get;
                }
                else
                {
                    i_Button_Right.GetComponent<Image>().sprite = s_Sample_Nope;
                }
            }
            else
            //Item
            if (cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Right()).GetComponent<EG_ClientTable_Item>() != null)
            {
                if (!cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Right()).GetComponent<EG_ClientTable_Item>().Get_Table_Get_Already())
                {
                    i_Button_Right.GetComponent<Image>().sprite = s_Sample_Get;
                }
                else
                {
                    i_Button_Right.GetComponent<Image>().sprite = s_Sample_Nope;
                }
            }
            else
            //Craft
            if (cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Right()).GetComponent<EG_ClientTable_Craft>() != null)
            {
                i_Button_Right.GetComponent<Image>().sprite = s_Sample_Craft;
            }
        }
        else
        {
            i_Button_Right.GetComponent<Image>().sprite = s_Sample_Move;
        }
    }
}
