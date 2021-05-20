using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EG_Player_Poision : MonoBehaviour
{
    [Header("Poision Blue")]
    [SerializeField]
    private int i_Poision_Blue = 0;

    [SerializeField]
    private Text t_Poision_Blue;

    [Header("Poision Red")]
    [SerializeField]
    private int i_Poision_Red = 0;

    [SerializeField]
    private Text t_Poision_Red;

    [Header("Poision Green")]
    [SerializeField]
    private int i_Poision_Green = 0;

    [SerializeField]
    private Text t_Poision_Green;

    [Header("Table Poision")]
    [SerializeField]
    private List<char> l_TablePoisionCode;

    private Isometric_Move cl_Move;

    private void Start()
    {
        cl_Move = GetComponent<Isometric_Move>();
    }

    private void Update()
    {
        t_Poision_Blue.text = i_Poision_Blue.ToString();
        t_Poision_Red.text = i_Poision_Red.ToString();
        t_Poision_Green.text = i_Poision_Green.ToString();
    }

    //Check Table Poision

    /// <summary>
    /// Get Check if Table Poision nearby
    /// </summary>
    /// <returns></returns>
    public bool Get_Check_Poision(Vector2Int v2_Dir)
    {
        for(int i = 0; i < l_TablePoisionCode.Count; i++)
        {
            if (l_TablePoisionCode[i] == cl_Move.Get_Map_ObjectCode(v2_Dir))
                return true;
        }
        return false;
    }

    //Action on Table Poision

    /// <summary>
    /// Get Table Isometric Object
    /// </summary>
    /// <returns></returns>
    public GameObject Get_GameObject_TablePoision(Vector2Int v2_Dir)
    {
        return cl_Move.Get_GameObject_Map_Object(v2_Dir);
    }

    //Check on Table Poision

    public bool Get_Check_TablePoision_Emty(Vector2Int v2_Dir)
    {
        return Get_GameObject_TablePoision(v2_Dir).GetComponent<EG_Table_Poision>().Get_PoisionEmty();
    }

    //Button Table Poision

    /// <summary>
    /// Button Table Poision Action
    /// </summary>
    public void Button_TablePoision()
    {
        if (Get_Check_Poision(cl_Move.Get_Dir_Up()))
        {
            if (Get_GameObject_TablePoision(cl_Move.Get_Dir_Up()).GetComponent<EG_Table_Poision>() != null)
            {
                int i_PoisonTake_Blue = 0, i_PoisionTake_Red = 0, i_PoisionTake_Green = 0;

                Get_GameObject_TablePoision(cl_Move.Get_Dir_Up()).GetComponent<EG_Table_Poision>().Set_Act_PoisionTake(
                    out i_PoisonTake_Blue, out i_PoisionTake_Red, out i_PoisionTake_Green);

                this.i_Poision_Blue += i_PoisonTake_Blue;
                this.i_Poision_Red += i_PoisionTake_Red;
                this.i_Poision_Green += i_PoisionTake_Green;
            }
        }
    }

    /// <summary>
    /// Button Table Poision Action
    /// </summary>
    public void Button_TablePoision_Down()
    {
        if (Get_Check_Poision(cl_Move.Get_Dir_Down()))
        {
            if (Get_GameObject_TablePoision(cl_Move.Get_Dir_Down()).GetComponent<EG_Table_Poision>() != null)
            {
                int i_PoisonTake_Blue = 0, i_PoisionTake_Red = 0, i_PoisionTake_Green = 0;

                Get_GameObject_TablePoision(cl_Move.Get_Dir_Down()).GetComponent<EG_Table_Poision>().Set_Act_PoisionTake(
                    out i_PoisonTake_Blue, out i_PoisionTake_Red, out i_PoisionTake_Green);

                this.i_Poision_Blue += i_PoisonTake_Blue;
                this.i_Poision_Red += i_PoisionTake_Red;
                this.i_Poision_Green += i_PoisionTake_Green;
            }
        }
    }

    /// <summary>
    /// Button Table Poision Action
    /// </summary>
    public void Button_TablePoision_Left()
    {
        if (Get_Check_Poision(cl_Move.Get_Dir_Left()))
        {
            if (Get_GameObject_TablePoision(cl_Move.Get_Dir_Left()).GetComponent<EG_Table_Poision>() != null)
            {
                int i_PoisonTake_Blue = 0, i_PoisionTake_Red = 0, i_PoisionTake_Green = 0;

                Get_GameObject_TablePoision(cl_Move.Get_Dir_Left()).GetComponent<EG_Table_Poision>().Set_Act_PoisionTake(
                    out i_PoisonTake_Blue, out i_PoisionTake_Red, out i_PoisionTake_Green);

                this.i_Poision_Blue += i_PoisonTake_Blue;
                this.i_Poision_Red += i_PoisionTake_Red;
                this.i_Poision_Green += i_PoisionTake_Green;
            }
        }
    }

    /// <summary>
    /// Button Table Poision Action
    /// </summary>
    public void Button_TablePoision_Right()
    {
        if (Get_Check_Poision(cl_Move.Get_Dir_Right()))
        {
            if (Get_GameObject_TablePoision(cl_Move.Get_Dir_Right()).GetComponent<EG_Table_Poision>() != null)
            {
                int i_PoisonTake_Blue = 0, i_PoisionTake_Red = 0, i_PoisionTake_Green = 0;

                Get_GameObject_TablePoision(cl_Move.Get_Dir_Right()).GetComponent<EG_Table_Poision>().Set_Act_PoisionTake(
                    out i_PoisonTake_Blue, out i_PoisionTake_Red, out i_PoisionTake_Green);

                this.i_Poision_Blue += i_PoisonTake_Blue;
                this.i_Poision_Red += i_PoisionTake_Red;
                this.i_Poision_Green += i_PoisionTake_Green;
            }
        }
    }
}
