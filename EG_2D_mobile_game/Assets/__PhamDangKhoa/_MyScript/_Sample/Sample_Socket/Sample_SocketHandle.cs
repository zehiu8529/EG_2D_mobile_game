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
            string s_ID = Get_Socket_ID(s_SocketGet);
            string s_Command = Get_Socket_Command(s_SocketGet);
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

    private string Get_Socket_ID(string s_SocketDataGet)
    {
        string s_ID = "";
        for (int i = 0; i < s_SocketDataGet.Length; i++) 
        {
            if(s_SocketDataGet[i] != ':')
            {
                s_ID += s_SocketDataGet[i];
            }
            else
            {
                return s_ID;
            }
        }
        Debug.LogError("Get_Socket_ID: Can not Read ID!");
        return "";
    }

    private string Get_Socket_Command(string s_SocketDataGet)
    {
        string s_Command = "";
        int i_Char = -1;
        for (int i = 0; i < s_SocketDataGet.Length; i++)
        {
            if (s_SocketDataGet[i] != ':')
            {
                i_Char++;
            }
            else
            {
                i_Char++;
                break;
            }
        }
        i_Char++;
        for (int i = i_Char; i < s_SocketDataGet.Length; i++)
        {
            if (s_SocketDataGet[i] != ':')
            {
                s_Command += s_SocketDataGet[i];
            }
        }
        return s_Command;
    }

    public void Button_Plus()
    {
        i_Plus++;
        cl_SocketManager.Set_Socket_Write(true, i_Plus.ToString());
    }
}
