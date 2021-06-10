using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EG_ControlManager : MonoBehaviour
{
    /// <summary>
    /// Tag for other Isometric Object to Find
    /// </summary>
    [Header("Isometric Map Tag")]
    [SerializeField]
    private string s_MapManager_Tag = "IsometricMap";

    /// <summary>
    /// Get MapManager GameObject
    /// </summary>
    [SerializeField]
    private GameObject g_MapManager;

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

    private Socket_ClientManager cl_ClientManager;

    private EG_SocketManager cl_SocketManager;

    private bool b_JoinGameSend = false;
    private void Start()
    {
        if (g_MapManager == null)
        {
            if (s_MapManager_Tag != "")
            {
                g_MapManager = GameObject.FindGameObjectWithTag(s_MapManager_Tag);
            }
        }

        cl_ClientManager = GetComponent<Socket_ClientManager>();

        cl_SocketManager = GetComponent<EG_SocketManager>();
    }

    private void Update()
    {
        if (cl_ClientManager.Get_Socket_Start())
        {
            if (!b_JoinGameSend)
            {
                Set_JoinGame();
            }
            if (cl_ClientControl == null)
            {
                if (s_Client_Tag != "")
                {
                    cl_ClientControl = GameObject.FindGameObjectWithTag(s_Client_Tag).GetComponent<Isometric_MoveControl>();
                }
            }
        }
    }

    /// <summary>
    /// Button Move Up for Client
    /// </summary>
    public void Button_Up()
    {
        if (cl_ClientControl == null)
        {
            return;
        }
        //g_ClientControl.GetComponent<Isometric_MoveControl>().Set_PosMoveTo_Up();

        if (cl_ClientControl.Get_CheckMove_Dir(g_MapManager.GetComponent<Isometric_MapManager>().v2_DirUp))
        {
            //cl_ClientManager.Set_Socket_Write(
            //    cl_ClientManager.Get_DeviceID() + ":" +
            //    cl_ClientControl.Get_PosMoveTo_Up().x + ":" +
            //    cl_ClientControl.Get_PosMoveTo_Up().y);

            cl_ClientControl.Set_PosMoveTo_Up();
        }
    }

    /// <summary>
    /// Button Move Down for Client
    /// </summary>
    public void Button_Down()
    {
        if (cl_ClientControl == null)
        {
            return;
        }
        //g_ClientControl.GetComponent<Isometric_MoveControl>().Set_PosMoveTo_Down();

        if (cl_ClientControl.Get_CheckMove_Dir(g_MapManager.GetComponent<Isometric_MapManager>().v2_DirDown))
        {
            //cl_ClientManager.Set_Socket_Write(
            //    cl_ClientManager.Get_DeviceID() + ":" +
            //    cl_ClientControl.Get_PosMoveTo_Down().x + ":" +
            //    cl_ClientControl.Get_PosMoveTo_Down().y);

            cl_ClientControl.Set_PosMoveTo_Down();
        }
    }

    /// <summary>
    /// Button Move Left for Client
    /// </summary>
    public void Button_Left()
    {
        if (cl_ClientControl == null)
        {
            return;
        }
        //g_ClientControl.GetComponent<Isometric_MoveControl>().Set_PosMoveTo_Left();

        if (cl_ClientControl.Get_CheckMove_Dir(g_MapManager.GetComponent<Isometric_MapManager>().v2_DirLeft))
        {
            //cl_ClientManager.Set_Socket_Write(
            //    cl_ClientManager.Get_DeviceID() + ":" +
            //    cl_ClientControl.Get_PosMoveTo_Left().x + ":" +
            //    cl_ClientControl.Get_PosMoveTo_Left().y);

            cl_ClientControl.Set_PosMoveTo_Left();
        }
    }

    /// <summary>
    /// Button Move Right for Client
    /// </summary>
    public void Button_Right()
    {
        if (cl_ClientControl == null)
        {
            return;
        }
        //g_ClientControl.GetComponent<Isometric_MoveControl>().Set_PosMoveTo_Right();

        if (cl_ClientControl.Get_CheckMove_Dir(g_MapManager.GetComponent<Isometric_MapManager>().v2_DirRight))
        {
            //cl_ClientManager.Set_Socket_Write(
            //    cl_ClientManager.Get_DeviceID() + ":" +
            //    cl_ClientControl.Get_PosMoveTo_Right().x.ToString() + ":" +
            //    cl_ClientControl.Get_PosMoveTo_Right().y.ToString());

            cl_ClientControl.Set_PosMoveTo_Right();
        }
    }

    /// <summary>
    /// Button Join After Socket Start
    /// </summary>
    public void Set_JoinGame()
    {
        Vector2Int v2_Spawm = g_MapManager.GetComponent<Isometric_MapString>().Get_List_SpawmPoint()[0];

        cl_ClientManager.Set_Socket_Write(
            cl_ClientManager.Get_DeviceID() + ":" +
            v2_Spawm.x.ToString() + ":" +
            v2_Spawm.y.ToString());

        b_JoinGameSend = true;
    }
}
