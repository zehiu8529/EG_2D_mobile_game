using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Rigid3D_Component))]

public class Control3D_JumpSingle : MonoBehaviour
//Control Player Jump
{
    private Rigid3D_Component cs_Rigid;
    //Use "Head Check" & "Foot Check"

    [Header("Keyboard")]
    public KeyCode k_Jump = KeyCode.Space;
    //Button Jump
    public bool b_HoldJump = false;
    //Hold to continue Jump

    [Header("Jump")]
    public float f_JumpPower = 5f;
    //Jump Velocity

    private void Awake()
    {
        cs_Rigid = GetComponent<Rigid3D_Component>();
    }

    private void Update()
    {
        Set_JumpButton();
    }

    public void Set_JumpButton()
    //Set Jump by Keyboard
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

    public void Set_JumpAuto(bool b_JumpHold)
    //Set Jump Auto
    {
        if (b_JumpHold)
        {
            Set_Jump();
        }
    }

    public void Set_Jump()
    //Set Jump
    {
        if (cs_Rigid.Get_CheckFoot())
            cs_Rigid.Set_MoveY_Jump(f_JumpPower);
    }

    public void Set_ScriptEnable(bool b_ScriptEnable)
    {
        this.enabled = b_ScriptEnable;
    }
}