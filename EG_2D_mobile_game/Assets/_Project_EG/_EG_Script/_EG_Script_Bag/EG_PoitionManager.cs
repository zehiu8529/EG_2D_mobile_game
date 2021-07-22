using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EG_PoitionManager : MonoBehaviour
{
    /// <summary>
    /// Map Manager
    /// </summary>
    [Header("Isometric Map Tag")]
    [SerializeField]
    private Isometric_MapManager cl_MapManager;

    [Header("Client Manager Tag")]
    [SerializeField]
    private Socket_ClientManager cl_ClientManager;

    [Header("Isometric Client Tag")]
    [SerializeField]
    private string s_Client_Tag = "IsometricClient";

    /// <summary>
    /// Client GameObject
    /// </summary>
    [SerializeField]
    private Isometric_MoveControl cl_ClientControl;

    [SerializeField]
    [Header("Poition Slot")]
    private Text t_Poition_Red;

    /// <summary>
    /// Poition Red
    /// </summary>
    [SerializeField]
    private int i_Poition_Red_Count = 0;

    [SerializeField]
    private Text t_Poition_Blue;

    /// <summary>
    /// Poition Blue
    /// </summary>
    [SerializeField]
    private int i_Poition_Blue_Count = 0;

    [SerializeField]
    private Text t_Poition_Green;

    /// <summary>
    /// Poition Green
    /// </summary>
    [SerializeField]
    private int i_Poition_Green_Count = 0;

    private void Update()
    {
        if (cl_ClientManager.Get_Socket_Start())
        {
            if (cl_ClientControl == null)
            {
                if (s_Client_Tag != "")
                {
                    GameObject g_FindGameObject = GameObject.FindGameObjectWithTag(s_Client_Tag);

                    if (g_FindGameObject != null)
                    {
                        cl_ClientControl = g_FindGameObject.GetComponent<Isometric_MoveControl>();
                    }
                }
            }
            else
            {
                t_Poition_Red.text = i_Poition_Red_Count.ToString();
                t_Poition_Blue.text = i_Poition_Blue_Count.ToString();
                t_Poition_Green.text = i_Poition_Green_Count.ToString();
            }
        }
    }

    public void Button_GetPoition_Up()
    {
        if (cl_ClientManager.Get_Socket_Start())
        {
            if (cl_ClientControl != null)
            {
                if (!cl_ClientControl.Get_CheckMove_Dir_InsideMap(new Class_Vector().v2_Isometric_DirUp))
                {
                    return;
                }
                else
                if (!cl_ClientControl.Get_CheckMove_Dir_Fence(new Class_Vector().v2_Isometric_DirUp))
                {
                    return;
                }
                else
                if (!cl_ClientControl.Get_CheckMove_Dir_Object(new Class_Vector().v2_Isometric_DirUp))
                {
                    if (cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Up()).GetComponent<EG_ClientTable_Poision>() != null)
                    {
                        if (!cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Up()).GetComponent<EG_ClientTable_Poision>().Get_Table_Get_Already())
                        {
                            EG_ClientTable_Poision cl_GetPoition = cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Up()).GetComponent<EG_ClientTable_Poision>();
                            int i_GetPoition_Red = 0, i_GetPoition_Blue = 0, i_GetPoition_Green = 0;
                            cl_GetPoition.Set_Table_Get(out i_GetPoition_Red, out i_GetPoition_Blue, out i_GetPoition_Green);
                            this.i_Poition_Red_Count += i_GetPoition_Red;
                            this.i_Poition_Blue_Count += i_GetPoition_Blue;
                            this.i_Poition_Green_Count += i_GetPoition_Green;
                        }
                    }
                }
            }
        }
    }

    public void Button_GetPoition_Down()
    {
        if (cl_ClientManager.Get_Socket_Start())
        {
            if (cl_ClientControl != null)
            {
                if (!cl_ClientControl.Get_CheckMove_Dir_InsideMap(new Class_Vector().v2_Isometric_DirDown))
                {
                    return;
                }
                else
                if (!cl_ClientControl.Get_CheckMove_Dir_Fence(new Class_Vector().v2_Isometric_DirDown))
                {
                    return;
                }
                else
                if (!cl_ClientControl.Get_CheckMove_Dir_Object(new Class_Vector().v2_Isometric_DirDown))
                {
                    if (cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Down()).GetComponent<EG_ClientTable_Poision>() != null)
                    {
                        if (!cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Down()).GetComponent<EG_ClientTable_Poision>().Get_Table_Get_Already())
                        {
                            EG_ClientTable_Poision cl_GetPoition = cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Down()).GetComponent<EG_ClientTable_Poision>();
                            int i_GetPoition_Red = 0, i_GetPoition_Blue = 0, i_GetPoition_Green = 0;
                            cl_GetPoition.Set_Table_Get(out i_GetPoition_Red, out i_GetPoition_Blue, out i_GetPoition_Green);
                            this.i_Poition_Red_Count += i_GetPoition_Red;
                            this.i_Poition_Blue_Count += i_GetPoition_Blue;
                            this.i_Poition_Green_Count += i_GetPoition_Green;
                        }
                    }
                }
            }
        }
    }

    public void Button_GetPoition_Left()
    {
        if (cl_ClientManager.Get_Socket_Start())
        {
            if (cl_ClientControl != null)
            {
                if (!cl_ClientControl.Get_CheckMove_Dir_InsideMap(new Class_Vector().v2_Isometric_DirLeft))
                {
                    return;
                }
                else
                if (!cl_ClientControl.Get_CheckMove_Dir_Fence(new Class_Vector().v2_Isometric_DirLeft))
                {
                    return;
                }
                else
                if (!cl_ClientControl.Get_CheckMove_Dir_Object(new Class_Vector().v2_Isometric_DirLeft))
                {
                    if (cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Left()).GetComponent<EG_ClientTable_Poision>() != null)
                    {
                        if (!cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Left()).GetComponent<EG_ClientTable_Poision>().Get_Table_Get_Already())
                        {
                            EG_ClientTable_Poision cl_GetPoition = cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Left()).GetComponent<EG_ClientTable_Poision>();
                            int i_GetPoition_Red = 0, i_GetPoition_Blue = 0, i_GetPoition_Green = 0;
                            cl_GetPoition.Set_Table_Get(out i_GetPoition_Red, out i_GetPoition_Blue, out i_GetPoition_Green);
                            this.i_Poition_Red_Count += i_GetPoition_Red;
                            this.i_Poition_Blue_Count += i_GetPoition_Blue;
                            this.i_Poition_Green_Count += i_GetPoition_Green;
                        }
                    }
                }
            }
        }
    }

    public void Button_GetPoition_Right()
    {
        if (cl_ClientManager.Get_Socket_Start())
        {
            if (cl_ClientControl != null)
            {
                if (!cl_ClientControl.Get_CheckMove_Dir_InsideMap(new Class_Vector().v2_Isometric_DirRight))
                {
                    return;
                }
                else
                if (!cl_ClientControl.Get_CheckMove_Dir_Fence(new Class_Vector().v2_Isometric_DirRight))
                {
                    return;
                }
                else
                if (!cl_ClientControl.Get_CheckMove_Dir_Object(new Class_Vector().v2_Isometric_DirRight))
                {
                    if (cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Right()).GetComponent<EG_ClientTable_Poision>() != null)
                    {
                        if (!cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Right()).GetComponent<EG_ClientTable_Poision>().Get_Table_Get_Already())
                        {
                            EG_ClientTable_Poision cl_GetPoition = cl_MapManager.Get_GameObject_Object(cl_ClientControl.Get_PosMoveTo_Right()).GetComponent<EG_ClientTable_Poision>();
                            int i_GetPoition_Red = 0, i_GetPoition_Blue = 0, i_GetPoition_Green = 0;
                            cl_GetPoition.Set_Table_Get(out i_GetPoition_Red, out i_GetPoition_Blue, out i_GetPoition_Green);
                            this.i_Poition_Red_Count += i_GetPoition_Red;
                            this.i_Poition_Blue_Count += i_GetPoition_Blue;
                            this.i_Poition_Green_Count += i_GetPoition_Green;
                        }
                    }
                }
            }
        }
    }

    public int Get_Poision_Red()
    {
        return i_Poition_Red_Count;
    }

    public void Set_Poision_Red_Take(int i_Poision_Red_Take)
    {
        this.i_Poition_Red_Count -= i_Poision_Red_Take;
    }

    public int Get_Poision_Blue()
    {
        return i_Poition_Blue_Count;
    }

    public void Set_Poision_Blue_Take(int i_Poision_Blue_Take)
    {
        this.i_Poition_Blue_Count -= i_Poision_Blue_Take;
    }

    public int Get_Poision_Green()
    {
        return i_Poition_Green_Count;
    }

    public void Set_Poision_Green_Take(int i_Poision_Green_Take)
    {
        this.i_Poition_Green_Count -= i_Poision_Green_Take;
    }
}
