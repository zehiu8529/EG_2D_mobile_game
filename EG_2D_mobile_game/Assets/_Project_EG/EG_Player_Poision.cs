using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EG_Player_Poision : MonoBehaviour
{
    [SerializeField]
    private List<char> l_TablePoisionCode;

    private Isometric_Move cl_Move;

    private void Start()
    {
        cl_Move = GetComponent<Isometric_Move>();
    }

    //Check Table Poision

    /// <summary>
    /// Get Check if Table Poision nearby
    /// </summary>
    /// <returns></returns>
    public bool Get_Check_Poision_Up()
    {
        for(int i = 0; i < l_TablePoisionCode.Count; i++)
        {
            if (l_TablePoisionCode[i] == cl_Move.Get_Map_ObjectCode(cl_Move.Get_Dir_Up()))
                return true;
        }
        return false;
    }

    /// <summary>
    /// Get Check if Table Poision nearby
    /// </summary>
    /// <returns></returns>
    public bool Get_Check_Poision_Down()
    {
        for (int i = 0; i < l_TablePoisionCode.Count; i++)
        {
            if (l_TablePoisionCode[i] == cl_Move.Get_Map_ObjectCode(cl_Move.Get_Dir_Down()))
                return true;
        }
        return false;
    }

    /// <summary>
    /// Get Check if Table Poision nearby
    /// </summary>
    /// <returns></returns>
    public bool Get_Check_Poision_Left()
    {
        for (int i = 0; i < l_TablePoisionCode.Count; i++)
        {
            if (l_TablePoisionCode[i] == cl_Move.Get_Map_ObjectCode(cl_Move.Get_Dir_Left()))
                return true;
        }
        return false;
    }

    /// <summary>
    /// Get Check if Table Poision nearby
    /// </summary>
    /// <returns></returns>
    public bool Get_Check_Poision_Right()
    {
        for (int i = 0; i < l_TablePoisionCode.Count; i++)
        {
            if (l_TablePoisionCode[i] == cl_Move.Get_Map_ObjectCode(cl_Move.Get_Dir_Right()))
                return true;
        }
        return false;
    }

    //Action on Table Poision

    public GameObject Get_GameObject_TablePoision_Up()
    {
        return cl_Move.Get_GameObject_Map_Object(cl_Move.Get_Dir_Up());
    }

    public GameObject Get_GameObject_TablePoision_Down()
    {
        return cl_Move.Get_GameObject_Map_Object(cl_Move.Get_Dir_Down());
    }

    public GameObject Get_GameObject_TablePoision_Left()
    {
        return cl_Move.Get_GameObject_Map_Object(cl_Move.Get_Dir_Left());
    }

    public GameObject Get_GameObject_TablePoision_Right()
    {
        return cl_Move.Get_GameObject_Map_Object(cl_Move.Get_Dir_Right());
    }
}
