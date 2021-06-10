using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sample_SocketHandle : MonoBehaviour
{
    [Header("Socket Manager")]
    [SerializeField]
    private Socket_ClientManager cl_SocketManager;

    [Header("Debug List")]
    [SerializeField]
    private Text t_Debug;

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

    private void FixedUpdate()
    {
        //if (cl_SocketManager.Get_Socket_Queue_Read_Exist())
        //{
        //    string s_SocketGet = cl_SocketManager.Get_Socket_Queue_Read();
        //    Debug.Log("Debug:" + s_SocketGet);
        //    //List<string> l_Data = cl_SocketManager.Get_SocketData(s_SocketGet);
        //    string[] l_Data = s_SocketGet.Split(':');

        //    string s_ID = l_Data[0];
        //    string s_Command = l_Data[1];
        //    Debug.Log("Debug: " + s_ID + "||" + s_Command);

        //    if (Get_Exist_ID(s_ID))
        //    {
        //        int i_Index = Get_Exist_ID_Index(s_ID);
        //        l_Message[i_Index] = s_Command;
        //    }
        //    else
        //    {
        //        l_ID.Add(s_ID);
        //        l_Message.Add(s_Command);
        //        //cl_SocketManager.Set_Get(l_ID.Count);
        //    }

        //    string s_Debug = "";
        //    for(int i = 0; i < l_ID.Count; i++)
        //    {
        //        s_Debug += l_ID[i] + ":" + l_Message[i] + "\n";
        //    }
        //}
    }

    public void Button_SendDeviceID()
    {
        i_Plus++;
        cl_SocketManager.Set_Socket_Write(cl_SocketManager.Get_DeviceID() + ":" + i_Plus.ToString());
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
