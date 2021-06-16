using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* - ISOMETRIC SQUARE:
 * 
 *                  ..
 *    UP(-1;+0)   ......   RIGHT(+0;+1)
 *              ..........
 *            .............. --> SQUARE(0;0)
 *              ..........
 *  LEFT(+0,-1)   ......   DOWN(+1;+0)
 *                  ..
 *  
 */

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

    /// <summary>
    /// Get MapManager GameObject
    /// </summary>
    [SerializeField]
    private GameObject g_MapManager;

    #region Control Move

    /// <summary>
    /// Allow Keyboard Control
    /// </summary>
    [Header("Move on Ground")]
    [SerializeField]
    private bool b_UseScriptControl = true;

    /// <summary>
    /// Key Move Up
    /// </summary>
    [SerializeField]
    private KeyCode k_Up = KeyCode.UpArrow;

    /// <summary>
    /// Key Move Down
    /// </summary>
    [SerializeField]
    private KeyCode k_Down = KeyCode.DownArrow;

    /// <summary>
    /// Key Move Left
    /// </summary>
    [SerializeField]
    private KeyCode k_Left = KeyCode.LeftArrow;

    /// <summary>
    /// Key Move Right
    /// </summary>
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
    private Isometric_CheckFence cl_Fence_Check;

    /// <summary>
    /// Object Check
    /// </summary>
    private Isometric_CheckObject cl_Object_Check;

    /// <summary>
    /// Pos Move To
    /// </summary>
    public Vector2Int v2_PosMoveTo; 

    /// <summary>
    /// Pos Stand On after Move
    /// </summary>
    public Vector2Int v2_PosStandOn;

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
    private int i_FaceRight = 1;

    #endregion

    private void Start()
    {
        if (g_MapManager == null)
        {
            if (s_Tag != "")
            {
                g_MapManager = GameObject.FindGameObjectWithTag(s_Tag);
            }
        }

        cl_Single = GetComponent<Isometric_Single>();

        cl_Ground_Check = GetComponent<Isometric_CheckGround>();
        cl_Fence_Check = GetComponent<Isometric_CheckFence>();
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


    #region Keyboard Control

    //Control

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
        //Not Delay Control
        {
            if (Get_Moving())
                return;
        }

        if (!Get_CheckMove_Dir(v2_Dir))
        //Check Move Accept
        {
            return;
        }
        
        //Move
        v2_PosMoveTo += v2_Dir;
        f_MoveTime_Cur = f_TimeDelay;
    }

    //Instance Control

    /// <summary>
    /// Set Pos to Pos Move To by Choice Pos
    /// </summary>
    /// <param name="v2_Pos"></param>
    public void Set_PosMoveTo_Pos(Vector2Int v2_Pos)
    {
        if (b_DelayControl)
        //Not Delay Control
        {
            if (Get_Moving())
                return;
        }

        //Face
        if(v2_PosStandOn.x < v2_Pos.x)
        {
            Set_Face_Right();
        }
        else
        if(v2_PosStandOn.y < v2_Pos.y)
        {
            Set_Face_Right();
        }
        else
        if (v2_PosStandOn.x > v2_Pos.x)
        {
            Set_Face_Left();
        }
        else
        if (v2_PosStandOn.y > v2_Pos.y)
        {
            Set_Face_Left();
        }

        if (!Get_CheckMove_Pos(v2_Pos))
        //Check Move Accept
        {
            return;
        }

        //Move
        this.v2_PosMoveTo = v2_Pos;
        f_MoveTime_Cur = f_TimeDelay;
    }

    //Check Dir

    /// <summary>
    /// Check Inside Map
    /// </summary>
    /// <param name="v2_Dir"></param>
    /// <returns></returns>
    public bool Get_CheckMove_Dir_InsideMap(Vector2Int v2_Dir)
    {
        return cl_MapManager_MapManager.Get_Check_InsideMap(v2_PosStandOn + v2_Dir);
    }

    /// <summary>
    /// Check Ground Accept
    /// </summary>
    /// <param name="v2_Dir"></param>
    /// <returns></returns>
    public bool Get_CheckMove_Dir_Ground(Vector2Int v2_Dir)
    {
        if (cl_Ground_Check != null)
        {
            if (!cl_Ground_Check.Get_Check_Ground_Accept(v2_PosStandOn, v2_Dir))
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Check Object Accept
    /// </summary>
    /// <param name="v2_Dir"></param>
    /// <returns></returns>
    public bool Get_CheckMove_Dir_Object(Vector2Int v2_Dir)
    {
        if (cl_Object_Check != null)
        {
            if (!cl_Object_Check.Get_Check_Object_Accept(v2_PosStandOn, v2_Dir))
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Check Fence Accept
    /// </summary>
    /// <param name="v2_Dir"></param>
    /// <returns></returns>
    public bool Get_CheckMove_Dir_Fence(Vector2Int v2_Dir)
    {
        if (cl_Fence_Check != null)
        {
            if (cl_Fence_Check.Get_Check_Fence_Ahead(v2_PosStandOn, v2_Dir))
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Check Move by Dir
    /// </summary>
    /// <param name="v2_Dir"></param>
    /// <returns></returns>
    public bool Get_CheckMove_Dir(Vector2Int v2_Dir)
    {
        if (!Get_CheckMove_Dir_InsideMap(v2_Dir))
        {
            return false;
        }

        if (!Get_CheckMove_Dir_Ground(v2_Dir))
        {
            return false;
        }

        if (!Get_CheckMove_Dir_Object(v2_Dir))
        {
            return false;
        }

        if (!Get_CheckMove_Dir_Fence(v2_Dir))
        {
            return false;
        }
        
        return true;
    }

    //Check Pos

    /// <summary>
    /// Check Inside Map
    /// </summary>
    /// <param name="v2_Pos"></param>
    /// <returns></returns>
    public bool Get_CheckMove_Pos_InsideMap(Vector2Int v2_Pos)
    {
        return cl_MapManager_MapManager.Get_Check_InsideMap(v2_Pos);
    }

    /// <summary>
    /// Check Ground Accept
    /// </summary>
    /// <param name="v2_Pos"></param>
    /// <returns></returns>
    public bool Get_CheckMove_Pos_Ground(Vector2Int v2_Pos)
    {
        if (cl_Ground_Check != null)
        {
            if (!cl_Ground_Check.Get_Check_Ground_Accept(v2_Pos))
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Check Object Accept
    /// </summary>
    /// <param name="v2_Pos"></param>
    /// <returns></returns>
    public bool Get_CheckMove_Pos_Object(Vector2Int v2_Pos)
    {
        if (cl_Object_Check != null)
        {
            if (!cl_Object_Check.Get_Check_Object_Accept(v2_Pos))
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Check Move by Pos
    /// </summary>
    /// <param name="v2_Pos"></param>
    /// <returns></returns>
    public bool Get_CheckMove_Pos(Vector2Int v2_Pos)
    {
        if (!Get_CheckMove_Pos_InsideMap(v2_Pos))
        {
            return false;
        }

        if (!Get_CheckMove_Pos_Ground(v2_Pos))
        {
            return false;
        }

        if (!Get_CheckMove_Pos_Object(v2_Pos))
        {
            return false;
        }
        
        return true;
    }

    #endregion

    #region Fast Move Control

    //Set Pos

    /// <summary>
    /// Set Pos Move To at Up Dir
    /// </summary>
    public void Set_PosMoveTo_Up()
    {
        Set_PosMoveTo_Dir(new Class_Isometric().v2_DirUp);
        i_FaceRight = -1;
    }

    /// <summary>
    /// Set Pos Move To at Down Dir
    /// </summary>
    public void Set_PosMoveTo_Down()
    {
        Set_PosMoveTo_Dir(new Class_Isometric().v2_DirDown);
        i_FaceRight = 1;
    }

    /// <summary>
    /// Set Pos Move To at Left Dir
    /// </summary>
    public void Set_PosMoveTo_Left()
    {
        Set_PosMoveTo_Dir(new Class_Isometric().v2_DirLeft);
        i_FaceRight = -1;
    }

    /// <summary>
    /// Set Pos Move To at Right Dir
    /// </summary>
    public void Set_PosMoveTo_Right()
    {
        Set_PosMoveTo_Dir(new Class_Isometric().v2_DirRight);
        i_FaceRight = 1;
    }

    //Get Pos

    /// <summary>
    /// Get Pos if Move Up
    /// </summary>
    /// <returns></returns>
    public Vector2Int Get_PosMoveTo_Up()
    {
        return v2_PosStandOn + new Class_Isometric().v2_DirUp;
    }

    /// <summary>
    /// Get Pos if Move Down
    /// </summary>
    /// <returns></returns>
    public Vector2Int Get_PosMoveTo_Down()
    {
        return v2_PosStandOn + new Class_Isometric().v2_DirDown;
    }

    /// <summary>
    /// Get Pos if Move Left
    /// </summary>
    /// <returns></returns>
    public Vector2Int Get_PosMoveTo_Left()
    {
        return v2_PosStandOn + new Class_Isometric().v2_DirLeft;
    }

    /// <summary>
    /// Get Pos if Move Right
    /// </summary>
    /// <returns></returns>
    public Vector2Int Get_PosMoveTo_Right()
    {
        return v2_PosStandOn + new Class_Isometric().v2_DirRight;
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

    #region Face

    /// <summary>
    /// Set Face
    /// </summary>
    /// <param name="i_FaceRight"></param>
    public void Set_FaceRight(int i_FaceRight)
    {
        this.i_FaceRight = i_FaceRight;
    }
    
    /// <summary>
    /// Set Face Left
    /// </summary>
    public void Set_Face_Left()
    {
        this.i_FaceRight = -1;
    }

    /// <summary>
    /// Set Face Right
    /// </summary>
    public void Set_Face_Right()
    {
        this.i_FaceRight = 1;
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
        return i_FaceRight == 1;
    }

    /// <summary>
    /// Get Pos Move To
    /// </summary>
    /// <returns></returns>
    public Vector2Int Get_PosMoveTo()
    {
        return v2_PosMoveTo;
    }

    /// <summary>
    /// Get Pos Stand On
    /// </summary>
    /// <returns></returns>
    public Vector2Int Get_PosStandOn()
    {
        return this.v2_PosStandOn;
    }
}
