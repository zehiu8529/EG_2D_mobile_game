using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EG_SocketTest : MonoBehaviour
{
    private Socket_Client cl_Client;
    private Isometric_Move cl_Move;

    private void Start()
    {
        cl_Client = GetComponent<Socket_Client>();
        cl_Move = GetComponent<Isometric_Move>();
    }

    private void Update()
    {
        if (cl_Client.Get_Socket_Read("UP"))
            cl_Move.Set_Move_Up();
        else
        if (cl_Client.Get_Socket_Read("DOWN"))
            cl_Move.Set_Move_Down();
        else
        if (cl_Client.Get_Socket_Read("LEFT"))
            cl_Move.Set_Move_Left();
        else
        if (cl_Client.Get_Socket_Read("RIGHT"))
            cl_Move.Set_Move_Right();

        if (!cl_Client.Get_Socket_Read().Equals(""))
            Debug.Log(cl_Client.Get_Socket_Read());
    }

    private void LateUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            cl_Client.Set_Socket_Write("U " + cl_Move.Get_Pos_PosMoveTo().ToString());
        }
        else
        if (Input.GetKey(KeyCode.S))
        {
            cl_Client.Set_Socket_Write("DOWN");
        }
        else
        if (Input.GetKey(KeyCode.A))
        {
            cl_Client.Set_Socket_Write("LEFT");
        }
        else
        if (Input.GetKey(KeyCode.D))
        {
            cl_Client.Set_Socket_Write("RIGHT");
        }
    }
}
