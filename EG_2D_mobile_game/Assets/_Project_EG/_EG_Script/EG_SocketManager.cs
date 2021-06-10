using System.Collections;
using System.Collections.Generic;
using System.Threading;
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

    private string s_DeviceID = "";
    private Transform t_Transform;

    private const string s_Command_Pos = "Pos";

    private int i_QueuePush = (int)(0.2 / 0.02);
    private int i_QueuePush_Cur = 0;

    private void Start()
    {
        cl_ClientManager = GetComponent<Socket_ClientManager>();

        cl_Object = new Class_Object();

        s_DeviceID = cl_ClientManager.Get_DeviceID();
        t_Transform = this.transform;
    }

    private void Update()
    {
        Set_Thread_AutoGame();

        if (cl_ClientManager.Get_Socket_Start())
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
        Set_AutoFixed();
    }

    private void Set_Thread_AutoGame()
    {
        if (cl_ClientManager.Get_Socket_Start())
        //Socket Start
        {
            for(int i = 0; i < cl_ClientManager.Get_SocketQueue_Count(); i++)
            //Loop Queue
            {
                string s_DataGet = cl_ClientManager.Get_SocketQueue_Read();
                Debug.Log("Set_Thread_AutoGame: " + s_DataGet);
                if (s_DataGet != "")
                //Data Get
                {
                    List<string> l_Data = new Class_String().Get_String_Split_List(s_DataGet, ':');
                    string s_ID = l_Data[0];

                    int i_x = int.Parse(l_Data[1]);
                    int i_y = int.Parse(l_Data[2]);

                    if (s_ID == s_DeviceID)
                    //If ID get Equa this Device ID
                    {
                        if (!Get_Exist_ID(s_ID))
                        //If not Exist this Device on List >> Create Remote of this Device
                        {
                            GameObject g_NewRemote = cl_Object.Set_Prepab_Create(g_Local, t_Transform);
                            g_NewRemote.GetComponent<Isometric_Single>().Set_Pos(i_x, i_y);
                            l_ID.Add(s_ID);
                            l_Remote.Add(g_NewRemote);

                            cl_ClientManager.Set_Socket_Write(
                                cl_ClientManager.Get_DeviceID() + ":" +
                                cl_ClientControl.Get_PosStandOn().x + ":" +
                                cl_ClientControl.Get_PosStandOn().y);
                            //Send my Pos to the new player join
                        }
                    }
                    else
                    //If ID get NOT Equa this Device ID
                    {
                        if (!Get_Exist_ID(s_ID))
                        //If not Exist this Device on List >> Create Remote
                        {
                            GameObject g_NewRemote = cl_Object.Set_Prepab_Create(g_Remote, t_Transform);
                            g_NewRemote.GetComponent<Isometric_Single>().Set_Pos(i_x, i_y);
                            l_ID.Add(s_ID);
                            l_Remote.Add(g_NewRemote);

                            cl_ClientManager.Set_Socket_Write(
                                cl_ClientManager.Get_DeviceID() + ":" +
                                cl_ClientControl.Get_PosStandOn().x + ":" +
                                cl_ClientControl.Get_PosStandOn().y);
                            //Send my Pos to the new player join
                        }
                        else
                        //If Exist this Device on List >> Control Remote
                        {
                            int i_Index = Get_Exist_ID_Index(s_ID);

                            if (l_Remote[i_Index].GetComponent<Isometric_MoveControl>().Get_PosStandOn() != new Vector2Int(i_x, i_y))
                            {
                                l_Remote[i_Index].GetComponent<Isometric_MoveControl>().Set_PosMoveTo_Pos(new Vector2Int(i_x, i_y));
                            }
                        }
                    }
                } //Data Get
            } //Loop Queue
        } //Socket Start
    }

    private void Set_AutoFixed()
    {
        //Fixed Updated Data for another Client(s)
        if (cl_ClientManager.b_SocketStart)
        {
            //if (cl_ClientControl.Get_Moving())
            //{
            //    cl_ClientManager.Set_Socket_Write(
            //        cl_ClientManager.Get_DeviceID() + ":" +
            //        cl_ClientControl.Get_PosStandOn().x + ":" +
            //        cl_ClientControl.Get_PosStandOn().y);
            //    //Fixed Data send to Host
            //}
            //else
            {
                if (i_QueuePush_Cur > 0)
                {
                    i_QueuePush_Cur--;
                }
                else
                {
                    i_QueuePush_Cur = i_QueuePush;

                    cl_ClientManager.Set_Socket_Write(
                        cl_ClientManager.Get_DeviceID() + ":" +
                        cl_ClientControl.Get_PosStandOn().x + ":" +
                        cl_ClientControl.Get_PosStandOn().y);
                    //Fixed Data send to Host
                }
            }
        }
    }

    public void Set_ResetAutoFixed()
    {
        this.i_QueuePush_Cur = i_QueuePush;
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
