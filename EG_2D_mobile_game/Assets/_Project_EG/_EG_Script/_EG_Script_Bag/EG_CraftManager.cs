using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EG_CraftManager : MonoBehaviour
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

    [Header("Craft Pannel")]
    [SerializeField]
    private EG_UICraft cl_UICraft;

    [Header("Craft Item Avaible")]
    [SerializeField]
    private List<GameObject> lg_CraftAvaible;

    private int i_CraftChoice = 0;

    [Header("Craft UI")]
    [SerializeField]
    private Text t_Poision_Red_Require;

    [SerializeField]
    private Text t_Poision_Blue_Require;

    [SerializeField]
    private Text t_Poision_Green_Require;

    [SerializeField]
    private Image i_ItemCraft;

    [SerializeField]
    private Text t_ItemText;

    private EG_PoitionManager cl_PoisionManager;
    private EG_ItemManager cl_ItemManager;
    
    private void Start()
    {
        cl_PoisionManager = GetComponent<EG_PoitionManager>();
        cl_ItemManager = GetComponent<EG_ItemManager>();

        Set_CraftUI(0);
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
    /// Choice Next Craft
    /// </summary>
    public void Button_Craft_Next()
    {
        i_CraftChoice++;
        if (i_CraftChoice > lg_CraftAvaible.Count - 1)
            i_CraftChoice = 0;
        Set_CraftUI(i_CraftChoice);
    }

    /// <summary>
    /// Choice Prev Craft
    /// </summary>
    public void Button_Craft_Prev()
    {
        i_CraftChoice--;
        if (i_CraftChoice < 0)
            i_CraftChoice = lg_CraftAvaible.Count - 1;
        Set_CraftUI(i_CraftChoice);
    }

    /// <summary>
    /// Show Item Imformation
    /// </summary>
    /// <param name="i_CraftChoice"></param>
    private void Set_CraftUI(int i_CraftChoice)
    {
        if(lg_CraftAvaible[i_CraftChoice].GetComponent<EG_ItemCraft>() != null)
        {
            t_Poision_Red_Require.text = lg_CraftAvaible[i_CraftChoice].GetComponent<EG_ItemCraft>().Get_Poision_Red_Require().ToString();
            t_Poision_Blue_Require.text = lg_CraftAvaible[i_CraftChoice].GetComponent<EG_ItemCraft>().Get_Poision_Blue_Require().ToString();
            t_Poision_Green_Require.text = lg_CraftAvaible[i_CraftChoice].GetComponent<EG_ItemCraft>().Get_Poision_Green_Require().ToString();
            t_ItemText.text = lg_CraftAvaible[i_CraftChoice].GetComponent<EG_ItemCraft>().Get_ItemText();
            i_ItemCraft.sprite = lg_CraftAvaible[i_CraftChoice].GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            Debug.LogError("Set_CraftUI: Not found 'EG_ItemCraft.cs' at Index " + i_CraftChoice);
        }
    }

    /// <summary>
    /// Craft Item Choice
    /// </summary>
    public void Button_Craft()
    {
        if(cl_PoisionManager.Get_Poision_Red() < lg_CraftAvaible[i_CraftChoice].GetComponent<EG_ItemCraft>().Get_Poision_Red_Require())
        {
            return;
        }

        if (cl_PoisionManager.Get_Poision_Blue() < lg_CraftAvaible[i_CraftChoice].GetComponent<EG_ItemCraft>().Get_Poision_Blue_Require())
        {
            return;
        }

        if (cl_PoisionManager.Get_Poision_Green() < lg_CraftAvaible[i_CraftChoice].GetComponent<EG_ItemCraft>().Get_Poision_Green_Require())
        {
            return;
        }

        if (cl_ItemManager.Get_ItemSlot_Full())
        {
            if (!cl_ItemManager.Get_ItemSlot_Check(lg_CraftAvaible[i_CraftChoice]))
            {
                return;
            }
        }

        cl_PoisionManager.Set_Poision_Red_Take(lg_CraftAvaible[i_CraftChoice].GetComponent<EG_ItemCraft>().Get_Poision_Red_Require());
        cl_PoisionManager.Set_Poision_Blue_Take(lg_CraftAvaible[i_CraftChoice].GetComponent<EG_ItemCraft>().Get_Poision_Blue_Require());
        cl_PoisionManager.Set_Poision_Green_Take(lg_CraftAvaible[i_CraftChoice].GetComponent<EG_ItemCraft>().Get_Poision_Green_Require());

        cl_ItemManager.Set_Item_Add(lg_CraftAvaible[i_CraftChoice], 1);
    }

    public void Button_CraftActive_Up()
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
                    if (cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Up()).GetComponent<EG_ClientTable_Craft>() != null)
                    {
                        cl_UICraft.Set_CraftActive();
                    }
                }
            }
        }
    }

    public void Button_CraftActive_Down()
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
                    if (cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Down()).GetComponent<EG_ClientTable_Craft>() != null)
                    {
                        cl_UICraft.Set_CraftActive();
                    }
                }
            }
        }
    }

    public void Button_CraftActive_Left()
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
                    if (cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Left()).GetComponent<EG_ClientTable_Craft>() != null)
                    {
                        cl_UICraft.Set_CraftActive();
                    }
                }
            }
        }
    }

    public void Button_CraftActive_Right()
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
                    if (cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Right()).GetComponent<EG_ClientTable_Craft>() != null)
                    {
                        cl_UICraft.Set_CraftActive();
                    }
                }
            }
        }
    }
}
