using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EG_MoveManager : MonoBehaviour
{
    #region Public Varible

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
    private Isometric_MoveControl cl_Client_MoveControl;

    #endregion

    #region Private Varible

    private Socket_ClientManager cl_ClientManager;

    private EG_SocketManager cl_EGSocketManager;

    private bool b_JoinGameSend = false;

    #endregion

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

        cl_EGSocketManager = GetComponent<EG_SocketManager>();
    }

    private void Update()
    {
        if (cl_ClientManager.Get_Socket_Start())
        {
            if (!b_JoinGameSend)
            {
                Set_JoinGame();
            }
            if (cl_Client_MoveControl == null)
            {
                if (s_Client_Tag != "")
                {
                    GameObject g_FindGameObject = GameObject.FindGameObjectWithTag(s_Client_Tag);

                    if (g_FindGameObject != null)
                    {
                        cl_Client_MoveControl = g_FindGameObject.GetComponent<Isometric_MoveControl>();
                    }
                }
            }
        }
    }

    //Button Control Move

    /// <summary>
    /// Button Move Up for Client
    /// </summary>
    public void Button_Up()
    {
        if (cl_Client_MoveControl == null)
        {
            return;
        }
        cl_Client_MoveControl.Set_PosMoveTo_Up();
    }

    /// <summary>
    /// Button Move Down for Client
    /// </summary>
    public void Button_Down()
    {
        if (cl_Client_MoveControl == null)
        {
            return;
        }
        cl_Client_MoveControl.Set_PosMoveTo_Down();
    }

    /// <summary>
    /// Button Move Left for Client
    /// </summary>
    public void Button_Left()
    {
        if (cl_Client_MoveControl == null)
        {
            return;
        }
        cl_Client_MoveControl.Set_PosMoveTo_Left();
    }

    /// <summary>
    /// Button Move Right for Client
    /// </summary>
    public void Button_Right()
    {
        if (cl_Client_MoveControl == null)
        {
            return;
        }
        cl_Client_MoveControl.Set_PosMoveTo_Right();
    }

    //Button Join Game

    /// <summary>
    /// Button Join After Socket Start
    /// </summary>
    public void Set_JoinGame()
    {
        Vector2Int v2_Spawm = g_MapManager.GetComponent<Isometric_MapString>().Get_List_SpawmPoint()[0];

        List<string> l_Data = new List<string>();
        //0
        l_Data.Add(cl_EGSocketManager.s_Command_Pos);
        //1
        l_Data.Add(cl_ClientManager.Get_DeviceID());
        //2
        l_Data.Add(v2_Spawm.x.ToString());
        //3
        l_Data.Add(v2_Spawm.y.ToString());

        Class_String cl_String = new Class_String();

        string s_Data = cl_String.Get_StringData_Encypt(l_Data, ':');

        cl_ClientManager.Set_Socket_Write(s_Data);

        b_JoinGameSend = true;
    }
}
