using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Add this Script on "2.5D Sprite Moving" with "Object" like "Character", etc...
/// </summary>
/// <remarks>
/// Use with Camera set 'Orthographic'
/// </remarks>
[RequireComponent(typeof(Isometric_Single))]
public class Isometric_Move : MonoBehaviour
{
    //Notice: Need Move in Square to continue Control Move

    #region Public Varible

    [Header("Isometric Map Tag")]
    [SerializeField]
    private string s_Tag = "IsometricMap";

    /// <summary>
    /// Map
    /// </summary>
    [SerializeField]
    private Isometric_Map cl_Map;

    /// <summary>
    /// Is Keyboard Control Allow on this Game Object?
    /// </summary>
    [Header("Control")]
    public bool b_KeyboardControl = false;

    /// <summary>
    /// Control Pos
    /// </summary>
    [SerializeField]
    private KeyCode k_Up = KeyCode.UpArrow;

    /// <summary>
    /// Control Pos
    /// </summary>
    [SerializeField]
    private KeyCode k_Down = KeyCode.DownArrow;

    /// <summary>
    /// Control Pos
    /// </summary>
    [SerializeField]
    private KeyCode k_Left = KeyCode.LeftArrow;

    /// <summary>
    /// Control Pos
    /// </summary>
    [SerializeField]
    private KeyCode k_Right = KeyCode.RightArrow;

    /// <summary>
    /// Time Move between Square
    /// </summary>
    [Header("Move")]
    [SerializeField]
    private float f_TimeMove = 0.2f;

    /// <summary>
    /// Time Move Delay between Square
    /// </summary>
    private float f_TimeMove_Cur = 0f;

    /// <summary>
    /// Code check not Move into on Map (In Unity)
    /// </summary>
    [Header("Avoid")]
    [SerializeField]
    private List<char> l_GroundCodeAvoid;

    /// <summary>
    /// Code check not Move into on Map (In Unity)
    /// </summary>
    [SerializeField]
    private List<char> l_ObjectCodeAvoid;

    /// <summary>
    /// Pos Move to
    /// </summary>
    //[SerializeField]
    private Vector2Int v2_PosSquareMoveTo;

    /// <summary>
    /// Name of Boolean Varible of Animator "Move"
    /// </summary>
    [Header("Animator")]
    public string s_Boolean_Move = "Move";

    public bool b_FaceRight = true;

    #endregion

    #region Private Varible

    /// <summary>
    /// Add this Script on "2.5D Sprite" to show on Environment with Camera set 'Orthographic'
    /// </summary>
    private Isometric_Single cl_Single;

    /// <summary>
    /// Working on Vector
    /// </summary>
    private Class_Vector cl_Vector;

    /// <summary>
    /// Smooth Damp Velocity x
    /// </summary>
    private float f_Velocity_x = 0.0f;
    /// <summary>
    /// Smooth Damp Velocity y
    /// </summary>
    private float f_Velocity_y = 0.0f;

    /// <summary>
    /// Pos on Map by Square exacly
    /// </summary>
    private Vector2Int v2_PosSquareCurrent;

    /// <summary>
    /// Animator
    /// </summary>
    private Animator a_Animator;

    /// <summary>
    /// Scale Begin of Sprite on this GameObject
    /// </summary>
    private float f_x_Scale;

    /// <summary>
    /// Is Keyboard Control this?
    /// </summary>
    private bool b_InputChanged = false;

    private int i_x_Pressed = 0;
    private int i_y_Pressed = 0;

    #endregion

    private void Start()
    {
        cl_Single = GetComponent<Isometric_Single>();

        if (cl_Map == null)
        {
            if (s_Tag != "")
            {
                cl_Map = GameObject.FindGameObjectWithTag(s_Tag).GetComponent<Isometric_Map>();

                if (cl_Map == null)
                {
                    Debug.LogError("Isometric_Move.cs: 'Isometric_Map' not found!");
                }
            }
        }

        cl_Vector = new Class_Vector();

        v2_PosSquareMoveTo = cl_Vector.Get_VectorInt(cl_Single.Get_Pos());

        //Pos of Square on Map
        v2_PosSquareCurrent = cl_Vector.Get_VectorInt(cl_Vector.Get_VectorInt(cl_Single.Get_Pos()));
        if(cl_Single.Get_Pos() != v2_PosSquareCurrent)
        //If this Object not Set in Square Ground >> Set Pos to near Square
        {
            cl_Single.Set_Pos(v2_PosSquareCurrent);
        }

        if (GetComponent<Animator>() != null)
        {
            a_Animator = GetComponent<Animator>();
        }

        f_x_Scale = Mathf.Abs(this.transform.localScale.x);
    }

    private void Update()
    {
        if (b_KeyboardControl)
        {
            Set_KeyboardControl();
        }
        
        Set_MoveControl();
    }

    /// <summary>
    /// Control by Keyboard
    /// </summary>
    public void Set_KeyboardControl()
    {
        var i_x_Pressed = 0;
        var i_y_Pressed = 0;

        if (Input.GetKey(k_Up))
        {
            Set_Move_Up();

            //i_x_Pressed = Set_Move_Up();
            //i_y_Pressed = 0;
        }
        else
        if (Input.GetKey(k_Down))
        {
            Set_Move_Down();

            //i_x_Pressed = Set_Move_Down();
            //i_y_Pressed = 0;
        }
        else
        if (Input.GetKey(k_Left))
        {
            Set_Move_Left();

            //i_x_Pressed = 0;
            //i_y_Pressed = Set_Move_Left();
        }
        else
        if (Input.GetKey(k_Right))
        {
            Set_Move_Right();

            //i_x_Pressed = 0;
            //i_y_Pressed = Set_Move_Right();
        }
        else
        {
            i_x_Pressed = 0;
            i_y_Pressed = 0;
        }

        b_InputChanged =
            this.i_x_Pressed != i_x_Pressed ||
            this.i_y_Pressed != i_y_Pressed;

        this.i_x_Pressed = i_x_Pressed;
        this.i_y_Pressed = i_y_Pressed;
    }

    /// <summary>
    /// Get Check Allow Control yet?
    /// </summary>
    /// <returns>If False, can not Control to Move</returns>
    public bool Get_AllowControl()
    {
        return f_TimeMove_Cur <= 0;
    }

    /// <summary>
    /// Get Check Moving?
    /// </summary>
    /// <returns></returns>
    public bool Get_Moving()
    {
        return !Get_AllowControl();
    }

    public void Set_TimeMove(float f_TimeMove)
    {
        this.f_TimeMove = f_TimeMove;
    }

    public float Get_TimeMove()
    {
        return f_TimeMove;
    }

    #region Check Avoid

    /// <summary>
    /// Check Ground Square if it Avoid
    /// </summary>
    /// <param name="v2_Dir"></param>
    /// <returns>If True, Avoid Move to that Ground Square</returns>
    public bool Get_CheckAvoid_Ground(Vector2Int v2_Dir)
    {
        if (cl_Map.Get_Map_GroundCode(v2_PosSquareCurrent, v2_Dir) == ' ')
        {
            return true;
        }
        for (int i = 0; i < l_GroundCodeAvoid.Count; i++)
        {
            if(cl_Map.Get_Map_GroundCode(v2_PosSquareCurrent, v2_Dir) == l_GroundCodeAvoid[i])
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Check Object on Ground Square if it Avoid
    /// </summary>
    /// <param name="v2_Dir"></param>
    /// <returns>If True, Avoid Move to that Ground Square</returns>
    public bool Get_CheckAvoid_Object(Vector2Int v2_Dir)
    {
        if(cl_Map.Get_Map_ObjectCode(v2_PosSquareCurrent, v2_Dir) == ' ')
        {
            return true;
        }
        for (int i = 0; i < l_ObjectCodeAvoid.Count; i++)
        {
            if (cl_Map.Get_Map_ObjectCode(v2_PosSquareCurrent, v2_Dir) == l_ObjectCodeAvoid[i])
            {
                return true;
            }
        }
        return false;
    }

    public bool Get_CheckAllow_Move(Vector2Int v2_Dir)
    {
        return
            Get_CheckAvoid_Ground(v2_Dir) ||
            Get_CheckAvoid_Object(v2_Dir) ||
            cl_Map.Get_Check_Fence(v2_PosSquareCurrent, v2_Dir);
    }

    #endregion

    #region Control Move

    /// <summary>
    /// Move Up (+1,0)
    /// </summary>
    public void Set_Move_Up()
    {
        Set_Scale_Left();

        if (Get_CheckAllow_Move(cl_Map.v2_DirUp))
        //Check if on the Square of Map is Avoid or have Fence at Up Dir
        {
            return;
            //return 0;
        }
        if (Get_AllowControl())
        {
            v2_PosSquareMoveTo += cl_Map.v2_DirUp;
            f_TimeMove_Cur = f_TimeMove;

            //return 1;
        }
        //return 0;
    }

    /// <summary>
    /// Get Pos if Move Up
    /// </summary>
    /// <returns></returns>
    public Vector2Int Get_PosMoveTo_Up()
    {
        return v2_PosSquareCurrent + cl_Map.v2_DirUp;
    }

    /// <summary>
    /// Move Down (-1,0)
    /// </summary>
    public void Set_Move_Down()
    {
        Set_Scale_Right();

        if (Get_CheckAllow_Move(cl_Map.v2_DirDown))
        //Check if on the Square of Map is Avoid or have Fence at Up Dir
        {
            return;
            //return 0;
        }
        if (Get_AllowControl())
        {
            v2_PosSquareMoveTo += cl_Map.v2_DirDown;
            f_TimeMove_Cur = f_TimeMove;

            //return -1;
        }
        //return 0;
    }

    /// <summary>
    /// Get Pos if Move Down
    /// </summary>
    /// <returns></returns>
    public Vector2Int Get_PosMoveTo_Down()
    {
        return v2_PosSquareCurrent + cl_Map.v2_DirDown;
    }

    /// <summary>
    /// Move Left (0,-1)
    /// </summary>
    public void Set_Move_Left()
    {
        Set_Scale_Left();

        if (Get_CheckAllow_Move(cl_Map.v2_DirLeft))
        //Check if on the Square of Map is Avoid
        {
            return;
            //return 0;
        }
        if (Get_AllowControl())
        {
            v2_PosSquareMoveTo += cl_Map.v2_DirLeft;
            f_TimeMove_Cur = f_TimeMove;

            //return -1;
        }
        //return 0;
    }

    /// <summary>
    /// Get Pos if Move Left
    /// </summary>
    /// <returns></returns>
    public Vector2Int Get_PosMoveTo_Left()
    {
        return v2_PosSquareCurrent + cl_Map.v2_DirLeft;
    }

    /// <summary>
    /// Move Right (0,+1)
    /// </summary>
    public void Set_Move_Right()
    {
        Set_Scale_Right();

        if (Get_CheckAllow_Move(cl_Map.v2_DirRight))
        //Check if on the Square of Map is Avoid
        {
            return;
            //return 0;
        }
        if (Get_AllowControl())
        {
            v2_PosSquareMoveTo += cl_Map.v2_DirRight;
            f_TimeMove_Cur = f_TimeMove;

            //return 1;
        }
        //return 0;
    }

    /// <summary>
    /// Get Pos if Move Right
    /// </summary>
    /// <returns></returns>
    public Vector2Int Get_PosMoveTo_Right()
    {
        return v2_PosSquareCurrent + cl_Map.v2_DirRight;
    }

    /// <summary>
    /// Move with Dir (x,y)
    /// </summary>
    /// <param name="i_x">Dir for Up (Forward) (+1) and Down (Backward) (-1)</param>
    /// <param name="i_y">Dir for Left (-1) and Right (+1)</param>
    public void Set_Move(int i_x, int i_y)
    {
        if (i_x != 0 && i_y != 0)
            return;

        if(i_x == 1)
        {
            Set_Move_Up();
        }
        else
        if(i_x == -1)
        {
            Set_Move_Down();
        }
        else
        if(i_y == 1)
        {
            Set_Move_Right();
        }
        else
        if(i_y == -1)
        {
            Set_Move_Left();
        }
    }

    /// <summary>
    /// Set Pos Square Move To for Auto Move
    /// </summary>
    /// <param name="v2_PosSquareMoveTo"></param>
    public void Set_Move_PosMoveTo(Vector2Int v2_PosSquareMoveTo)
    {
        this.v2_PosSquareMoveTo = v2_PosSquareMoveTo;

        f_TimeMove_Cur = f_TimeMove;

        if (this.v2_PosSquareMoveTo.x > v2_PosSquareMoveTo.x)
        {
            if(this.v2_PosSquareMoveTo.y > v2_PosSquareMoveTo.y)
            {
                Set_Scale_Left();
            }
            else
            if (this.v2_PosSquareMoveTo.y < v2_PosSquareMoveTo.y)
            {
                Set_Scale_Right();
            }
        }
        else
        if(this.v2_PosSquareMoveTo.x < v2_PosSquareMoveTo.x)
        {
            if (this.v2_PosSquareMoveTo.y < v2_PosSquareMoveTo.y)
            {
                Set_Scale_Left();
            }
            else
            if (this.v2_PosSquareMoveTo.y > v2_PosSquareMoveTo.y)
            {
                Set_Scale_Right();
            }
        }
    }

    /// <summary>
    /// Get Pos Square Move To for Auto Move
    /// </summary>
    /// <returns></returns>
    public Vector2Int Get_Pos_PosMoveTo()
    {
        return this.v2_PosSquareMoveTo;
    }

    /// <summary>
    /// Use for Teleport to a Square
    /// </summary>
    /// <param name="v2_Pos"></param>
    public void Set_Pos_Teleport(Vector2Int v2_Pos)
    {
        cl_Single.Set_Pos(v2_Pos);
        v2_PosSquareMoveTo = v2_Pos;
    }

    /// <summary>
    /// Get Pos of this Isometric Single
    /// </summary>
    /// <returns></returns>
    public Vector2 Get_Pos()
    {
        return cl_Single.Get_Pos();
    }

    #endregion

    #region Auto Control Methode

    /// <summary>
    /// Control Move by Command
    /// </summary>
    private void Set_MoveControl()
    {
        if (f_TimeMove_Cur > 0)
        //If In time Move between Square Ground
        {
            v2_PosSquareCurrent = v2_PosSquareMoveTo;

            f_TimeMove_Cur -= Time.deltaTime;

            if (a_Animator != null)
            {
                a_Animator.SetBool(s_Boolean_Move, true);
            }
        }
        else
        //If Not In time Move between Square Ground
        {
            if (a_Animator != null)
            {
                a_Animator.SetBool(s_Boolean_Move, false);
            }
        }

        Set_Move_x(v2_PosSquareMoveTo.x);
        Set_Move_y(v2_PosSquareMoveTo.y);
    }

    /// <summary>
    /// Move x
    /// </summary>
    /// <param name="f_x_MoveTo"></param>
    private void Set_Move_x(float f_x_MoveTo)
    {
        float f_x_MoveNew = Mathf.SmoothDamp(cl_Single.Get_Pos().x, f_x_MoveTo, ref f_Velocity_x, f_TimeMove);
        cl_Single.Set_Pos(f_x_MoveNew, cl_Single.Get_Pos().y);
    }

    /// <summary>
    /// Move y
    /// </summary>
    /// <param name="f_y_MoveTo"></param>
    private void Set_Move_y(float f_y_MoveTo)
    {
        float f_y_MoveNew = Mathf.SmoothDamp(cl_Single.Get_Pos().y, f_y_MoveTo, ref f_Velocity_y, f_TimeMove);
        cl_Single.Set_Pos(cl_Single.Get_Pos().x, f_y_MoveNew);
    }

    /// <summary>
    /// Scale Left when Control Move
    /// </summary>
    private void Set_Scale_Left()
    {
        if (b_FaceRight)
        {
            this.transform.localScale = new Vector3(
                -f_x_Scale,
                this.transform.localScale.y,
                this.transform.localScale.z);
        }
        else
        {
            this.transform.localScale = new Vector3(
                f_x_Scale,
                this.transform.localScale.y,
                this.transform.localScale.z);
        }
    }

    /// <summary>
    /// Scale Right wehn Control Move
    /// </summary>
    private void Set_Scale_Right()
    {
        if (b_FaceRight)
        {
            this.transform.localScale = new Vector3(
                f_x_Scale,
                this.transform.localScale.y,
                this.transform.localScale.z);
        }
        else
        {
            this.transform.localScale = new Vector3(
                -f_x_Scale,
                this.transform.localScale.y,
                this.transform.localScale.z);
        }
    }

    #endregion

    #region Last Frame Control

    /// <summary>
    /// Get Last Frame Input Changed?
    /// </summary>
    /// <returns></returns>
    public bool Get_InputChanged()
    {
        return b_InputChanged;
    }

    /// <summary>
    /// Get Last Frame Input X Changed
    /// </summary>
    /// <returns>Get "+1" is Move Right, else "-1" is Move Left, else "0" is Stop Move</returns>
    public int Get_X_Pressed()
    {
        return i_x_Pressed;
    }

    /// <summary>
    /// Get Last Frame Input Z Changed
    /// </summary>
    /// <returns>Get "+1" is Move Forward, else "-1" is Move Backward, else "0" is Stop Move</returns>
    public int Get_Y_Pressed()
    {
        return i_y_Pressed;
    }

    #endregion

    #region Spawm Set

    /// <summary>
    /// Add Map to this Script
    /// </summary>
    /// <param name="cl_Map"></param>
    public void Set_Map(Isometric_Map cl_Map)
    {
        this.cl_Map = cl_Map;
    }

    #endregion
}
