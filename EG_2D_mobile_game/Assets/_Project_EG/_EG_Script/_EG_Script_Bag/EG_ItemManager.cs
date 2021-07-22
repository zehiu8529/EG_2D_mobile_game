using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EG_ItemManager : MonoBehaviour
{
    /// <summary>
    /// Map Manager
    /// </summary>
    [Header("Isometric Map Tag")]
    [SerializeField]
    private Isometric_MapManager cl_MapManager;

    [Header("Client Manager Tag")]
    [SerializeField]
    private Socket_ClientManager cl_ClientManager;

    [Header("Isometric Client Tag")]
    [SerializeField]
    private string s_Client_Tag = "IsometricClient";

    /// <summary>
    /// Client GameObject
    /// </summary>
    [SerializeField]
    private Isometric_MoveControl cl_ClientControl;

    [Header("Item 01 Slot")]
    [SerializeField]
    private Text t_Item_01_Count;

    [SerializeField]
    private Image i_Item_01;

    //[SerializeField]
    private GameObject g_Item_01;

    //[SerializeField]
    private int i_Item_01_Count = 0;

    [SerializeField]
    private GameObject g_ItemStart_01;

    [SerializeField]
    private int i_ItemStart_01_Count = 0;

    [Header("Item 02 Slot")]
    [SerializeField]
    private Text t_Item_02_Count;

    [SerializeField]
    private Image i_Item_02;

    //[SerializeField]
    private GameObject g_Item_02;

    //[SerializeField]
    private int i_Item_02_Count = 0;

    [SerializeField]
    private GameObject g_ItemStart_02;

    [SerializeField]
    private int i_ItemStart_02_Count = 0;

    private Color c_EmtySlot = Color.clear;
    private Color c_GotSlot = Color.white;

    private void Start()
    {
        Set_Item_Add(g_ItemStart_01, i_ItemStart_01_Count);
        Set_Item_Add(g_ItemStart_02, i_ItemStart_02_Count);
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

                    if (g_FindGameObject != null)
                    {
                        cl_ClientControl = g_FindGameObject.GetComponent<Isometric_MoveControl>();
                    }
                }
            }
        }
    }

    /// <summary>
    /// Add Item to Bag
    /// </summary>
    /// <param name="g_ItemAdd"></param>
    public void Set_Item_Add(GameObject g_ItemAdd, int i_NumberAdd)
    {
        if(g_ItemAdd == null)
        {
            return;
        }

        //Item 01
        if (g_Item_01 == null)
        {
            g_Item_01 = g_ItemAdd;
            i_Item_01.sprite = g_ItemAdd.GetComponent<SpriteRenderer>().sprite;
            i_Item_01.color = c_GotSlot;
            i_Item_01_Count = i_NumberAdd;
            t_Item_01_Count.text = 1.ToString();
            return;
        }
        else
        if (g_Item_01.name == g_ItemAdd.name) 
        {
            i_Item_01_Count += i_NumberAdd;
            t_Item_01_Count.text = i_Item_01_Count.ToString();
            return;
        }

        //Item 02
        if (g_Item_02 == null) 
        {
            g_Item_02 = g_ItemAdd;
            i_Item_02.sprite = g_ItemAdd.GetComponent<SpriteRenderer>().sprite;
            i_Item_02.color = c_GotSlot;
            i_Item_02_Count = i_NumberAdd;
            t_Item_02_Count.text = 1.ToString();
            return;
        }
        else
        if (g_Item_02.name == g_ItemAdd.name)
        {
            i_Item_02_Count += i_NumberAdd;
            t_Item_02_Count.text = i_Item_02_Count.ToString();
            return;
        }
    }

    /// <summary>
    /// Use Item 01
    /// </summary>
    public void Button_Item_01()
    {
        if (g_Item_01 != null) 
        {
            Set_UseItem(g_Item_01);

            i_Item_01_Count--;
            t_Item_01_Count.text = i_Item_01_Count.ToString();

            if (i_Item_01_Count == 0) 
            {
                g_Item_01 = null;
                i_Item_01.sprite = null;
                i_Item_01.color = c_EmtySlot;
            }
        }
    }

    /// <summary>
    /// Use Item 02
    /// </summary>
    public void Button_Item_02()
    {
        if (g_Item_02 != null)
        {
            Set_UseItem(g_Item_02);

            i_Item_02_Count--;
            t_Item_02_Count.text = i_Item_02_Count.ToString();

            if (i_Item_02_Count == 0)
            {
                g_Item_02 = null;
                i_Item_02.sprite = null;
                i_Item_02.color = c_EmtySlot;
            }
        }
    }

    /// <summary>
    /// Use Item
    /// </summary>
    /// <param name="g_ItemUse"></param>
    private void Set_UseItem(GameObject g_ItemUse)
    {

    }

    public void Button_GetItem_Up()
    {
        if (cl_ClientManager.Get_Socket_Start())
        {
            if (cl_ClientControl != null)
            {
                if (!cl_ClientControl.Get_CheckMove_Dir_InsideMap(new Class_Vector().v2_Isometric_DirUp))
                {
                    return;
                }
                else
                if (!cl_ClientControl.Get_CheckMove_Dir_Fence(new Class_Vector().v2_Isometric_DirUp))
                {
                    return;
                }
                else
                if (!cl_ClientControl.Get_CheckMove_Dir_Object(new Class_Vector().v2_Isometric_DirUp))
                {
                    if (cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Up()).GetComponent<EG_ClientTable_Item>() != null)
                    {
                        if (!cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Up()).GetComponent<EG_ClientTable_Item>().Get_Table_Get_Already())
                        {
                            EG_ClientTable_Item cl_GetItem = cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Up()).GetComponent<EG_ClientTable_Item>();
                            GameObject g_ItemGet;
                            cl_GetItem.Set_Table_Get(out g_ItemGet);
                            Set_Item_Add(g_ItemGet, 1);
                        }
                    } 
                }
            }
        }
    }

    public void Button_GetItem_Down()
    {
        if (cl_ClientManager.Get_Socket_Start())
        {
            if (cl_ClientControl != null)
            {
                if (!cl_ClientControl.Get_CheckMove_Dir_InsideMap(new Class_Vector().v2_Isometric_DirDown))
                {
                    return;
                }
                else
                if (!cl_ClientControl.Get_CheckMove_Dir_Fence(new Class_Vector().v2_Isometric_DirDown))
                {
                    return;
                }
                else
                if (!cl_ClientControl.Get_CheckMove_Dir_Object(new Class_Vector().v2_Isometric_DirDown))
                {
                    if (cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Down()).GetComponent<EG_ClientTable_Item>() != null)
                    {
                        if (!cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Down()).GetComponent<EG_ClientTable_Item>().Get_Table_Get_Already())
                        {
                            EG_ClientTable_Item cl_GetItem = cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Down()).GetComponent<EG_ClientTable_Item>();
                            GameObject g_ItemGet;
                            cl_GetItem.Set_Table_Get(out g_ItemGet);
                            Set_Item_Add(g_ItemGet, 1);
                        }
                    }
                }
            }
        }
    }

    public void Button_GetItem_Left()
    {
        if (cl_ClientManager.Get_Socket_Start())
        {
            if (cl_ClientControl != null)
            {
                if (!cl_ClientControl.Get_CheckMove_Dir_InsideMap(new Class_Vector().v2_Isometric_DirLeft))
                {
                    return;
                }
                else
                if (!cl_ClientControl.Get_CheckMove_Dir_Fence(new Class_Vector().v2_Isometric_DirLeft))
                {
                    return;
                }
                else
                if (!cl_ClientControl.Get_CheckMove_Dir_Object(new Class_Vector().v2_Isometric_DirLeft))
                {
                    if (cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Left()).GetComponent<EG_ClientTable_Item>() != null)
                    {
                        if (!cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Left()).GetComponent<EG_ClientTable_Item>().Get_Table_Get_Already())
                        {
                            EG_ClientTable_Item cl_GetItem = cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Left()).GetComponent<EG_ClientTable_Item>();
                            GameObject g_ItemGet;
                            cl_GetItem.Set_Table_Get(out g_ItemGet);
                            Set_Item_Add(g_ItemGet, 1);
                        }
                    }
                }
            }
        }
    }

    public void Button_GetItem_Right()
    {
        if (cl_ClientManager.Get_Socket_Start())
        {
            if (cl_ClientControl != null)
            {
                if (!cl_ClientControl.Get_CheckMove_Dir_InsideMap(new Class_Vector().v2_Isometric_DirRight))
                {
                    return;
                }
                else
                if (!cl_ClientControl.Get_CheckMove_Dir_Fence(new Class_Vector().v2_Isometric_DirRight))
                {
                    return;
                }
                else
                if (!cl_ClientControl.Get_CheckMove_Dir_Object(new Class_Vector().v2_Isometric_DirRight))
                {
                    if (cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Right()).GetComponent<EG_ClientTable_Item>() != null)
                    {
                        if (!cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Right()).GetComponent<EG_ClientTable_Item>().Get_Table_Get_Already())
                        {
                            EG_ClientTable_Item cl_GetItem = cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Right()).GetComponent<EG_ClientTable_Item>();
                            GameObject g_ItemGet;
                            cl_GetItem.Set_Table_Get(out g_ItemGet);
                            Set_Item_Add(g_ItemGet, 1);
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Check Item Slot is Full
    /// </summary>
    /// <returns></returns>
    public bool Get_ItemSlot_Full()
    {
        return g_Item_01 != null && g_Item_02 != null;
    }

    /// <summary>
    /// Check Item Exist in Bag Slot
    /// </summary>
    /// <param name="g_ItemCheck"></param>
    /// <returns></returns>
    public bool Get_ItemSlot_Check(GameObject g_ItemCheck)
    {
        if (Get_ItemSlot_Full())
        {
            if (g_Item_01.name == g_ItemCheck.name || g_Item_02.name == g_ItemCheck.name) 
            {
                return true;
            }
        }
        return false;
    }
}
