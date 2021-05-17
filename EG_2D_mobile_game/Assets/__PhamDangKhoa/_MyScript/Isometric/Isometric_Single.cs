using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Add this Script on "2.5D Sprite" with "Ground", "Object" like "Box", "Character", etc...
/// </summary>
/// <remarks>
/// Use with Camera set 'Orthographic'
/// </remarks>
[ExecuteAlways]
public class Isometric_Single : MonoBehaviour
{
    //Notice: This Script will auto Run in Edit Mode, and not when in Play Mode if this is Ground

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
    /// Pos on Map this Object
    /// </summary>
    [Header("Pos on Map")]
    //Use to Show Private Varible on Unity
    [SerializeField]
    private Vector2 v2_Pos = new Vector2();

    /// <summary>
    /// Pos Offset for Map
    /// </summary>
    [SerializeField]
    private Vector2 v2_Offset = new Vector2(0, 0);

    /// <summary>
    /// Check if this Object is Ground (not Character, Burden, etc...)
    /// </summary>
    [Header("Object on Map (Not Ground)")]
    [SerializeField]
    private bool b_isObject = false;

    /// <summary>
    /// Object Stand on Centre Ground
    /// </summary>
    [SerializeField]
    private float f_Centre = 0.3f;

    /// <summary>
    /// Layer of this Isometric between other Isometric (Set 'i_Layer_Max' in 'IsometricMap' Object)
    /// </summary>
    [SerializeField]
    private float f_Depth = 0;

    #endregion

    /// <summary>
    /// Pos on Environmemt
    /// </summary>
    private Vector3 v3_Pos = new Vector3();

    private void Start()
    {
        if (cl_Map == null)
        {
            if (s_Tag != "")
            {
                cl_Map = GameObject.FindGameObjectWithTag(s_Tag).GetComponent<Isometric_Map>();

                if (cl_Map == null)
                {
                    Debug.LogError("Not found 'Isometric Map Object' with tag: " + s_Tag);
                }
            }
        }
    }

    private void Update()
    {
        Set_Auto();
    }

    /// <summary>
    /// Set on Map Auto on Edit Mode
    /// </summary>
    private void Set_Auto()
    {
        Class_Vector cl_Vector = new Class_Vector();

        if (b_isObject)
        {
            if (cl_Map != null) 
            {
                v3_Pos = cl_Vector.Get_Isometric_FixedDepth(v2_Pos + v2_Offset, f_Centre, f_Depth, cl_Map.Get_MapSize());
            }
            else
            {
                v3_Pos = cl_Vector.Get_Isometric_FixedDepth(v2_Pos + v2_Offset, f_Centre);
            }
        }
        else
        {
            v3_Pos = cl_Vector.Get_Isometric_FixedDepth(v2_Pos + v2_Offset);
        }

        this.transform.position = cl_Vector.Get_Isometric_TransformPosition(v3_Pos);
    }

    /// <summary>
    /// Set Pos for this Isometric
    /// </summary>
    /// <param name="v2_Pos"></param>
    public void Set_Pos(Vector2 v2_Pos)
    {
        this.v2_Pos = v2_Pos;
    }

    /// <summary>
    /// Set Pos for this Isometric
    /// </summary>
    /// <param name="f_Pos_x"></param>
    /// <param name="f_Pos_y"></param>
    public void Set_Pos(float f_Pos_x, float f_Pos_y)
    {
        Set_Pos(new Vector2(f_Pos_x, f_Pos_y));
    }

    /// <summary>
    /// Get Pos of this Isometric
    /// </summary>
    /// <returns></returns>
    public Vector2 Get_Pos()
    {
        return v2_Pos;
    }

    /// <summary>
    /// Set Offset for this Isometric
    /// </summary>
    /// <param name="v2_Pos"></param>
    public void Set_Offset(Vector2 v2_Offset)
    {
        this.v2_Offset = v2_Offset;
    }

    /// <summary>
    /// Get Offset of this Isometric
    /// </summary>
    /// <returns></returns>
    public Vector2 Get_Offset()
    {
        return v2_Offset;
    }

    public bool Get_isObject()
    {
        return b_isObject;
    }

    public void Set_isObject(bool b_isObject)
    {
        this.b_isObject = b_isObject;
    }
}
