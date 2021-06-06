using UnityEngine;

/// <summary>
/// Control Velocity GameObject in 3D
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class Control3D_Rigidbody : MonoBehaviour
{
    /// <summary>
    /// Use Script Auto Set at Start
    /// </summary>
    [Header("Rigid")]
    [SerializeField]
    private bool b_UseScriptStart = true;

    /// <summary>
    /// Set Kinematic (Static)
    /// </summary>
    [SerializeField]
    private bool b_Kinematic = false;

    /// <summary>
    /// Lock Rotation
    /// </summary>
    [SerializeField]
    private bool b_LockRot = true;

    /// <summary>
    /// Lock Pos
    /// </summary>
    [SerializeField]
    private bool b_LockPos = false;

    /// <summary>
    /// Layer Mask to Check Foot and Head
    /// </summary>
    [Header("Layer Check")]
    [SerializeField]
    private LayerMask l_GroundCheck;

    /// <summary>
    /// Debug Foot Check
    /// </summary>
    [Header("Foot Check")]
    [SerializeField]
    private bool b_FootDebug = true;

    /// <summary>
    /// Box Cast of Foot
    /// </summary>
    [SerializeField]
    private Vector3 v3_FootCast = new Vector3(1f, 0.1f, 1f);

    /// <summary>
    /// Box Cast Distance of Foot
    /// </summary>
    [SerializeField]
    private float f_FootCast = 1f;

    /// <summary>
    /// Use Script Auto Gravity
    /// </summary>
    [SerializeField]
    private bool b_UseScriptGravity = true;

    /// <summary>
    /// Auto Gravity Velocity
    /// </summary>
    [SerializeField]
    private float f_GravityFall = 20f;

    /// <summary>
    /// Debug Head Check
    /// </summary>
    [Header("Head Check")]
    [SerializeField]
    private bool b_HeadDebug = true;

    /// <summary>
    /// Box Cast of Head
    /// </summary>
    [SerializeField]
    private Vector3 v3_HeadCast = new Vector3(1f, 0.1f, 1f);

    /// <summary>
    /// Box Cast Distance of Foot
    /// </summary>
    [SerializeField]
    private float f_HeadCast = 1f;

    /// <summary>
    /// Use Script Auto Bounce
    /// </summary>
    [SerializeField]
    private bool b_UseScriptBounce = true;

    /// <summary>
    /// Auto Bounce Velocity
    /// </summary>
    [SerializeField]
    private float f_HeadBounce = 10f;

    private void Awake()
    {
        if (b_UseScriptStart)
            Set_Rigid();
    }

    private void FixedUpdate()
    {
        if (b_UseScriptBounce)
        {
            if (Get_CheckHead() && !Get_CheckFoot())
                //If Jump but Head touch Top >> Set Fall Down
                Set_MoveY_Fall(f_HeadBounce);
        }

        if (b_UseScriptGravity)
        {
            if (!Get_CheckFoot())
                //If not Stand on Ground >> Gravity Set
                Set_MoveY_Gravity(f_GravityFall);
        }
    }

    /// <summary>
    /// Set Rigid by EDITOR
    /// </summary>
    public void Set_Rigid()
    {
        Rigidbody r_Rigidbody = GetComponent<Rigidbody>();
        r_Rigidbody.isKinematic = b_Kinematic;
        r_Rigidbody.useGravity = false;
        if (b_LockRot && !b_LockPos)
            r_Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        else
        if (!b_LockRot && b_LockPos)
            r_Rigidbody.constraints = RigidbodyConstraints.FreezePosition;
        else
            r_Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }

    /// <summary>
    /// Set Rigid by SCRIPT
    /// </summary>
    /// <param name="b_Kinematic"></param>
    /// <param name="b_LockRot">No Rotation</param>
    /// <param name="b_LockPos">No Move</param>
    public void Set_Rigid(bool b_Kinematic, bool b_LockRot, bool b_LockPos)
    {
        Rigidbody r_Rigidbody = GetComponent<Rigidbody>();
        r_Rigidbody.isKinematic = b_Kinematic;
        r_Rigidbody.useGravity = false;
        if (b_LockRot && !b_LockPos)
            r_Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        else
        if (!b_LockRot && b_LockPos)
            r_Rigidbody.constraints = RigidbodyConstraints.FreezePosition;
        else
            r_Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }

    /// <summary>
    /// Move by Velocity
    /// </summary>
    /// <param name="v3_Velocity"></param>
    public void Set_Rigidbody_Velocity(Vector3 v3_Velocity)
    {
        Rigidbody r_Rigidbody = GetComponent<Rigidbody>();
        r_Rigidbody.velocity = v3_Velocity;
    }

    //X

    /// <summary>
    /// Move X by Rigid
    /// </summary>
    /// <param name="i_DirMoveRight"></param>
    /// <param name="f_VelocityMove"></param>
    /// <param name="f_VelocityMoveMax"></param>
    public void Set_MoveX_Velocity(int i_DirMoveRight, float f_VelocityMove, float f_VelocityMoveMax)
    {
        int i_Dir = (i_DirMoveRight > 0) ? 1 : (i_DirMoveRight < 0) ? -1 : 0;
        Rigidbody r_Rigidbody = GetComponent<Rigidbody>();
        if (Mathf.Abs(r_Rigidbody.velocity.x) <= f_VelocityMoveMax)
            r_Rigidbody.AddForce(Vector3.right * f_VelocityMove * i_Dir);
    }

    /// <summary>
    /// Stop X by Rigid
    /// </summary>
    /// <param name="f_VelocityStop"></param>
    public void Set_StopX_Velocity(float f_VelocityStop)
    {
        Rigidbody r_Rigidbody = GetComponent<Rigidbody>();
        if (f_VelocityStop != 0)
            r_Rigidbody.AddForce(Vector3.left * r_Rigidbody.velocity.x * f_VelocityStop);
        else
            r_Rigidbody.AddForce(Vector3.left * r_Rigidbody.velocity.x);
    }

    /// <summary>
    /// Stop X by Rigid Instanly
    /// </summary>
    public void Set_StopX_Velocity()
    {
        Rigidbody r_Rigidbody = GetComponent<Rigidbody>();
        r_Rigidbody.velocity = new Vector3(0, r_Rigidbody.velocity.y, r_Rigidbody.velocity.z);
    }

    /// <summary>
    /// Move by Translate
    /// </summary>
    /// <param name="i_DirMoveRight"></param>
    /// <param name="f_VelocityMove"></param>
    public void Set_MoveX_NotVelocity(int i_DirMoveRight, float f_VelocityMove)
    {
        int i_Dir = (i_DirMoveRight > 0) ? 1 : (i_DirMoveRight < 0) ? -1 : 0;
        transform.Translate(Vector3.right * f_VelocityMove * i_Dir * Time.fixedDeltaTime);
    }

    //Z

    /// <summary>
    /// Move Z by Velocity
    /// </summary>
    /// <param name="i_DirMoveForward"></param>
    /// <param name="f_VelocityMove"></param>
    /// <param name="f_VelocityMoveMax"></param>
    public void Set_MoveZ_Velocity(int i_DirMoveForward, float f_VelocityMove, float f_VelocityMoveMax)
    {
        int i_Dir = (i_DirMoveForward > 0) ? 1 : (i_DirMoveForward < 0) ? -1 : 0;
        Rigidbody r_Rigidbody = GetComponent<Rigidbody>();
        if (Mathf.Abs(r_Rigidbody.velocity.z) <= f_VelocityMoveMax)
            r_Rigidbody.AddForce(Vector3.forward * f_VelocityMove * i_Dir);
    }

    /// <summary>
    /// Stop Z by Velocity
    /// </summary>
    /// <param name="f_VelocityStop"></param>
    public void Set_StopZ_Velocity(float f_VelocityStop)
    {
        Rigidbody r_Rigidbody = GetComponent<Rigidbody>();
        if (f_VelocityStop != 0)
            r_Rigidbody.AddForce(Vector3.back * r_Rigidbody.velocity.z * f_VelocityStop);
        else
            r_Rigidbody.AddForce(Vector3.back * r_Rigidbody.velocity.z);
    }

    /// <summary>
    /// Stop Z by Velocity
    /// </summary>
    public void Set_StopZ_Velocity()
    {
        Rigidbody r_Rigidbody = GetComponent<Rigidbody>();
        r_Rigidbody.velocity = new Vector3(r_Rigidbody.velocity.x, r_Rigidbody.velocity.y, 0);
    }

    //Non-Velocity

    /// <summary>
    /// Move by Move Toward
    /// </summary>
    /// <param name="v_PosMoveTo"></param>
    /// <param name="f_VelocityMove"></param>
    public void Set_Move_NotVelocity_MoveTowards(Vector3 v_PosMoveTo, float f_VelocityMove)
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            new Vector3(v_PosMoveTo.x, v_PosMoveTo.y, v_PosMoveTo.z),
            f_VelocityMove * Time.fixedDeltaTime);
    }

    /// <summary>
    /// Move by Translate
    /// </summary>
    /// <param name="v_DirMove">"Vector3.Up" or "(0; 1; 0)" or etc</param>
    /// <param name="f_VelocityMove"></param>
    public void Set_Move_NotVelocity_Translate(Vector3 v_DirMove, float f_VelocityMove)
    {
        transform.Translate(v_DirMove * f_VelocityMove * Time.fixedDeltaTime);
    }

    //Deg

    /// <summary>
    /// Get Rotation XY
    /// </summary>
    /// <returns></returns>
    public float Get_Rotation_XY()
    {
        Class_Vector cl_Vector = new Class_Vector();

        return cl_Vector.Get_Exchance_Rotate_Unity(cl_Vector.Get_Rot_TransformToVector(transform.rotation).z);
    }

    /// <summary>
    /// Get Rotation XZ
    /// </summary>
    /// <returns>Degree</returns>
    public float Get_Rotation_XZ()
    {
        Class_Vector cl_Vector = new Class_Vector();

        return cl_Vector.Get_Exchance_Rotate_Unity(cl_Vector.Get_Rot_TransformToVector(transform.rotation).y);
    }

    /// <summary>
    /// Set Rotation XY
    /// </summary>
    /// <param name="f_Rotation"></param>
    public void Set_Rotation_XY(float f_Rotation)
    {
        Class_Vector cl_Vector = new Class_Vector();

        transform.rotation = cl_Vector.Get_Rot_VectorToTransform(new Vector3(0, 0, f_Rotation));
    }

    /// <summary>
    /// Set Rotation XZ
    /// </summary>
    /// <param name="f_Rotation"></param>
    public void Set_Rotation_XZ(float f_Rotation)
    {
        Class_Vector cl_Vector = new Class_Vector();

        transform.rotation = cl_Vector.Get_Rot_VectorToTransform(new Vector3(0, f_Rotation, 0));
    }

    /// <summary>
    /// Set Rotation Chance XY
    /// </summary>
    /// <param name="f_RotationChance"></param>
    public void Set_RotationChance_XY(float f_RotationChance)
    {
        Set_Rotation_XY(Get_Rotation_XZ() + f_RotationChance);
    }

    /// <summary>
    /// Set Rotation Chance XZ
    /// </summary>
    /// <param name="f_RotationChance"></param>
    public void Set_RotationChance_XZ(float f_RotationChance)
    {
        Set_Rotation_XZ(Get_Rotation_XZ() + f_RotationChance);
    }

    /// <summary>
    /// Set Move by Rotation
    /// </summary>
    /// <param name="f_Rotation"></param>
    /// <param name="f_VelocityMove"></param>
    public void Set_MoveRotation_XZ(float f_Rotation, float f_VelocityMove)
    {
        Class_Vector cl_Vector = new Class_Vector();

        Rigidbody r_Rigidbody = GetComponent<Rigidbody>();

        r_Rigidbody.AddForce(
            cl_Vector.Get_DegToVector_XZ(
                cl_Vector.Get_Exchance_Rotate_Unity(-f_Rotation), 1).normalized * f_VelocityMove);
    }

    //Y

    /// <summary>
    /// Set Jump
    /// </summary>
    /// <param name="f_VelocityJump"></param>
    public void Set_MoveY_Jump(float f_VelocityJump)
    {
        Rigidbody r_Rigidbody = GetComponent<Rigidbody>();
        r_Rigidbody.velocity = new Vector3(r_Rigidbody.velocity.x, f_VelocityJump, r_Rigidbody.velocity.z);
    }

    /// <summary>
    /// Set Gravity
    /// </summary>
    /// <param name="f_VelocityGravity"></param>
    public void Set_MoveY_Gravity(float f_VelocityGravity)
    {
        Rigidbody r_Rigidbody = GetComponent<Rigidbody>();
        r_Rigidbody.AddForce(Vector3.down * f_VelocityGravity);
    }

    /// <summary>
    /// Set Fall
    /// </summary>
    /// <param name="f_VelocityFall"></param>
    public void Set_MoveY_Fall(float f_VelocityFall)
    {
        Rigidbody r_Rigidbody = GetComponent<Rigidbody>();
        r_Rigidbody.velocity = new Vector3(r_Rigidbody.velocity.x, -f_VelocityFall, r_Rigidbody.velocity.z);
    }

    //Check

    /// <summary>
    /// Check Foot
    /// </summary>
    /// <returns></returns>
    public bool Get_CheckFoot()
    {
        Class_Vector cl_Vector = new Class_Vector();

        Class_Eye cs_Eye = new Class_Eye();

        return cs_Eye.Get_BoxCast_Dir_Check(
            transform.position,
            v3_FootCast,
            Vector3.down,
            cl_Vector.Get_Rot_TransformToVector(transform.rotation),
            f_FootCast,
            l_GroundCheck);
    }

    /// <summary>
    /// Check Head
    /// </summary>
    /// <returns></returns>
    public bool Get_CheckHead()
    {
        Class_Vector cl_Vector = new Class_Vector();

        Class_Eye cs_Eye = new Class_Eye();

        return cs_Eye.Get_BoxCast_Dir_Check(
            transform.position,
            v3_HeadCast,
            Vector3.up,
            cl_Vector.Get_Rot_TransformToVector(transform.rotation),
            f_HeadCast,
            l_GroundCheck);
    }

    //Gizmos

    private void OnDrawGizmos()
    {
        if (b_FootDebug)
        {
            //Foot Check
            if (Get_CheckFoot())
                Gizmos.color = Color.red;
            else
                Gizmos.color = Color.white;

            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * f_FootCast);
            Gizmos.DrawWireCube(transform.position + Vector3.down * f_FootCast, v3_FootCast);
        }

        if (b_HeadDebug)
        {
            //Head Check
            if (Get_CheckHead())
                Gizmos.color = Color.red;
            else
                Gizmos.color = Color.white;

            Gizmos.DrawLine(transform.position, transform.position + Vector3.up * f_HeadCast);
            Gizmos.DrawWireCube(transform.position + Vector3.up * f_HeadCast, v3_HeadCast);
        }
    }
}