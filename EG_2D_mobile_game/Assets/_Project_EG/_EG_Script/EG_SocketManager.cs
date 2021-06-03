using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EG_SocketManager : MonoBehaviour
{
    [SerializeField]
    private List<string> l_ID;

    [SerializeField]
    private List<GameObject> l_Remote;

    private Socket_ClientManager cl_SocketManager;

    private const string s_Command_Pos = "Pos";

    private void Start()
    {
        cl_SocketManager = GetComponent<Socket_ClientManager>();
    }

    private void Update()
    {
        if (cl_SocketManager.Get_Socket_Queue_Read_Exist())
        {
            string s_DataGet = cl_SocketManager.Get_Socket_Queue_Read();
            string s_ID = cl_SocketManager.Get_SocketData_First(s_DataGet);
            string s_Command = cl_SocketManager.Get_SocketData_Second(s_DataGet);
            string s_Control = cl_SocketManager.Get_SocketData_First(s_Command);

            if (s_ID == cl_SocketManager.Get_DeviceID())
            //If ID get Equa this Device ID
            {
                if (!Get_Exist_ID(s_ID))
                //If not Exist this Device on List >> Create Remote of this Device
                {

                }
                else
                //If Exist this Device on List >> Control Remote of this Device
                {
                    int i_Index = Get_Exist_ID_Index(s_ID);

                    if(s_Control == s_Command_Pos)
                    //If Command of Remote is Pos Move To >> Set Pos Move to
                    {

                    }
                }
            }
            else
            //If ID get NOT Equa this Device ID
            {
                if (!Get_Exist_ID(s_ID))
                //If not Exist this Device on List >> Create Remote
                {

                }
                else
                //If Exist this Device on List >> Control Remote
                {
                    int i_Index = Get_Exist_ID_Index(s_ID);

                    if (s_Control == s_Command_Pos)
                    //If Command of Remote is Pos Move To >> Set Pos Move to
                    {

                    }
                }
            }
        }

        
    }

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

}
