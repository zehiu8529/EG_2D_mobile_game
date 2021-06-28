using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EG_MoveManager : MonoBehaviour
{
    #region Public Varible

    /// <summary>
    /// Client GameObject
    /// </summary>
    [SerializeField]
    private Isometric_MoveControl cl_Client_MoveControl;

    #endregion

    #region Private Varible

    private Socket_ClientManager cl_SocketClientManager;

    private EG_SocketManager cl_EGSocketManager;

    private EG_CharacterManager cl_EGCharacterManager;

    #endregion

    private void Start()
    {
        cl_SocketClientManager = GetComponent<Socket_ClientManager>();
        cl_EGSocketManager = GetComponent<EG_SocketManager>();
        cl_EGCharacterManager = GetComponent<EG_CharacterManager>();
    }

    /// <summary>
    /// Add Client for Button Control
    /// </summary>
    /// <param name="cl_Client_MoveControl"></param>
    public void Set_ClientRemoteControl(Isometric_MoveControl cl_Client_MoveControl)
    {
        this.cl_Client_MoveControl = cl_Client_MoveControl;
    }

    //Button Control Move

    /// <summary>
    /// Button Move Up for Client
    /// </summary>
    public void Button_Up()
    {
        if (cl_Client_MoveControl == null)
        {
            return;
        }

        if (cl_Client_MoveControl.Get_CheckMove_Dir(new Class_Vector().v2_Isometric_DirUp))
        {
            Set_SendData_PosAlowMoveTo(cl_Client_MoveControl.Get_PosMoveTo_Up(), cl_Client_MoveControl.Get_FaceRight_Up());
        }
        else
        {
            Set_SendData_PosAlowMoveTo(cl_Client_MoveControl.Get_PosStandOn(), cl_Client_MoveControl.Get_FaceRight_Up());
        }

        cl_Client_MoveControl.Set_PosMoveTo_Up();
    }

    /// <summary>
    /// Button Move Down for Client
    /// </summary>
    public void Button_Down()
    {
        if (cl_Client_MoveControl == null)
        {
            return;
        }

        if (cl_Client_MoveControl.Get_CheckMove_Dir(new Class_Vector().v2_Isometric_DirDown))
        {
            Set_SendData_PosAlowMoveTo(cl_Client_MoveControl.Get_PosMoveTo_Down(), cl_Client_MoveControl.Get_FaceRight_Down());
        }
        else
        {
            Set_SendData_PosAlowMoveTo(cl_Client_MoveControl.Get_PosStandOn(), cl_Client_MoveControl.Get_FaceRight_Down());
        }

        cl_Client_MoveControl.Set_PosMoveTo_Down();
    }

    /// <summary>
    /// Button Move Left for Client
    /// </summary>
    public void Button_Left()
    {
        if (cl_Client_MoveControl == null)
        {
            return;
        }

        if (cl_Client_MoveControl.Get_CheckMove_Dir(new Class_Vector().v2_Isometric_DirLeft))
        {
            Set_SendData_PosAlowMoveTo(cl_Client_MoveControl.Get_PosMoveTo_Left(), cl_Client_MoveControl.Get_FaceRight_Left());
        }
        else
        {
            Set_SendData_PosAlowMoveTo(cl_Client_MoveControl.Get_PosStandOn(), cl_Client_MoveControl.Get_FaceRight_Left());
        }

        cl_Client_MoveControl.Set_PosMoveTo_Left();
    }

    /// <summary>
    /// Button Move Right for Client
    /// </summary>
    public void Button_Right()
    {
        if (cl_Client_MoveControl == null)
        {
            return;
        }

        if (cl_Client_MoveControl.Get_CheckMove_Dir(new Class_Vector().v2_Isometric_DirRight))
        {
            Set_SendData_PosAlowMoveTo(cl_Client_MoveControl.Get_PosMoveTo_Right(), cl_Client_MoveControl.Get_FaceRight_Right());
        }
        else
        {
            Set_SendData_PosAlowMoveTo(cl_Client_MoveControl.Get_PosStandOn(), cl_Client_MoveControl.Get_FaceRight_Right());
        }

        cl_Client_MoveControl.Set_PosMoveTo_Right();
    }

    /// <summary>
    /// Send Client Control
    /// </summary>
    /// <param name="v2_PosAlowMoveTo"></param>
    private void Set_SendData_PosAlowMoveTo(Vector2Int v2_PosAlowMoveTo, int i_FaceAlowToTurn)
    {
        List<string> l_Data = new List<string>();
        //0
        l_Data.Add(cl_EGSocketManager.s_Command_Pos);
        //1
        l_Data.Add(cl_SocketClientManager.Get_DeviceID());
        //2
        l_Data.Add(v2_PosAlowMoveTo.x.ToString());
        //3
        l_Data.Add(v2_PosAlowMoveTo.y.ToString());
        //4
        l_Data.Add(i_FaceAlowToTurn.ToString());
        //5
        l_Data.Add(cl_EGCharacterManager.Get_ClientCharacterChoice().ToString());

        Class_String cl_String = new Class_String();

        string s_Data = cl_String.Get_StringData_Encypt(l_Data, ':');

        cl_SocketClientManager.Set_Socket_Write(s_Data);
    }
}
