using UnityEngine;

/// <summary>
/// Working on Vector
/// </summary>
public class Class_Vector
//Vector Primary
{
    public Class_Vector()
    {

    }

    #region Distance

    /// <summary>
    /// Receive Distance Between 2 Vector Point
    /// </summary>
    /// <param name="v_Start"></param>
    /// <param name="v_End"></param>
    /// <returns></returns>
    public float Get_Distance(Vector3 v_Start, Vector3 v_End)
    {
        return Vector3.Distance(v_Start, v_End);
    }

    /// <summary>
    /// Receive Distance Between 2 GameObject Point
    /// </summary>
    /// <param name="t_Start"></param>
    /// <param name="t_End"></param>
    /// <returns></returns>
    public float Get_Distance(Transform t_Start, Transform t_End)
    {
        return Vector3.Distance(t_Start.position, t_End.position);
    }

    //===2: Vector on Circle

    //===2.1: Exchance Deg

    /// <summary>
    /// Exchance Rotate Follow Primary
    /// </summary>
    /// <param name="f_DegNeedExchance"></param>
    /// <returns>Return Rotation on Degree</returns>
    public float Get_Exchance_Rotate_Script(float f_DegNeedExchance)
    {
        if (f_DegNeedExchance >= 360)
            return f_DegNeedExchance - 360 * (f_DegNeedExchance / 360);
        else
        if (f_DegNeedExchance <= 0)
            return 360 * (Mathf.Abs(f_DegNeedExchance) / 360 + 1) + f_DegNeedExchance;
        return f_DegNeedExchance;
    }

    /// <summary>
    /// Exchance Rotate Follow Unity
    /// </summary>
    /// <param name="f_DegNeedExchance"></param>
    /// <returns>Return Rotation on Degree</returns>
    public float Get_Exchance_Rotate_Unity(float f_DegNeedExchance)
    {
        if (f_DegNeedExchance <= -180)
            return 360 + f_DegNeedExchance;
        else
        if (f_DegNeedExchance >= 180)
            return (360 - f_DegNeedExchance) * -1;
        return f_DegNeedExchance;
    }

    #region XY

    /// <summary>
    /// Receive Vector Point Based on Circle
    /// </summary>
    /// <param name="f_Angle"></param>
    /// <param name="f_Duration"></param>
    /// <returns></returns>
    public Vector3 Get_DegToVector_XY(float f_Angle, float f_Duration)
    {
        return new Vector3(Mathf.Cos(f_Angle * Mathf.Deg2Rad), Mathf.Sin(f_Angle * Mathf.Deg2Rad), 0) * f_Duration;
    }

    /// <summary>
    /// Receive Dir Between 2 Vector Point
    /// </summary>
    /// <param name="v2_Start"></param>
    /// <param name="v2_End"></param>
    /// <returns></returns>
    public Vector2 Get_Dir_XY(Vector2 v2_Start, Vector2 v2_End)
    {
        return (v2_End - v2_Start).normalized;
    }

    /// <summary>
    /// Receive Offset Rotate Between 2 Vector Dir
    /// </summary>
    /// <param name="v2_DirStart"></param>
    /// <param name="v2_DirEnd"></param>
    /// <returns></returns>
    public float Get_DirToDeg_XY(Vector2 v2_DirStart, Vector2 v2_DirEnd)
    {
        return Vector2.Angle(v2_DirStart, v2_DirEnd);
    }

    #endregion

    #region XZ

    /// <summary>
    /// Receive Vector Point Based on trên Circle
    /// </summary>
    /// <param name="f_Angle"></param>
    /// <param name="f_Duration"></param>
    /// <returns></returns>
    public Vector3 Get_DegToVector_XZ(float f_Angle, float f_Duration)
    {
        return new Vector3(Mathf.Cos(f_Angle * Mathf.Deg2Rad), 0, Mathf.Sin(f_Angle * Mathf.Deg2Rad)) * f_Duration;
    }

    /// <summary>
    /// Receive Dir Between 2 Vector Point
    /// </summary>
    /// <param name="v3_Start"></param>
    /// <param name="v3_End"></param>
    /// <returns></returns>
    public Vector3 Get_Dir_XZ(Vector3 v3_Start, Vector3 v3_End)
    {
        return (v3_End - v3_Start).normalized;
    }

    /// <summary>
    /// Receive Offset Rotate Between 2 Vector Dir
    /// </summary>
    /// <param name="v3_VecStart"></param>
    /// <param name="v3_DirEnd"></param>
    /// <returns></returns>
    public float Get_DirToDeg_XZ(Vector3 v3_VecStart, Vector3 v3_DirEnd)
    {
        Vector2 v2_DirStart = new Vector2(v3_VecStart.x, v3_VecStart.z);
        Vector2 v2_DirEnd = new Vector2(v3_DirEnd.x, v3_DirEnd.z);
        return Vector2.Angle(v2_DirStart, v2_DirEnd);
    }

    /// <summary>
    /// Receive Offset Rotate Between 2 Object, with Main Object Rotate
    /// </summary>
    /// <param name="t_Main"></param>
    /// <param name="t_Check"></param>
    /// <returns></returns>
    public float Get_DirToDeg_XZ_RotateFromTransform(Transform t_Main, Transform t_Check)
    {
        float f_Distance = Vector3.Distance(t_Main.transform.position, t_Check.position);
        float f_Deg = Get_Rot_TransformToVector(t_Main.transform.rotation).y;

        Vector3 v3_DirStart = Get_Dir_XZ(t_Main.transform.position, t_Main.transform.position + Get_DegToVector_XZ(-f_Deg, f_Distance));
        Vector3 v3_DirEnd = Get_Dir_XZ(t_Main.transform.position, t_Main.transform.position + Get_Dir_XZ(t_Main.transform.position, t_Check.position) * f_Distance);

        return Get_DirToDeg_XZ(v3_DirStart, v3_DirEnd);
    }

    #endregion

    #endregion

    #region Rotate & Transform Rotate

    /// <summary>
    /// Receive Rotate To Add Transform Rotate
    /// </summary>
    /// <param name="f_DegX_Chance"></param>
    /// <param name="f_DegY_Chance"></param>
    /// <param name="f_DegZ_Chance"></param>
    /// <returns></returns>
    public Quaternion Get_Rot_VectorToTransform(float f_DegX_Chance, float f_DegY_Chance, float f_DegZ_Chance)
    {
        Vector3 v3_RotationVector = new Vector3(f_DegX_Chance, f_DegY_Chance, f_DegZ_Chance);
        Quaternion q_Rotation = Quaternion.Euler(v3_RotationVector);
        return q_Rotation;
    }

    /// <summary>
    /// Receive Rotate To Add Transform Rotate
    /// </summary>
    /// <param name="v3_RotationChance"></param>
    /// <returns></returns>
    public Quaternion Get_Rot_VectorToTransform(Vector3 v3_RotationChance)
    {
        Quaternion q_Rotation = Quaternion.Euler(v3_RotationChance);
        return q_Rotation;
    }

    /// <summary>
    /// Receive Rotate From Transform Rotate [0..360]
    /// </summary>
    /// <param name="q_Rotation"></param>
    /// <returns></returns>
    public Vector3 Get_Rot_TransformToVector(Quaternion q_Rotation)
    {
        Vector3 v3_Rotation = q_Rotation.eulerAngles;
        return v3_Rotation;
    }

    #endregion

    #region Isometric (2.5D 45' Dec)

    #region Dir

    /// <summary>
    /// Dir(-1, 0) on Isometric Square
    /// </summary>
    public readonly Vector2Int v2_Isometric_DirUp = new Vector2Int(-1, 0);

    /// <summary>
    /// Dir(+1, 0) on Isometric Square
    /// </summary>
    /// <returns></returns>
    public readonly Vector2Int v2_Isometric_DirDown = new Vector2Int(1, 0);

    /// <summary>
    /// Dir(0, -1) on Isometric Square
    /// </summary>
    /// <returns></returns>
    public readonly Vector2Int v2_Isometric_DirLeft = new Vector2Int(0, -1);

    /// <summary>
    /// Dir(0, +1) on Isometric Square
    /// </summary>
    /// <returns></returns>
    public readonly Vector2Int v2_Isometric_DirRight = new Vector2Int(0, 1);

    #endregion

    #region Isometric Fixed Depth

    //Primary

    /// <summary>
    /// Get Fixed Depth of Ground Vector
    /// </summary>
    /// <remarks>
    /// Chance [Z] of TRANSFORM
    /// </remarks>
    /// <param name="v2_Pos">Dir X is [UP;DOWN] and Dir Y [LEFT;RIGHT]</param>
    /// <returns>Use for 'Get_Isometric_TransformPosition()'</returns>
    public Vector3Int Get_Isometric_FixedDepth(Vector2Int v2_Pos)
    {
        //Primary
        Vector3Int v3_Pos = new Vector3Int(v2_Pos.x, v2_Pos.y, 0);

        //Fixed
        v3_Pos.z += (v2_Pos.y - v2_Pos.x);

        return v3_Pos;
    }

    /// <summary>
    /// Get Fixed Depth of Ground Vector
    /// </summary>
    /// <remarks>
    /// Chance [Z] of TRANSFORM
    /// </remarks>
    /// <param name="v2_Pos">Dir X is [UP;DOWN] and Dir Y [LEFT;RIGHT]</param>
    /// <returns>Use for 'Get_Isometric_TransformPosition()'</returns>
    public Vector3 Get_Isometric_FixedDepth_Ground(Vector2 v2_Pos)
    {
        //Primary
        Vector3 v3_Pos = new Vector3(v2_Pos.x, v2_Pos.y, 0);

        //Fixed
        v3_Pos.z += (v2_Pos.y - v2_Pos.x);

        return v3_Pos;
    }

    /// <summary>
    /// Get Fixed Depth of Object Vector ontop Ground
    /// </summary>
    /// <remarks>
    /// Chance [Z] of TRANSFORM
    /// </remarks>
    /// <param name="v2_Pos">Dir X is [UP;DOWN] and Dir Y [LEFT;RIGHT]</param>
    /// <param name="f_Depth"></param>
    /// <param name="f_Centre">Centre on Ground</param>
    /// <returns>Use for 'Get_Isometric_TransformPosition()'</returns>
    public Vector3 Get_Isometric_FixedDepth_OnGround(Vector2 v2_Pos, float f_Depth, float f_Centre)
    {
        //Primary
        Vector3 v3_Pos = new Vector3(v2_Pos.x, v2_Pos.y, 0);

        //Fixed
        v3_Pos.z += (v2_Pos.y - v2_Pos.x);
        v3_Pos.z += f_Depth;
        v3_Pos.x -= (f_Centre + 1);
        v3_Pos.y += (f_Centre + 1);

        return v3_Pos;
    }

    //On Map

    /// <summary>
    /// Get Fixed Depth of Ground Vector
    /// </summary>
    /// <remarks>
    /// Chance [Z] of TRANSFORM
    /// </remarks>
    /// <param name="v2_Pos">Dir X is [UP;DOWN] and Dir Y [LEFT;RIGHT]</param>
    /// <returns>Use for 'Get_Isometric_TransformPosition()'</returns>
    public Vector3 Get_Isometric_FixedDepth_Ground(Vector2 v2_Pos, float f_Floor, float f_ConstDepth)
    {
        //Primary
        Vector3 v3_Pos = new Vector3(v2_Pos.x, v2_Pos.y, 0);

        //Fixed
        v3_Pos.z += ((f_Floor / f_ConstDepth) + (v2_Pos.y - v2_Pos.x));

        return v3_Pos;
    }

    /// <summary>
    /// Get Fixed Depth of Object Vector ontop Ground
    /// </summary>
    /// <remarks>
    /// Chance [Z] of TRANSFORM
    /// </remarks>
    /// <param name="v2_Pos">Dir X is [UP;DOWN] and Dir Y [LEFT;RIGHT]</param>
    /// <param name="f_Depth"></param>
    /// <param name="f_Centre">Centre on Ground</param>
    /// <returns>Use for 'Get_Isometric_TransformPosition()'</returns>
    public Vector3 Get_Isometric_FixedDepth_OnGround(Vector2 v2_Pos, float f_Floor, float f_Depth, float f_Centre, float f_ConstDepth)
    {
        //Primary
        Vector3 v3_Pos = new Vector3(v2_Pos.x, v2_Pos.y, 0);

        //Fixed
        v3_Pos.z += ((f_Floor / f_ConstDepth) + (v2_Pos.y - v2_Pos.x) - 0.75f);
        v3_Pos.z += f_Depth;
        v3_Pos.x -= (f_Centre + 1);
        v3_Pos.y += (f_Centre + 1);

        return v3_Pos;
    }

    //Transform

    /// <summary>
    /// Get Pos Isometric for Transform.position
    /// </summary>
    /// <remarks>
    /// Chance [X;Y] of TRANSFORM
    /// </remarks>
    /// <param name="v3_Pos">Get from 'Get_Pos_ForIsometric()'</param>
    /// <returns></returns>
    public Vector3 Get_Isometric_TransformPosition(Vector3 v3_Pos, Vector2 v2_Centre, float f_Floor)
    {
        //Primary
        Vector3 v3_Transform = new Vector3(v3_Pos.x + v3_Pos.y, 0.5f * (v3_Pos.y - v3_Pos.x), v3_Pos.z);

        //Fixed
        v3_Transform += new Vector3(v2_Centre.x, v2_Centre.y + f_Floor, 0);

        return v3_Transform;
    }

    #endregion

    //Chance Transform Back to Isometric Pos

    //public Vector2 Get_Pos_IsometricToXY(Vector2 v_PosOnMap)
    //{
    //    return new Vector2(0.5f * v_PosOnMap.x - v_PosOnMap.y, 0.5f * v_PosOnMap.x + v_PosOnMap.y);
    //}

    #endregion

    //===5: Vector and VectorInt

    /// <summary>
    /// Chance Vector to VectorInt
    /// </summary>
    /// <param name="v2_Vector"></param>
    /// <returns></returns>
    public Vector2Int Get_VectorInt(Vector2 v2_Vector)
    {
        return new Vector2Int((int)v2_Vector.x, (int)v2_Vector.y);
    }

    /// <summary>
    /// Chance Vector to VectorInt
    /// </summary>
    /// <param name="v3_Vector"></param>
    /// <returns></returns>
    public Vector3Int Get_VectorInt(Vector3 v3_Vector)
    {
        return new Vector3Int((int)v3_Vector.x, (int)v3_Vector.y, (int)v3_Vector.z);
    }
}