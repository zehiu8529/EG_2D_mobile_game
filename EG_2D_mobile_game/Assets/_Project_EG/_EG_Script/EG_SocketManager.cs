using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EG_SocketManager : MonoBehaviour
{
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
    [SerializeField]
    private List<string> l_ID;

    [SerializeField]
    private List<GameObject> l_Remote;

    private Socket_ClientManager cl_ClientManager;

    private Class_Object cl_Object;

    private const string s_Command_Pos = "Pos";

    private void Start()
    {
        cl_ClientManager = GetComponent<Socket_ClientManager>();

        cl_Object = new Class_Object();
    }

    private void Update()
    {
        if (cl_ClientManager.Get_SocketStart())
        {
            if (cl_ClientControl == null)
            {
                if (s_Client_Tag != "")
                {
                    cl_ClientControl = GameObject.FindGameObjectWithTag(s_Client_Tag).GetComponent<Isometric_MoveControl>();
                }
            }
        }
    }

    private void FixedUpdate()
    {
        Set_AutoSocket();

        cl_ClientManager.Set_Socket_Write("");
        //Fixed Data send to Host
    }

    private void Set_AutoSocket()
    {
        if (cl_ClientManager.Get_Socket_Queue_Read_Exist())
        {
            string s_DataGet = cl_ClientManager.Get_Socket_Queue_Read();
            List<string> l_Data = cl_ClientManager.Get_SocketData(s_DataGet);
            string s_ID = l_Data[0];
            //string s_Control = cl_SocketManager.Get_SocketData_First(s_Command);
            
            int i_x = int.Parse(l_Data[1]);
            int i_y = int.Parse(l_Data[2]);

            if (s_ID == cl_ClientManager.Get_DeviceID())
            //If ID get Equa this Device ID
            {
                if (!Get_Exist_ID(s_ID))
                //If not Exist this Device on List >> Create Remote of this Device
                {
                    GameObject g_NewRemote = cl_Object.Set_Prepab_Create(g_Local, this.transform);
                    g_NewRemote.GetComponent<Isometric_Single>().Set_Pos(i_x, i_y);
                    l_ID.Add(s_ID);
                    l_Remote.Add(g_NewRemote);

                    cl_ClientManager.Set_Socket_Write(
                        cl_ClientManager.Get_DeviceID() + ":" +
                        cl_ClientControl.Get_PosMoveTo().x + ":" +
                        cl_ClientControl.Get_PosMoveTo().y);
                    //Send my Pos to the new player join
                }
                else
                //If Exist this Device on List >> Control Remote of this Device
                {
                    int i_Index = Get_Exist_ID_Index(s_ID);

                    //if (s_Control == s_Command_Pos)
                    ////If Command of Remote is Pos Move To >> Set Pos Move to
                    //{

                    //}

                    l_Remote[i_Index].GetComponent<Isometric_MoveControl>().Set_PosMoveTo_Pos(new Vector2Int(i_x, i_y));
                }
            }
            else
            //If ID get NOT Equa this Device ID
            {
                if (!Get_Exist_ID(s_ID))
                //If not Exist this Device on List >> Create Remote
                {
                    GameObject g_NewRemote = cl_Object.Set_Prepab_Create(g_Remote, this.transform);
                    g_NewRemote.GetComponent<Isometric_Single>().Set_Pos(i_x, i_y);
                    l_ID.Add(s_ID);
                    l_Remote.Add(g_NewRemote);

                    cl_ClientManager.Set_Socket_Write(
                        cl_ClientManager.Get_DeviceID() + ":" +
                        cl_ClientControl.Get_PosMoveTo().x + ":" +
                        cl_ClientControl.Get_PosMoveTo().y);
                    //Send my Pos to the new player join
                }
                else
                //If Exist this Device on List >> Control Remote
                {
                    int i_Index = Get_Exist_ID_Index(s_ID);

                    //    if (s_Control == s_Command_Pos)
                    //    //If Command of Remote is Pos Move To >> Set Pos Move to
                    //    {

                    //    }

                    l_Remote[i_Index].GetComponent<Isometric_MoveControl>().Set_PosMoveTo_Pos(new Vector2Int(i_x, i_y));
                }
            }
        }
    }

    #region List Manager

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
