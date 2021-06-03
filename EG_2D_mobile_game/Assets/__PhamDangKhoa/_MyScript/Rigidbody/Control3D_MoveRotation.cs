using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Control3D_Rigidbody))]

public class Control3D_MoveRotation : MonoBehaviour
//Move Control Surface (X & Z & Rotation)
{
    private Control3D_Rigidbody cs_Rigid;

    [Header("Keyboard")]
    public KeyCode k_MoveUp = KeyCode.UpArrow;
    //Control Move Up (Forward)

    public KeyCode k_MoveDown = KeyCode.DownArrow;
    //Control Move Down (Backward)

    public KeyCode k_TurnLeft = KeyCode.LeftArrow;
    //Control Turn Left

    public KeyCode k_TurnRight = KeyCode.RightArrow;
    //Control Turn Right

    public bool b_MutiButton = true;
    //Muti Button Dir

    public KeyCode k_SpeedChance = KeyCode.LeftShift;
    //Control Speed Chance

    [Header("Move")]
    public float f_SpeedNormal = 7f;
    //Normal Speed Move (Need More Power, because Script always Slow Move)

    public float f_SpeedChance = 10f;
    //Chance Speed Move
    private float f_SpeedCur;
    //Current Speed Move

    [Range(0.1f,5f)]
    public float f_SpeedRotate = 1f;
    //Speed Rotation Chance

    public bool b_StopRightAway = false;
    //Control Stop without Speed Stop Velocity

    public float f_SpeedStop = 3f;
    //Speed Stop (Chance Dir Move when Turn)

    public bool b_SlowWhenTurn = true;
    //Slow Speed When Turn

    public float f_SpeedSlow = 5f;
    //Slow Speed

    private Animator a_Animator;

    void Awake()
    {
        cs_Rigid = GetComponent<Control3D_Rigidbody>();
        a_Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Set_SpeedChance();
        Set_MoveButton();
    }

    public void Set_MoveButton()
    //Control Move by Button
    {
        if (Input.GetKey(k_TurnLeft) && Input.GetKey(k_TurnRight) || 
            Input.GetKey(k_MoveUp) && Input.GetKey(k_MoveDown))
            //
            return;

        if (Input.GetKey(k_TurnLeft))
            Set_RotationChance(-1);
        else
        if (Input.GetKey(k_TurnRight))
            Set_RotationChance(1);

        if (Input.GetKey(k_MoveUp))
            Set_Move(1);
        else
        if (Input.GetKey(k_MoveDown))
            Set_Move(-1);
        else
            Set_Stop();

        Set_SlowStop();
    }

    public void Set_SpeedChance()
    //Control Speed Chance
    {
        f_SpeedCur = (Input.GetKey(k_SpeedChance)) ? f_SpeedChance : f_SpeedNormal;
    }

    public void Set_RotationChance(int i_RotationDir)
    {
        if (b_SlowWhenTurn)
            Set_Slow();

        cs_Rigid.Set_RotationChance_XZ(f_SpeedRotate * i_RotationDir);
    }

    public void Set_Move(int i_MoveDir)
    {
        cs_Rigid.Set_MoveRotation_XZ(cs_Rigid.Get_Rotation_XZ(), f_SpeedCur * i_MoveDir);
        a_Animator.SetBool("isWalking", true);
    }

    public void Set_Stop()
    {
        if (b_StopRightAway)
        {
            cs_Rigid.Set_StopX_Velocity();
            cs_Rigid.Set_StopZ_Velocity();
        }
        a_Animator.SetBool("isWalking", false);
    }

    public void Set_Slow()
    {
        cs_Rigid.Set_StopX_Velocity(f_SpeedSlow);
        cs_Rigid.Set_StopZ_Velocity(f_SpeedSlow);
    }

    public void Set_SlowStop()
    {
        cs_Rigid.Set_StopX_Velocity(f_SpeedStop);
        cs_Rigid.Set_StopZ_Velocity(f_SpeedStop);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;

        Class_Vector cl_Vector = new Class_Vector();
        Control3D_Rigidbody cs_Rigid = GetComponent<Control3D_Rigidbody>();

        Gizmos.DrawLine(
            transform.position,
            transform.position + cl_Vector.Get_DegToVector_XZ(-cs_Rigid.Get_Rotation_XZ(), 1f));
        Gizmos.DrawWireSphere(
            transform.position + cl_Vector.Get_DegToVector_XZ(-cs_Rigid.Get_Rotation_XZ(), 1f),
            0.1f);
    }
}
