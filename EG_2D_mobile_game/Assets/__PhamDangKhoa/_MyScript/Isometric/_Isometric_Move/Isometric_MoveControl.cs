using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Control Object Move on Map Ground
/// </summary>
[RequireComponent(typeof(Isometric_Single))]
public class Isometric_MoveControl : MonoBehaviour
{
    /// <summary>
    /// Tag for other Isometric Object to Find
    /// </summary>
    [Header("Isometric Map Tag")]
    [SerializeField]
    private string s_Tag = "IsometricMap";

    [SerializeField]
    private GameObject g_MapManager;

    #region Control Move

    [Header("Move on Ground")]
    [SerializeField]
    private bool b_UseScriptControl = true;

    [SerializeField]
    private KeyCode k_Up = KeyCode.UpArrow;

    [SerializeField]
    private KeyCode k_Down = KeyCode.DownArrow;

    [SerializeField]
    private KeyCode k_Left = KeyCode.LeftArrow;

    [SerializeField]
    private KeyCode k_Right = KeyCode.RightArrow;

    /// <summary>
    /// Disable Control while Moving
    /// </summary>
    [Header("Move")]
    [SerializeField]
    private bool b_DelayControl = false;

    /// <summary>
    /// Time Move between Square
    /// </summary>
    [SerializeField]
    [Min(0.2f)]
    private float f_TimeDelay = 0.2f;

    /// <summary>
    /// Percent of Time Move with Time Control Delay
    /// </summary>
    [SerializeField]
    [Range(0.1f, 1f)]
    private float f_MovePercent = 0.5f;

    /// <summary>
    /// Time Move Delay between Square
    /// </summary>
    private float f_MoveTime_Cur = 0f;

    #endregion

    #region Private Varible

    /// <summary>
    /// Add this Script on "2.5D Sprite" with "Ground", "Object" like "Box", "Character", etc...
    /// </summary>
    private Isometric_Single cl_Single;

    /// <summary>
    /// Map Manager of Matrix Map Code and Matrix Map Isometric GameObject
    /// </summary>
    private Isometric_MapManager cl_MapManager_MapManager;

    /// <summary>
    /// Ground Check
    /// </summary>
    private Isometric_CheckGround cl_Ground_Check;

    /// <summary>
    /// Fence Check
    /// </summary>
    private Isometric_MoveFence cl_Fence_Check;

    /// <summary>
    /// Object Check
    /// </summary>
    private Isometric_CheckObject cl_Object_Check;

    /// <summary>
    /// Pos Move To
    /// </summary>
    private Vector2Int v2_PosMoveTo; 

    /// <summary>
    /// Pos Stand On after Move
    /// </summary>
    private Vector2Int v2_PosStandOn;

    /// <summary>
    /// Smooth Damp Velocity x
    /// </summary>
    private float f_Velocity_x = 0.0f;
    /// <summary>
    /// Smooth Damp Velocity y
    /// </summary>
    private float f_Velocity_y = 0.0f;

    /// <summary>
    /// Face Base on Dir Move (Start at Face Right)
    /// </summary>
    private int i_Face = 1;

    #endregion

    private void Start()
    {
        if (g_MapManager == null)
        {
            if (s_Tag != "")
            {
                g_MapManager = GameObject.FindGameObjectWithTag(s_Tag);

                if (g_MapManager == null)
                {
                    Debug.LogError(this.name + ": Not found 'MapManager GameObject' with tag: " + s_Tag);
                }
            }
        }

        cl_Single = GetComponent<Isometric_Single>();

        cl_Ground_Check = GetComponent<Isometric_CheckGround>();
        cl_Fence_Check = GetComponent<Isometric_MoveFence>();
        cl_Object_Check = GetComponent<Isometric_CheckObject>();

        cl_MapManager_MapManager = g_MapManager.GetComponent<Isometric_MapManager>();

        v2_PosMoveTo = new Vector2Int((int)cl_Single.Get_Pos().x, (int)cl_Single.Get_Pos().y);
        v2_PosStandOn = new Vector2Int((int)cl_Single.Get_Pos().x, (int)cl_Single.Get_Pos().y);
        cl_Single.Set_Pos(v2_PosStandOn);
    }

    private void Update()
    {
        if (b_UseScriptControl)
        {
            Set_ControlKeyboard();
        }

        Set_MoveControl();
    }

    //Instance Control

    /// <summary>
    /// Set Pos to Pos Move To by Choice Pos
    /// </summary>
    /// <param name="v2_Pos"></param>
    public void Set_PosMoveTo_Pos(Vector2Int v2_Pos)
    {
        if (b_DelayControl)
        {
            if (Get_Moving())
                return;
        }

        if (!cl_MapManager_MapManager.Get_Check_InsideMap(v2_Pos, new Vector2Int()))
        {
            return;
        }

        if (cl_Ground_Check != null)
        {
            if (!cl_Ground_Check.Get_Check_Ground_Accept(v2_Pos, new Vector2Int()))
            {
                return;
            }
        }

        if (cl_Object_Check != null)
        {
            if (!cl_Object_Check.Get_Check_Object_Accept(v2_Pos, new Vector2Int()))
            {
                return;
            }
        }

        if (cl_Fence_Check != null)
        {
            if (cl_Fence_Check.Get_Check_Fence_Ahead(v2_Pos, new Vector2Int()))
            {
                return;
            }
        }

        this.v2_PosMoveTo = v2_Pos;
        f_MoveTime_Cur = f_TimeDelay;
    }

    #region Keyboard Control

    /// <summary>
    /// Keyboard Control
    /// </summary>
    public void Set_ControlKeyboard()
    {
        if (Input.GetKeyDown(k_Up))
        {
            Set_PosMoveTo_Up();
        }
        else
        if (Input.GetKeyDown(k_Down))
        {
            Set_PosMoveTo_Down();
        }
        else
        if (Input.GetKeyDown(k_Left))
        {
            Set_PosMoveTo_Left();
        }
        else
        if (Input.GetKeyDown(k_Right))
        {
            Set_PosMoveTo_Right();
        }
    }

    /// <summary>
    /// Set Pos to Pos Move To by Choice Dir
    /// </summary>
    /// <param name="v2_Dir"></param>
    private void Set_PosMoveTo_Dir(Vector2Int v2_Dir)
    {
        if (b_DelayControl)
        {
            if (Get_Moving())
                return;
        }

        if (!cl_MapManager_MapManager.Get_Check_InsideMap(v2_PosStandOn + v2_Dir))
        {
            return;
        }

        if (cl_Ground_Check != null)
        {
            if (!cl_Ground_Check.Get_Check_Ground_Accept(v2_PosStandOn, v2_Dir))
            {
                return;
            }
        }

        if(cl_Object_Check!= null)
        {
            if(!cl_Object_Check.Get_Check_Object_Accept(v2_PosStandOn, v2_Dir))
            {
                return;
            }
        }

        if(cl_Fence_Check != null)
        {
            if (cl_Fence_Check.Get_Check_Fence_Ahead(v2_PosStandOn, v2_Dir))
            {
                return;
            }
        }
        
        v2_PosMoveTo += v2_Dir;
        f_MoveTime_Cur = f_TimeDelay;
    }

    #endregion

    #region Fast Move Control

    /// <summary>
    /// Set Pos Move To at Up Dir
    /// </summary>
    public void Set_PosMoveTo_Up()
    {
        Set_PosMoveTo_Dir(cl_MapManager_MapManager.v2_DirUp);
        i_Face = -1;
    }

    /// <summary>
    /// Set Pos Move To at Down Dir
    /// </summary>
    public void Set_PosMoveTo_Down()
    {
        Set_PosMoveTo_Dir(cl_MapManager_MapManager.v2_DirDown);
        i_Face = 1;
    }

    /// <summary>
    /// Set Pos Move To at Left Dir
    /// </summary>
    public void Set_PosMoveTo_Left()
    {
        Set_PosMoveTo_Dir(cl_MapManager_MapManager.v2_DirLeft);
        i_Face = -1;
    }

    /// <summary>
    /// Set Pos Move To at Right Dir
    /// </summary>
    public void Set_PosMoveTo_Right()
    {
        Set_PosMoveTo_Dir(cl_MapManager_MapManager.v2_DirRight);
        i_Face = 1;
    }

    #endregion

    #region Auto Control Methode

    /// <summary>
    /// Control Move by Command
    /// </summary>
    private void Set_MoveControl()
    {
        if (Get_Moving())
        //If In time Move between Square Ground
        {
            v2_PosStandOn = v2_PosMoveTo;

            f_MoveTime_Cur -= Time.deltaTime;
        }

        Set_Move_x(v2_PosMoveTo.x);
        Set_Move_y(v2_PosMoveTo.y);
    }

    /// <summary>
    /// Move x
    /// </summary>
    /// <param name="f_x_MoveTo"></param>
    private void Set_Move_x(float f_x_MoveTo)
    {
        float f_x_MoveNew = Mathf.SmoothDamp(
            cl_Single.Get_Pos().x, 
            f_x_MoveTo, 
            ref f_Velocity_x, 
            f_TimeDelay * f_MovePercent);
        cl_Single.Set_Pos(f_x_MoveNew, cl_Single.Get_Pos().y);
    }

    /// <summary>
    /// Move y
    /// </summary>
    /// <param name="f_y_MoveTo"></param>
    private void Set_Move_y(float f_y_MoveTo)
    {
        float f_y_MoveNew = Mathf.SmoothDamp(
            cl_Single.Get_Pos().y, 
            f_y_MoveTo, 
            ref f_Velocity_y, 
            f_TimeDelay * f_MovePercent);
        cl_Single.Set_Pos(cl_Single.Get_Pos().x, f_y_MoveNew);
    }

    #endregion

    /// <summary>
    /// In Current Moving?
    /// </summary>
    /// <returns></returns>
    public bool Get_Moving()
    {
        return (f_MoveTime_Cur > 0);
    }

    /// <summary>
    /// Control Face to Right?
    /// </summary>
    /// <returns></returns>
    public bool Get_Face_Right()
    {
        return i_Face == 1;
    }

    /// <summary>
    /// Get Pos Move To
    /// </summary>
    /// <returns></returns>
    public Vector2Int Get_PosMoveTo()
    {
        return v2_PosMoveTo;
    }
}
