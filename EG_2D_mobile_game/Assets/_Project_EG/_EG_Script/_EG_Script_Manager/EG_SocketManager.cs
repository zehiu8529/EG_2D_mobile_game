using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EG_SocketManager : MonoBehaviour
{
    #region Public Varible

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
    /// Prepab Local
    /// </summary>
    [Header("Prefab Remote")]
    [SerializeField]
    private GameObject g_Local;

    /// <summary>
    /// Prefab Remote(s)
    /// </summary>
    [SerializeField]
    private GameObject g_Remote;

    [Header("Data Get from Server")]
    //[SerializeField]
    private List<string> l_ID;

    //[SerializeField]
    private List<GameObject> l_Remote;

    #endregion

    #region Private Varible

    //Class

    private Socket_ClientManager cl_ClientManager;

    private Class_Object cl_Object;

    //Queue Push

    private int i_QueuePos = (int)(0.2 / 0.02);
    private int i_QueuePos_Cur = 0;

    //Command

    [HideInInspector]
    public readonly string s_Command_Pos = "Pos";

    [HideInInspector]
    public readonly string s_Command_Table = "Table";

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

        cl_ClientManager = GetComponent<Socket_ClientManager>();

        cl_Object = new Class_Object();

        l_ID = new List<string>();
        l_Remote = new List<GameObject>();
    }

    private void Update()
    {
        if (cl_ClientManager.Get_Socket_Start())
        {
            Set_Auto_GameControl();

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

    private void FixedUpdate()
    {
        Set_Auto_FixPos();
    }

    /// <summary>
    /// Auto Game
    /// </summary>
    private void Set_Auto_GameControl()
    {
        for (int i = 0; i < cl_ClientManager.Get_SocketQueue_Count(); i++)
        //Loop Queue
        {
            string s_DataGet = cl_ClientManager.Get_SocketQueue_Read();

            if (s_DataGet != "")
            //Data Get
            {
                List<string> l_DataGet = new Class_String().Get_String_Split_List(s_DataGet, ':');

                if (l_DataGet[0] == s_Command_Pos)
                //Pos Command
                {
                    Set_Command_Pos(l_DataGet);
                }
                else
                if (l_DataGet[0] == s_Command_Table)
                //Table Command
                {
                    Set_Command_Table(l_DataGet);
                }
            }
        }
    }

    //Command

    #region Command Pos

    /// <summary>
    /// Check and Set Command Pos
    /// </summary>
    /// <param name="l_DataGet"></param>
    private void Set_Command_Pos(List<string> l_DataGet)
    {
        string s_ID = l_DataGet[1];
        int i_x = int.Parse(l_DataGet[2]);
        int i_y = int.Parse(l_DataGet[3]);
        int i_Face = int.Parse(l_DataGet[4]);

        if (s_ID == cl_ClientManager.Get_DeviceID())
        //If ID get Equa this Device ID
        {
            if (!Get_Exist_ID(s_ID))
            //If not Exist this Device on List >> Create Remote of this Device
            {
                GameObject g_NewRemote = cl_Object.Set_Prepab_Create(g_Local, this.transform);
                g_NewRemote.GetComponent<Isometric_Single>().Set_Isometric_PosOnMap(i_x, i_y);
                g_NewRemote.GetComponent<Isometric_MoveControl>().Set_FaceRight(i_Face);
                l_ID.Add(s_ID);
                l_Remote.Add(g_NewRemote);
            }
        }
        else
        //If ID get NOT Equa this Device ID
        {
            if (!Get_Exist_ID(s_ID))
            //If not Exist this Device on List >> Create Remote
            {
                GameObject g_NewRemote = cl_Object.Set_Prepab_Create(g_Remote, this.transform);
                g_NewRemote.GetComponent<Isometric_Single>().Set_Isometric_PosOnMap(i_x, i_y);
                g_NewRemote.GetComponent<Isometric_MoveControl>().Set_FaceRight(i_Face);
                l_ID.Add(s_ID);
                l_Remote.Add(g_NewRemote);
            }
            else
            //If Exist this Device on List >> Control Remote
            {
                int i_Index = Get_Exist_ID_Index(s_ID);

                if (l_Remote[i_Index].GetComponent<Isometric_MoveControl>().Get_PosStandOn() != new Vector2Int(i_x, i_y))
                {
                    l_Remote[i_Index].GetComponent<Isometric_MoveControl>().Set_PosMoveTo_Pos(new Vector2Int(i_x, i_y));
                }
                l_Remote[i_Index].GetComponent<Isometric_MoveControl>().Set_FaceRight(i_Face);
            }
        }
    }

    #endregion

    #region Command Table

    /// <summary>
    /// Check and Set Command Table
    /// </summary>
    /// <param name="l_DataGet"></param>
    private void Set_Command_Table(List<string> l_DataGet)
    {
        int i_x = int.Parse(l_DataGet[1]);
        int i_y = int.Parse(l_DataGet[2]);

        cl_MapManager.Get_GameObject_Object(new Vector2Int(i_x, i_y)).GetComponent<EG_ClientTable>().Set_Table_Get_Aldready();
    }

    #endregion

    //Auto

    #region Auto Fix

    //Pos

    /// <summary>
    /// Auto Fix Pos to other Client(s)
    /// </summary>
    private void Set_Auto_FixPos()
    {
        if (cl_ClientManager.b_SocketStart)
        {
            if (cl_Client_MoveControl != null) 
            {
                if (i_QueuePos_Cur > 0)
                {
                    i_QueuePos_Cur--;
                }
                else
                {
                    Set_Reset_QueuePos();

                    List<string> l_Data = new List<string>();
                    //0
                    l_Data.Add(s_Command_Pos);
                    //1
                    l_Data.Add(cl_ClientManager.Get_DeviceID());
                    //2
                    l_Data.Add(cl_Client_MoveControl.Get_PosStandOn().x.ToString());
                    //3
                    l_Data.Add(cl_Client_MoveControl.Get_PosStandOn().y.ToString());
                    //4
                    l_Data.Add(cl_Client_MoveControl.Get_FaceRight_Int().ToString());

                    Class_String cl_String = new Class_String();

                    string s_Data = cl_String.Get_StringData_Encypt(l_Data, ':');

                    cl_ClientManager.Set_Socket_Write(s_Data);
                }
            }
        }
    }

    /// <summary>
    /// Auto Reset Pos Fixed
    /// </summary>
    public void Set_Reset_QueuePos()
    {
        this.i_QueuePos_Cur = this.i_QueuePos;
    }

    #endregion

    //List

    #region List Manager

    /// <summary>
    /// Get If ID Exist in List
    /// </summary>
    /// <param name="s_IDCheck"></param>
    /// <returns></returns>
    private bool Get_Exist_ID(string s_IDCheck)
    {
        for (int i = 0; i < l_ID.Count; i++)
        {
            if (l_ID[i] == s_IDCheck)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Get Index of ID Exist in List
    /// </summary>
    /// <param name="s_IDCheck"></param>
    /// <returns></returns>
    private int Get_Exist_ID_Index(string s_IDCheck)
    {
        for (int i = 0; i < l_ID.Count; i++)
        {
            if (l_ID[i] == s_IDCheck)
            {
                return i;
            }
        }
        return -1;
    }

    #endregion
}
