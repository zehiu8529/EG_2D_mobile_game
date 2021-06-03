using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample_SocketHandle : MonoBehaviour
{
    [SerializeField]
    private Socket_ClientManager cl_SocketManager;

    [SerializeField]
    private List<string> l_ID;

    [SerializeField]
    private List<string> l_Message;

    private int i_Plus = 0;

    private void Start()
    {
        l_ID = new List<string>();
        l_Message = new List<string>();
    }

    private void Update()
    {
        if (cl_SocketManager.Get_Socket_Queue_Read_Exist())
        {
            string s_SocketGet = cl_SocketManager.Get_Socket_Queue_Read();
            string s_ID = cl_SocketManager.Get_SocketData_First(s_SocketGet);
            string s_Command = cl_SocketManager.Get_SocketData_Second(s_SocketGet);
            if (Get_Exist_ID(s_ID))
            {
                int i_Index = Get_Exist_ID_Index(s_ID);
                l_Message[i_Index] = s_Command;
            }
            else
            {
                l_ID.Add(s_ID);
                l_Message.Add(s_Command);
                //cl_SocketManager.Set_Get(l_ID.Count);
            }
        }
    }

    public void Button_SendDeviceID()
    {
        cl_SocketManager.Set_Socket_Write(cl_SocketManager.Get_DeviceID());
    }

    private bool Get_Exist_ID(string s_IDCheck)
    {
        for(int i = 0; i < l_ID.Count; i++)
        {
            if(l_ID[i] == s_IDCheck)
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
