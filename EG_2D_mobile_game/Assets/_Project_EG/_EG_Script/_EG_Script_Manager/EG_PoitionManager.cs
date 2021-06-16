using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EG_PoitionManager : MonoBehaviour
{
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


    /// <summary>
    /// Poition Red
    /// </summary>
    [Header("Poition Have")]
    [SerializeField]
    private int i_Poition_Red = 0;

    [SerializeField]
    private Text t_Poition_Red;

    /// <summary>
    /// Poition Blue
    /// </summary>
    [SerializeField]
    private int i_Poition_Blue = 0;

    [SerializeField]
    private Text t_Poition_Blue;

    /// <summary>
    /// Poition Green
    /// </summary>
    [SerializeField]
    private int i_Poition_Green = 0;

    [SerializeField]
    private Text t_Poition_Green;

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

                    if (g_FindGameObject != null)
                    {
                        cl_ClientControl = g_FindGameObject.GetComponent<Isometric_MoveControl>();
                    }
                }
            }
            else
            {
                t_Poition_Red.text = i_Poition_Red.ToString();
                t_Poition_Blue.text = i_Poition_Blue.ToString();
                t_Poition_Green.text = i_Poition_Green.ToString();
            }
        }
    }

    public void Button_GetPoition_Up()
    {
        if (cl_ClientManager.Get_Socket_Start())
        {
            if (cl_ClientControl != null)
            {
                if (!cl_ClientControl.Get_CheckMove_Dir_InsideMap(new Class_Isometric().v2_DirUp))
                {
                    return;
                }
                else
                if (!cl_ClientControl.Get_CheckMove_Dir_Fence(new Class_Isometric().v2_DirUp))
                {
                    return;
                }
                else
                if (!cl_ClientControl.Get_CheckMove_Dir_Object(new Class_Isometric().v2_DirUp))
                {
                    if (!cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Up()).GetComponent<EG_ClientTable>().Get_Table_Get_Already())
                    {
                        EG_ClientTable cl_GetPoition = cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Up()).GetComponent<EG_ClientTable>();
                        int i_GetPoition_Red = 0, i_GetPoition_Blue = 0, i_GetPoition_Green = 0;
                        cl_GetPoition.Set_Table_Get(out i_GetPoition_Red, out i_GetPoition_Blue, out i_GetPoition_Green);
                        this.i_Poition_Red += i_GetPoition_Red;
                        this.i_Poition_Blue += i_GetPoition_Blue;
                        this.i_Poition_Green += i_GetPoition_Green;
                    }
                }
            }
        }
    }

    public void Button_GetPoition_Down()
    {
        if (cl_ClientManager.Get_Socket_Start())
        {
            if (cl_ClientControl != null)
            {
                if (!cl_ClientControl.Get_CheckMove_Dir_InsideMap(new Class_Isometric().v2_DirDown))
                {
                    return;
                }
                else
                if (!cl_ClientControl.Get_CheckMove_Dir_Fence(new Class_Isometric().v2_DirDown))
                {
                    return;
                }
                else
                if (!cl_ClientControl.Get_CheckMove_Dir_Object(new Class_Isometric().v2_DirDown))
                {
                    if (!cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Down()).GetComponent<EG_ClientTable>().Get_Table_Get_Already())
                    {
                        EG_ClientTable cl_GetPoition = cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Down()).GetComponent<EG_ClientTable>();
                        int i_GetPoition_Red = 0, i_GetPoition_Blue = 0, i_GetPoition_Green = 0;
                        cl_GetPoition.Set_Table_Get(out i_GetPoition_Red, out i_GetPoition_Blue, out i_GetPoition_Green);
                        this.i_Poition_Red += i_GetPoition_Red;
                        this.i_Poition_Blue += i_GetPoition_Blue;
                        this.i_Poition_Green += i_GetPoition_Green;
                    }
                }
            }
        }
    }

    public void Button_GetPoition_Left()
    {
        if (cl_ClientManager.Get_Socket_Start())
        {
            if (cl_ClientControl != null)
            {
                if (!cl_ClientControl.Get_CheckMove_Dir_InsideMap(new Class_Isometric().v2_DirLeft))
                {
                    return;
                }
                else
                if (!cl_ClientControl.Get_CheckMove_Dir_Fence(new Class_Isometric().v2_DirLeft))
                {
                    return;
                }
                else
                if (!cl_ClientControl.Get_CheckMove_Dir_Object(new Class_Isometric().v2_DirLeft))
                {
                    if (!cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Left()).GetComponent<EG_ClientTable>().Get_Table_Get_Already())
                    {
                        EG_ClientTable cl_GetPoition = cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Left()).GetComponent<EG_ClientTable>();
                        int i_GetPoition_Red = 0, i_GetPoition_Blue = 0, i_GetPoition_Green = 0;
                        cl_GetPoition.Set_Table_Get(out i_GetPoition_Red, out i_GetPoition_Blue, out i_GetPoition_Green);
                        this.i_Poition_Red += i_GetPoition_Red;
                        this.i_Poition_Blue += i_GetPoition_Blue;
                        this.i_Poition_Green += i_GetPoition_Green;
                    }
                }
            }
        }
    }

    public void Button_GetPoition_Right()
    {
        if (cl_ClientManager.Get_Socket_Start())
        {
            if (cl_ClientControl != null)
            {
                if (!cl_ClientControl.Get_CheckMove_Dir_InsideMap(new Class_Isometric().v2_DirRight))
                {
                    return;
                }
                else
                if (!cl_ClientControl.Get_CheckMove_Dir_Fence(new Class_Isometric().v2_DirRight))
                {
                    return;
                }
                else
                if (!cl_ClientControl.Get_CheckMove_Dir_Object(new Class_Isometric().v2_DirRight))
                {
                    if (!cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Right()).GetComponent<EG_ClientTable>().Get_Table_Get_Already())
                    {
                        EG_ClientTable cl_GetPoition = cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Right()).GetComponent<EG_ClientTable>();
                        int i_GetPoition_Red = 0, i_GetPoition_Blue = 0, i_GetPoition_Green = 0;
                        cl_GetPoition.Set_Table_Get(out i_GetPoition_Red, out i_GetPoition_Blue, out i_GetPoition_Green);
                        this.i_Poition_Red += i_GetPoition_Red;
                        this.i_Poition_Blue += i_GetPoition_Blue;
                        this.i_Poition_Green += i_GetPoition_Green;
                    }
                }
            }
        }
    }
}
