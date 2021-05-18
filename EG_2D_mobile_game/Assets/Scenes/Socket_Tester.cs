using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socket_Tester : MonoBehaviour
{
    private Socket_Client cl_Client;
    private Control3D_JumpSingle cl_Jump;
    private Control3D_MoveSurface cl_Move;

    private void Start()
    {
        cl_Client = GetComponent<Socket_Client>();
        cl_Jump = GetComponent<Control3D_JumpSingle>();
        cl_Move = GetComponent<Control3D_MoveSurface>();
    }

    private void Update()
    {
        if (cl_Client.Get_Socket_Read("JUMP"))
            cl_Jump.Set_Jump();

        if (cl_Client.Get_Socket_Read("UP"))
            cl_Move.Set_Move_Forward();
        else
        if (cl_Client.Get_Socket_Read("DOWN"))
            cl_Move.Set_Move_Backward();
        else
            cl_Move.Set_Stop_Z();

        if (cl_Client.Get_Socket_Read("LEFT"))
            cl_Move.Set_Move_Left();
        else
        if (cl_Client.Get_Socket_Read("RIGHT"))
            cl_Move.Set_Move_Right();
        else
            cl_Move.Set_Stop_X();
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            cl_Client.Set_Socket_Write("JUMP");

        if (Input.GetKey(KeyCode.UpArrow))
            cl_Client.Set_Socket_Write("UP");
        
        if (Input.GetKey(KeyCode.DownArrow))
            cl_Client.Set_Socket_Write("DOWN");
        
        if (Input.GetKey(KeyCode.LeftArrow))
            cl_Client.Set_Socket_Write("LEFT");
        
        if (Input.GetKey(KeyCode.RightArrow))
            cl_Client.Set_Socket_Write("RIGHT");
    }
}
