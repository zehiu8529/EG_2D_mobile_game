using UnityEngine;

/// <summary>
/// Jump Single Time at Once
/// </summary>
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Control3D_Rigidbody))]
public class Control3D_JumpSingle : MonoBehaviour
//Control Player Jump
{
    #region Public Varible

    /// <summary>
    /// Keyboard Control Allow?
    /// </summary>
    [Header("Keyboard")]
    [SerializeField]
    private bool b_UseScriptControl = true;

    /// <summary>
    /// Jump
    /// </summary>
    [SerializeField]
    private KeyCode k_Jump = KeyCode.Space;

    /// <summary>
    /// Hold Jump to Jump Higher?
    /// </summary>
    [SerializeField]
    private bool b_HoldJump = false;

    /// <summary>
    /// Jump Power
    /// </summary>
    [Header("Jump")]
    private float f_JumpVelocity = 5f;

    #endregion

    #region Private Varible

    /// <summary>
    /// Control Velocity GameObject in 3D
    /// </summary>
    private Control3D_Rigidbody cs_Rigid;

    #endregion

    private void Start()
    {
        cs_Rigid = GetComponent<Control3D_Rigidbody>();
    }

    private void Update()
    {
        if (b_UseScriptControl)
        {
            Set_JumpButton();
        }
    }

    /// <summary>
    /// Set Jump by Keyboard
    /// </summary>
    public void Set_JumpButton()
    {
        if (!b_HoldJump && Input.GetKeyDown(k_Jump))
        {
            Set_Jump();
        }
        else
        if (b_HoldJump && Input.GetKey(k_Jump))
        {
            Set_Jump();
        }
    }

    /// <summary>
    /// Set Jump Auto
    /// </summary>
    /// <param name="b_JumpHold"></param>
    public void Set_JumpAuto(bool b_JumpHold)
    {
        if (b_JumpHold)
        {
            Set_Jump();
        }
    }

    /// <summary>
    /// Set Jump
    /// </summary>
    public void Set_Jump()
    {
        if (cs_Rigid.Get_CheckFoot())
            cs_Rigid.Set_MoveY_Jump(f_JumpVelocity);
    }
}