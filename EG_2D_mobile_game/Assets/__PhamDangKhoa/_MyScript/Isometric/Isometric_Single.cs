using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Add this Script on "2.5D Sprite" with "Ground", "Object" like "Box", "Character", etc...
/// </summary>
/// <remarks>
/// Use with Camera set 'Orthographic'
/// </remarks>
public class Isometric_Single : MonoBehaviour
{
    //Notice: This Script will auto Run in Edit Mode, and not when in Play Mode if this is Ground

    #region Public Varible

    /// <summary>
    /// Tag Isometric Object to Find
    /// </summary>
    [Header("Isometric Map Tag")]
    [SerializeField]
    private string s_Tag = "IsometricMap";

    /// <summary>
    /// Map Manager GameObject
    /// </summary>
    [SerializeField]
    private GameObject g_MapManager;

    /// <summary>
    /// Pos on Map this Object
    /// </summary>
    [Header("Pos on Map")]
    [SerializeField]
    private Vector2 v2_PosOnMap = new Vector2();

    /// <summary>
    /// Pos Offset for Map
    /// </summary>
    [SerializeField]
    private Vector2 v2_OffsetOnMap = new Vector2(0, 0);

    /// <summary>
    /// Fix
    /// </summary>
    [Header("Centre on Square")]
    [SerializeField]
    private Vector2 v2_CentreOnSquare = new Vector2(0, 0);

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
    private float f_Centre = 1f;

    /// <summary>
    /// Layer of this Isometric between other Isometric (Set 'i_Layer_Max' in 'IsometricMap' Object)
    /// </summary>
    [SerializeField]
    private float f_Depth = 0;

    /// <summary>
    /// Single Code for Isometric Ground, Object, Fence on Map
    /// </summary>
    [Header("Single Code on Map")]
    [SerializeField]
    private char c_SingleCode = 'A';

    #endregion

    private Isometric_MapManager cl_MapManager;

    private Isometric_MapString cl_MapString;

    /// <summary>
    /// Pos on Environmemt
    /// </summary>
    private Vector3 v3_Pos = new Vector3();

    private void Start()
    {
        if (cl_MapManager == null)
        {
            if (s_Tag != "")
            {
                g_MapManager = GameObject.FindGameObjectWithTag(s_Tag);

                if (cl_MapManager == null)
                {
                    cl_MapManager = g_MapManager.GetComponent<Isometric_MapManager>();
                    cl_MapString = g_MapManager.GetComponent<Isometric_MapString>();
                }
            }
        }

        Set_Isometric_Transform();
    }

    private void OnDrawGizmosSelected()
    {
        Set_Isometric_Transform();
    }

    //Transform

    /// <summary>
    /// Set on Map Auto on Edit Mode
    /// </summary>
    private void Set_Isometric_Transform()
    {
        Class_Vector cl_Vector = new Class_Vector();

        if (b_isObject)
        {
            if (cl_MapManager != null)
            {
                v3_Pos = cl_Vector.Get_Isometric_FixedDepth(v2_PosOnMap + v2_OffsetOnMap, f_Depth, f_Centre, cl_MapString.Get_MapSize());
            }
            else
            {
                v3_Pos = cl_Vector.Get_Isometric_FixedDepth(v2_PosOnMap + v2_OffsetOnMap, f_Depth, f_Centre);
            }
        }
        else
        {
            v3_Pos = cl_Vector.Get_Isometric_FixedDepth(v2_PosOnMap + v2_OffsetOnMap);
        }

        Vector3 v3_Transform = cl_Vector.Get_Isometric_TransformPosition(v3_Pos);
        v3_Transform += new Vector3(v2_CentreOnSquare.x, v2_CentreOnSquare.y, 0);
        this.transform.position = v3_Transform;
    }

    //Pos

    /// <summary>
    /// Set Pos for this Isometric
    /// </summary>
    /// <param name="v2_PosOnMap"></param>
    public void Set_Isometric_PosOnMap(Vector2 v2_PosOnMap)
    {
        this.v2_PosOnMap = v2_PosOnMap;

        Set_Isometric_Transform();
    }

    /// <summary>
    /// Set Pos for this Isometric
    /// </summary>
    /// <param name="f_PosOnMap_x"></param>
    /// <param name="f_PosOnMap_y"></param>
    public void Set_Isometric_PosOnMap(float f_PosOnMap_x, float f_PosOnMap_y)
    {
        Set_Isometric_PosOnMap(new Vector2(f_PosOnMap_x, f_PosOnMap_y));

        Set_Isometric_Transform();
    }

    /// <summary>
    /// Get Pos of this Isometric
    /// </summary>
    /// <returns></returns>
    public Vector2 Get_Isometric_PosOnMap()
    {
        return v2_PosOnMap;
    }

    //Offset

    /// <summary>
    /// Set Offset for this Isometric
    /// </summary>
    /// <param name="v2_Pos"></param>
    public void Set_Isometric_OffsetOnMap(Vector2 v2_OffsetOnMap)
    {
        this.v2_OffsetOnMap = v2_OffsetOnMap;

        Set_Isometric_Transform();
    }

    /// <summary>
    /// Get Offset of this Isometric
    /// </summary>
    /// <returns></returns>
    public Vector2 Get_Isometric_OffsetOnMap()
    {
        return v2_OffsetOnMap;
    }

    //Is Object

    /// <summary>
    /// Set Object check for this Isometric
    /// </summary>
    /// <returns></returns>
    public bool Get_isObject()
    {
        return b_isObject;
    }

    /// <summary>
    /// Get Object check for this Isometric
    /// </summary>
    /// <param name="b_isObject"></param>
    public void Set_isObject(bool b_isObject)
    {
        this.b_isObject = b_isObject;
    }

    //Single Code

    /// <summary>
    /// Get Single Code for this Isometric
    /// </summary>
    /// <returns></returns>
    public char Get_SingleCode()
    {
        return c_SingleCode;
    }
}
