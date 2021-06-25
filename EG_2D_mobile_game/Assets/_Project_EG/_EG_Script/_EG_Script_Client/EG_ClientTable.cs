using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EG_ClientTable : MonoBehaviour
{
    #region Public Varible

    /// <summary>
    /// Tag for other Isometric Map Manager
    /// </summary>
    [Header("Client Manger Tag")]
    [SerializeField]
    private string s_SocketManager_Tag = "ClientManager";

    [SerializeField]
    private GameObject g_SocketManager;

    /// <summary>
    /// Poition Red
    /// </summary>
    [Header("Poition Get")]
    [SerializeField]
    private int i_Poition_Red = 0;

    /// <summary>
    /// Poition Blue
    /// </summary>
    [SerializeField]
    private int i_Poition_Blue = 0;

    /// <summary>
    /// Poition Green
    /// </summary>
    [SerializeField]
    private int i_Poition_Green = 0;

    #endregion

    #region Private Varible

    //Class

    private EG_SocketManager cl_EGSocketManager;

    private Socket_ClientManager cl_ClientManager;

    private Isometric_Single cl_Single;

    private Animator a_Animator;

    //Get

    /// <summary>
    /// Poition If Get
    /// </summary>
    private bool b_Get = false;

    //Queue Push

    private int i_QueueTable = (int)(1 / 0.02);
    private int i_QueueTable_Cur = 0;

    #endregion

    private void Start()
    {
        cl_Single = GetComponent<Isometric_Single>();

        a_Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (g_SocketManager == null)
        {
            if (s_SocketManager_Tag != "")
            {
                g_SocketManager = GameObject.FindGameObjectWithTag(s_SocketManager_Tag);

                if(g_SocketManager != null)
                {
                    cl_ClientManager = g_SocketManager.GetComponent<Socket_ClientManager>();
                    cl_EGSocketManager = g_SocketManager.GetComponent<EG_SocketManager>();
                }
            }
        }
    }

    private void FixedUpdate()
    {
        Set_Auto_FixTable();
    }

    /// <summary>
    /// Auto Fix Table to other Client(s)
    /// </summary>
    private void Set_Auto_FixTable()
    {
        if (g_SocketManager != null)
        {
            if (cl_ClientManager.Get_Socket_Start())
            {
                if (b_Get)
                {
                    if (i_QueueTable_Cur > 0)
                    {
                        i_QueueTable_Cur--;
                    }
                    else
                    {
                        Set_Reset_QueueTable();

                        List<string> l_Data = new List<string>();
                        //0
                        l_Data.Add(cl_EGSocketManager.s_Command_Table);
                        //1
                        l_Data.Add(cl_Single.Get_Isometric_PosOnMap().x.ToString());
                        //2
                        l_Data.Add(cl_Single.Get_Isometric_PosOnMap().y.ToString());

                        Class_String cl_String = new Class_String();

                        string s_Data = cl_String.Get_StringData_Encypt(l_Data, ':');

                        cl_ClientManager.Set_Socket_Write(s_Data);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Auto Reset Pos Fixed
    /// </summary>
    public void Set_Reset_QueueTable()
    {
        this.i_QueueTable_Cur = this.i_QueueTable;
    }

    /// <summary>
    /// Get Poition from Table
    /// </summary>
    /// <param name="i_Red"></param>
    /// <param name="i_Green"></param>
    /// <param name="i_Blue"></param>
    public void Set_Table_Get(out int i_Red, out int i_Blue, out int i_Green)
    {
        if (!Get_Table_Get_Already())
        {
            b_Get = true;
            a_Animator.SetTrigger("Take");

            i_Red = this.i_Poition_Red;
            i_Green = this.i_Poition_Green;
            i_Blue = this.i_Poition_Blue;
        }
        else
        {
            i_Red = 0;
            i_Green = 0;
            i_Blue = 0;
        }
    }

    /// <summary>
    /// Set Table Get Already
    /// </summary>
    public void Set_Table_Get_Aldready()
    {
        if (!Get_Table_Get_Already())
        {
            b_Get = true;
            a_Animator.SetTrigger("Take");
        }
    }

    /// <summary>
    /// Get Table Get Already
    /// </summary>
    /// <returns></returns>
    public bool Get_Table_Get_Already()
    {
        return b_Get;
    }
}
