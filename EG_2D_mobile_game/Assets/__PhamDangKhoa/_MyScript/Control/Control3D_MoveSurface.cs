using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Rigid3D_Component))]

public class Control3D_MoveSurface : MonoBehaviour
//Move Control Surface (X & Z)
{
    #region Public Varible (Keyboard)

    /// <summary>
    /// Is Keyboard Control Allow on this Game Object?
    /// </summary>
    [Header("Keyboard")]
    public bool b_KeyboardControl = false;

    /// <summary>
    /// Control Move Up (Forward)
    /// </summary>
    public KeyCode k_MoveUp = KeyCode.UpArrow;

    /// <summary>
    /// Control Move Down (Backward)
    /// </summary>
    public KeyCode k_MoveDown = KeyCode.DownArrow;

    /// <summary>
    /// Control Move Left
    /// </summary>
    public KeyCode k_MoveLeft = KeyCode.LeftArrow;

    /// <summary>
    /// Control Move Right
    /// </summary>
    public KeyCode k_MoveRight = KeyCode.RightArrow;

    /// <summary>
    /// Control Muti X & Z
    /// </summary>
    public bool b_MutiButton = true;

    /// <summary>
    /// Control Speed Chance
    /// </summary>
    public KeyCode k_SpeedChance = KeyCode.LeftShift;

    #endregion

    #region Public Varible

    /// <summary>
    /// Normal Speed Move
    /// </summary>
    [Header("Move")]
    public float f_SpeedNormal = 2f;

    /// <summary>
    /// Chance Speed Move
    /// </summary>
    public float f_SpeedChance = 5f;

    /// <summary>
    /// Current Speed Move
    /// </summary>
    private float f_SpeedCur;

    /// <summary>
    /// Control Stop without Speed Stop Velocity
    /// </summary>
    public bool b_StopRightAway = false;

    /// <summary>
    /// Speed Stop
    /// </summary>
    public float f_SpeedStop = 3f;

    #endregion

    #region Private Varible

    /// <summary>
    /// Use "Move" of this Script
    /// </summary>
    private Rigid3D_Component cs_Rigid;

    /// <summary>
    /// Get Button Control X
    /// </summary>
    private int i_ButtonMove_X_Right = 0;

    /// <summary>
    /// Get Button Control Z
    /// </summary>
    private int i_ButtonMove_Z_Forward = 0;

    /// <summary>
    /// Get Speed Chance
    /// </summary>
    private bool b_SpeedChance = false;

    //Keyboard Input Chance?

    /// <summary>
    /// Is Keyboard Control this?
    /// </summary>
    //private bool b_InputChanged = false;

    //private int i_x_Pressed = 0;
    //private int i_z_Pressed = 0;
    //private bool b_SpeedChance_Pressed = false;

    #endregion

    private void Awake()
    {
        cs_Rigid = GetComponent<Rigid3D_Component>();
    }

    private void Update()
    {
        if (b_KeyboardControl)
        {
            Set_KeyboadControl();
        }
    }

    private void FixedUpdate()
    {
        Set_MoveControl();
    }

    #region Keyboard Control Methode

    /// <summary>
    /// Control Move by Keyboard
    /// </summary>
    public void Set_KeyboadControl()
    {
        //var i_X_Pressed = 0;
        //var i_Z_Pressed = 0;
        //var b_SpeedChance_Pressed = false;

        Set_SpeedChance(Input.GetKey(k_SpeedChance) ? true : false);

        if (!b_MutiButton)
        //If NOT Allow Muti Control X & Z
        {
            if ((Input.GetKey(k_MoveLeft) || Input.GetKey(k_MoveRight)) &&
                (Input.GetKey(k_MoveUp) || Input.GetKey(k_MoveDown)))
            //Control X & Z >> Not Move
            {
                Set_Move_X_Right(0);
                Set_Move_Z_Forward(0);

                //i_X_Pressed = 0;
                //i_Z_Pressed = 0;
            }
        }
        else
        //If Allow Muti Control X & Z
        {
            if (Input.GetKey(k_MoveLeft) && Input.GetKey(k_MoveRight))
            //Press "Left" & "Right" >> Not Move X
            {
                Set_Move_X_Right(0);

                //i_X_Pressed = 0;
            }
            else
            {
                if (Input.GetKey(k_MoveLeft))
                //Control Left
                {
                    Set_Move_X_Right(-1);

                    //i_X_Pressed = -1;
                }
                else
                if (Input.GetKey(k_MoveRight))
                //Control Right
                {
                    Set_Move_X_Right(1);

                    //i_X_Pressed = 1;
                }
                else
                //Not Control X
                {
                    Set_Move_X_Right(0);

                    //i_X_Pressed = 0;
                }
            }

            if (Input.GetKey(k_MoveDown) && Input.GetKey(k_MoveUp))
            //Press "Up" & "Down" >> Not Move Z
            {
                Set_Move_Z_Forward(0);

                //i_Z_Pressed = 0;
            }
            else
            {
                if (Input.GetKey(k_MoveDown))
                //Control Down
                {
                    Set_Move_Z_Forward(-1);

                    //i_Z_Pressed = 0;
                }
                else
                if (Input.GetKey(k_MoveUp))
                //Control Up
                {
                    Set_Move_Z_Forward(1);

                    //i_Z_Pressed = 0;
                }
                else
                //Not Control Z
                {
                    Set_Move_Z_Forward(0);

                    //i_Z_Pressed = 0;
                }
            }
        }

        //b_InputChanged =
        //    this.i_x_Pressed != i_X_Pressed ||
        //    this.i_z_Pressed != i_Z_Pressed ||
        //    this.b_SpeedChance_Pressed != b_SpeedChance_Pressed;

        //this.i_x_Pressed = i_X_Pressed;
        //this.i_z_Pressed = i_Z_Pressed;
        //this.b_SpeedChance_Pressed = b_SpeedChance_Pressed;
    }

    /// <summary>
    /// Control Move X
    /// </summary>
    /// <param name="i_ButtonMove_X_Right">If "+1" is Move Right or "-1" is Move Left</param>
    public void Set_Move_X_Right(int i_ButtonMove_X_Right)
    {
        this.i_ButtonMove_X_Right = i_ButtonMove_X_Right;
    }

    /// <summary>
    /// Get Move X
    /// </summary>
    /// <returns>Get "+1" is Move Right, else "-1" is Move Left, else "0" is Stop Move</returns>
    public int Get_Move_X_Right()
    {
        return i_ButtonMove_X_Right;
    }

    /// <summary>
    /// Control Move Z
    /// </summary>
    /// <param name="i_ButtonMove_Z_Forward">If "+1" is Move Forward or "-1" is Move Backward</param>
    public void Set_Move_Z_Forward(int i_ButtonMove_Z_Forward)
    {
        this.i_ButtonMove_Z_Forward = i_ButtonMove_Z_Forward;
    }

    /// <summary>
    /// Get Move Z
    /// </summary>
    /// <returns>Get "+1" is Move Forward, else "-1" is Move Backward, else "0" is Stop Move</returns>
    public int Get_Move_Z_Forward()
    {
        return i_ButtonMove_Z_Forward;
    }

    /// <summary>
    /// Control Move Forward
    /// </summary>
    public void Set_Move_Forward()
    {
        Set_Move_Z_Forward(1);
    }

    /// <summary>
    /// Control Move Backward
    /// </summary>
    public void Set_Move_Backward()
    {
        Set_Move_Z_Forward(-1);
    }

    /// <summary>
    /// Control Move Left
    /// </summary>
    public void Set_Move_Left()
    {
        Set_Move_X_Right(-1);
    }

    /// <summary>
    /// Control Move Right
    /// </summary>
    public void Set_Move_Right()
    {
        Set_Move_X_Right(1);
    }

    /// <summary>
    /// Control Stop X
    /// </summary>
    public void Set_Stop_X()
    {
        i_ButtonMove_X_Right = 0;
    }

    /// <summary>
    /// Control Stop Z
    /// </summary>
    public void Set_Stop_Z()
    {
        i_ButtonMove_Z_Forward = 0;
    }

    /// <summary>
    /// Control Speed Chance
    /// </summary>
    public void Set_SpeedChance(bool b_SpeedChance)
    {
        this.b_SpeedChance = b_SpeedChance;
    }

    #endregion

    #region Auto Control Methode

    /// <summary>
    /// Control Move by Command
    /// </summary>
    private void Set_MoveControl()
    {
        f_SpeedCur = (b_SpeedChance) ? f_SpeedChance : f_SpeedNormal;

        if (i_ButtonMove_X_Right != 0)
        {
            cs_Rigid.Set_MoveX_Velocity(i_ButtonMove_X_Right, f_SpeedCur, f_SpeedCur);
        }
        else
        {
            if (b_StopRightAway)
                cs_Rigid.Set_StopX_Velocity();
            else
                cs_Rigid.Set_StopX_Velocity(f_SpeedStop);
        }

        if (i_ButtonMove_Z_Forward != 0)
        {
            cs_Rigid.Set_MoveZ_Velocity(i_ButtonMove_Z_Forward, f_SpeedCur, f_SpeedCur);
        }
        else
        {
            if (b_StopRightAway)
                cs_Rigid.Set_StopZ_Velocity();
            else
                cs_Rigid.Set_StopZ_Velocity(f_SpeedStop);
        }
    }

    #endregion

    #region Last Frame Control

    /// <summary>
    /// Get Last Frame Input Changed?
    /// </summary>
    /// <returns></returns>
    //public bool Get_InputChanged()
    //{
    //    return b_InputChanged;
    //}

    /// <summary>
    /// Get Last Frame Input X Changed
    /// </summary>
    /// <returns>Get "+1" is Move Right, else "-1" is Move Left, else "0" is Stop Move</returns>
    //public int Get_X_Pressed()
    //{
    //    return i_x_Pressed;
    //}

    /// <summary>
    /// Get Last Frame Input Z Changed
    /// </summary>
    /// <returns>Get "+1" is Move Forward, else "-1" is Move Backward, else "0" is Stop Move</returns>
    //public int Get_Z_Pressed()
    //{
    //    return i_z_Pressed;
    //}

    /// <summary>
    /// Get Last Frame Input Speed Chance Changed
    /// </summary>
    /// <returns></returns>
    //public bool Get_SpeedChance_Pressed()
    //{
    //    return b_SpeedChance_Pressed;
    //}

    #endregion
}
