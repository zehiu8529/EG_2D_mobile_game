using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
[RequireComponent(typeof(Isometric_MoveControl))]
public class Isometric_MoveAnimation : MonoBehaviour
{
    #region Public Varible

    /// <summary>
    /// Name of Boolean Varible of Animator "Move"
    /// </summary>
    [Header("Animator")]
    public string s_Boolean_Move = "Move";

    /// <summary>
    /// At Start, 
    /// </summary>
    public bool b_FaceRight = true;

    #endregion

    #region Private Varible

    /// <summary>
    /// Control Object Move on Map Ground
    /// </summary>
    private Isometric_MoveControl cl_Move;

    /// <summary>
    /// Animator
    /// </summary>
    private Animator a_Animator;

    /// <summary>
    /// Scale Begin of Sprite on this GameObject
    /// </summary>
    private float f_x_Scale;

    #endregion

    private void Start()
    {
        cl_Move = GetComponent<Isometric_MoveControl>();

        a_Animator = GetComponent<Animator>();

        f_x_Scale = transform.localScale.x;
    }

    private void Update()
    {
        if (a_Animator != null)
        {
            if (cl_Move.Get_Moving())
            {
                a_Animator.SetBool(s_Boolean_Move, true);
            }
            else
            {
                a_Animator.SetBool(s_Boolean_Move, false);
            }
        }

        if (cl_Move.Get_Face_Right())
        {
            Set_Scale_Right();
        }
        else
        {
            Set_Scale_Left();
        }
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
}
