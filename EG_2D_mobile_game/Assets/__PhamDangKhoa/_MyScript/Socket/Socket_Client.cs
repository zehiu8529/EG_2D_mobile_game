using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Socket_Client : MonoBehaviour
{
    [Header("Client Manager Tag")]
    [SerializeField]
    private string s_Tag = "ClientManager";

    [SerializeField]
    private Socket_ClientManager cl_Client;

    private void Start()
    {
        if (cl_Client == null)
        {
            if (s_Tag != "")
            {
                cl_Client = GameObject.FindGameObjectWithTag(s_Tag).GetComponent<Socket_ClientManager>();

                if (cl_Client == null)
                {
                    Debug.LogError("Socket_Client: 'Socket_ClientManager' not found!");
                }
            }
        }
    }

    //Read

    /// <summary>
    /// Get Data from Server
    /// </summary>
    /// <returns></returns>
    public string Get_Socket_Read()
    {
        string s_Data = cl_Client.Get_Socket_Queue_Read();
        if (!s_Data.Equals(""))
        {
            Debug.Log("Socket: Read '" + s_Data + "'");
        }
        return s_Data;
    }

    /// <summary>
    /// Check Data from Server
    /// </summary>
    /// <param name="s_DataCheck"></param>
    /// <returns></returns>
    public bool Get_Socket_Read(string s_DataCheck)
    {
        string s_Data = cl_Client.Get_Socket_Queue_Read();
        bool s_Check = s_Data.Equals(s_DataCheck);
        if (!s_Data.Equals(""))
        {
            Debug.Log("Socket: Read '" + s_DataCheck + "' <> '" + s_Data + "' is " + s_Check);
        }
        return s_Check;
    }

    //Write

    /// <summary>
    /// Write Data to Server
    /// </summary>
    /// <param name="s_Data"></param>
    public void Set_Socket_Write(string s_Data)
    {
        cl_Client.Set_Socket_Write(true, s_Data);
    }
}
