using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using UnityEngine;

public class Socket_ClientManager : MonoBehaviour
{
    #region Public Varible

    [Header("Client Manager Tag")]
    [SerializeField]
    private string s_Tag = "ClientManager";

    /// <summary>
    /// If Test Network on this Device
    /// </summary>
    [Header("Network Server")]
    [SerializeField]
    private bool b_LocalHost = true;

    /// <summary>
    /// IP on other Device or other Server
    /// </summary>
    [SerializeField]
    private String s_Host = "192.168.100.38";

    /// <summary>
    /// IP on this Device or this Server
    /// </summary>
    private String s_LocalHost = "localhost";

    /// <summary>
    /// Port on Server
    /// </summary>
    [SerializeField]
    private Int32 i_Port = 5056;

    #endregion

    #region Private Varible

    //Socket

    /// <summary>
    /// Socket Connect OK?
    /// </summary>
    internal Boolean b_SocketStart = false;

    private TcpClient tcp_Socket;
    private NetworkStream net_Stream;
    private StreamWriter st_Writer;
    private StreamReader st_Reader;

    private string s_ReadData = "";

    //Data

    /// <summary>
    /// Step = 0 to Start
    /// </summary>
    private int i_Step = -1;

    /// <summary>
    /// Stack of Data get from Server
    /// </summary>
    [Header("Data Get from Server")]
    [SerializeField]
    private List<string> l_Data;

    #endregion

    private void Awake()
    {
        if (s_Tag != "")
        {
            this.tag = s_Tag;
        }
    }

    private void Start()
    {
        tcp_Socket = new TcpClient();

        if (b_SocketStart)
        {
            Debug.Log("Client: Socket on!");
        }

        l_Data = new List<string>();
    }

    private void Update()
    {
        Set_Socket_Start();
        Set_Socket_Auto_Read();
    }

    private void OnDestroy()
    {
        
    }

    private void OnApplicationQuit()
    {
        if (tcp_Socket != null && tcp_Socket.Connected)
            tcp_Socket.Close();
    }

    //Ready And Start

    /// <summary>
    /// Set Socket Ready
    /// </summary>
    public void Set_Socket_Start()
    {
        if (!b_SocketStart)
        {
            try
            {
                if (b_LocalHost)
                {
                    tcp_Socket.Connect(s_LocalHost, i_Port);
                }
                else
                {
                    tcp_Socket.Connect(s_Host, i_Port);
                }
                net_Stream = tcp_Socket.GetStream();
                net_Stream.ReadTimeout = 1;
                st_Writer = new StreamWriter(net_Stream);
                st_Reader = new StreamReader(net_Stream);
                b_SocketStart = true;
                Debug.LogWarning("Client: Socket Start!");
            }
            catch (Exception e)
            {
                Debug.LogError("Client: Socket error '" + e + "'");
            }
        }
    }
    
    //Write

    /// <summary>
    /// Sent Data to Server
    /// </summary>
    /// <param name="s_Data"></param>
    public void Set_Socket_Write(string s_Data)
    {
        if (!b_SocketStart)
            return;
        String foo = s_Data + "\r\n";
        st_Writer.Write(foo);
        st_Writer.Flush();
        Debug.Log("Client: Send '" + s_Data + "'");
    }

    //Read

    /// <summary>
    /// Get Data from Server
    /// </summary>
    /// <returns></returns>
    private String Get_Socket_Read()
    {
        if (!b_SocketStart)
            return "";
        if (net_Stream.DataAvailable)
            return st_Reader.ReadLine();
        return "";
    }

    /// <summary>
    /// Get Data Auto from Server
    /// </summary>
    private void Set_Socket_Auto_Read()
    {
        s_ReadData = Get_Socket_Read();
        if (!s_ReadData.Equals(""))
        {
            l_Data.Add(s_ReadData);
            Debug.Log("Client: Get '" + s_ReadData + "'");
        }
    }

    /// <summary>
    /// Get Data Read from Server in Local
    /// </summary>
    /// <returns></returns>
    public string Get_Data()
    {
        string s_DataGet = l_Data[0];
        l_Data.RemoveAt(0);
        return s_DataGet;
    }

    //Close

    /// <summary>
    /// Close Connect to Server
    /// </summary>
    public void Set_Socket_Close()
    {
        if (!b_SocketStart)
            return;
        st_Writer.Close();
        st_Reader.Close();
        tcp_Socket.Close();
        b_SocketStart = false;
        Set_Socket_Write("Exit");
        Debug.LogWarning("Client: Socket Close!");
    }
}