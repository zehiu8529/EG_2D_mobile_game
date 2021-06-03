using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class Socket_ClientManager : MonoBehaviour
{
    #region Public Varible

    /// <summary>
    /// Tag for other Socket Object to Find
    /// </summary>
    [Header("Client Manager Tag")]
    [SerializeField]
    private string s_Tag = "ClientManager";

    /// <summary>
    /// Host to Connect
    /// </summary>
    [Header("Network Server")]
    [SerializeField]
    private InputField inp_Host;

    /// <summary>
    /// Port for Connect to Host
    /// </summary>
    [SerializeField]
    private InputField inp_Port;

    #endregion

    #region Private Varible

    //Socket

    /// <summary>
    /// IP on this Device or this Server
    /// </summary>
    private String s_LocalHost = "localhost";

    /// <summary>
    /// Socket Connect OK?
    /// </summary>
    internal Boolean b_SocketStart = false;

    private TcpClient tcp_Socket;
    private NetworkStream net_Stream;
    private StreamWriter st_Writer;
    private StreamReader st_Reader;

    //Data

    /// <summary>
    /// Stack of Data get from Server
    /// </summary>
    [Header("Data Get from Server")]
    [SerializeField]
    private List<string> l_Data_Queue;

    /// <summary>
    /// Readline of Auto Read Data Fer Frame
    /// </summary>
    private int i_ReadlinePerFrame = 4;

    public void Set_ReadlinePerFrame(int i_ReadlinePerFrame)
    {
        this.i_ReadlinePerFrame = i_ReadlinePerFrame;
    }

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

        l_Data_Queue = new List<string>();

        if (inp_Host != null)
        {
            inp_Host.text = "192.168.100.38";
        }

        if (inp_Port != null)
        {
            inp_Port.text = "5000";
        }
    }

    private void Update()
    {
        for (int i = 0; i < i_ReadlinePerFrame; i++)
        {
            Set_Socket_Auto_Read();
        }
    }

    private void OnApplicationQuit()
    {
        Set_Socket_Close();
    }

    #region Start Connect to Server

    /// <summary>
    /// Set Socket Ready
    /// </summary>
    public void Set_Socket_Start()
    {
        if (!b_SocketStart)
        {
            try
            {
                if (inp_Host == null || inp_Port == null)
                {
                    Debug.LogError("Set_Socket_Start: Require Input Field!");
                    return;
                }
                else
                {
                    if (inp_Port.text == "")
                    {
                        Debug.LogWarning("Set_Socket_Start: Port Require!");
                        return;
                    }

                    if (inp_Host.text == "")
                    {
                        Debug.LogWarning("Set_Socket_Start: Local Host Instead!");
                        tcp_Socket.Connect(s_LocalHost, int.Parse(inp_Port.text));
                    }
                    else
                    {
                        Debug.LogWarning("Set_Socket_Start: " + inp_Host.text + " Connecting!");
                        Debug.LogWarning("Set_Socket_Start: Device " + SystemInfo.deviceUniqueIdentifier);
                        tcp_Socket.Connect(inp_Host.text, int.Parse(inp_Port.text));
                    }
                }

                net_Stream = tcp_Socket.GetStream();
                net_Stream.ReadTimeout = 1;
                st_Writer = new StreamWriter(net_Stream);
                st_Reader = new StreamReader(net_Stream);
                b_SocketStart = true;

                Debug.LogWarning("Set_Socket_Start: Socket Start!");
            }
            catch (Exception e)
            {
                Debug.LogError("Client: Socket error '" + e + "'");
            }
        }
    }

    /// <summary>
    /// Get ID of this Device
    /// </summary>
    /// <returns></returns>
    public string Get_DeviceID()
    {
        return SystemInfo.deviceUniqueIdentifier;
    }

    #endregion

    #region Write Data to Server

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

        Debug.Log("Set_Socket_Write: " + s_Data);
    }

    #endregion

    #region Read Data from Server

    //Public

    /// <summary>
    /// Get Data Queue Read from Server add Local
    /// </summary>
    /// <returns></returns>
    public string Get_Socket_Queue_Read()
    {
        if (!Get_Socket_Queue_Read_Exist())
        {
            return "";
        }
        string s_DataGet = l_Data_Queue[0];
        l_Data_Queue.RemoveAt(0);
        return s_DataGet;
    }

    /// <summary>
    /// Get Data Read Queue from Server
    /// </summary>
    /// <returns></returns>
    public bool Get_Socket_Queue_Read_Exist()
    {
        return l_Data_Queue.Count > 0;
    }

    //Private

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
        string s_ReadData = Get_Socket_Read();
        if (!s_ReadData.Equals(""))
        {
            l_Data_Queue.Add(s_ReadData);

            Debug.Log("Set_Socket_Auto_Read: " + s_ReadData);
        }
    }

    #endregion

    #region Close Connect

    /// <summary>
    /// Close Connect to Server
    /// </summary>
    public void Set_Socket_Close()
    {
        if (!b_SocketStart)
            return;
        Set_Socket_Write("Exit");
        st_Writer.Close();
        st_Reader.Close();
        tcp_Socket.Close();
        b_SocketStart = false;

        Debug.LogWarning("Set_Socket_Close: Called!");
    }

    #endregion

    #region Data Get

    /// <summary>
    /// Get FISRT Command in long Command
    /// </summary>
    /// <param name="s_SocketDataGet"></param>
    /// <returns></returns>
    public string Get_SocketData_First(string s_SocketDataGet)
    {
        string s_ID = "";
        for (int i = 0; i < s_SocketDataGet.Length; i++)
        {
            if (s_SocketDataGet[i] != ':')
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

    /// <summary>
    /// Get ANOTHER Command in long Command after FIRST Command
    /// </summary>
    /// <param name="s_SocketDataGet"></param>
    /// <returns></returns>
    public string Get_SocketData_Second(string s_SocketDataGet)
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

    #endregion
}