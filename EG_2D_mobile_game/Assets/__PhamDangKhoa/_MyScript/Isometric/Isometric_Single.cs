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
    [Header("Map Manager Tag")]
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
    [Header("On Map")]
    [SerializeField]
    private Vector2 v2_Pos = new Vector2();

    /// <summary>
    /// Floor on Map (Use by non-Character)
    /// </summary>
    [SerializeField]
    [Range(0,9)]
    private int i_Floor = 0;

    /// <summary>
    /// Pos Offset for Map
    /// </summary>
    [SerializeField]
    private Vector2 v2_Offset = new Vector2(0, 0);

    /// <summary>
    /// Fix
    /// </summary>
    [Header("On Square")]
    [SerializeField]
    private Vector2 v2_Centre = new Vector2(0, 0);

    /// <summary>
    /// Check if this Object is Ground (not Character, Burden, etc...)
    /// </summary>
    [Header("Is Object (Isn't Ground) on Map")]
    [SerializeField]
    private bool b_isObject = false;

    /// <summary>
    /// Object Stand on Centre Ground
    /// </summary>
    [SerializeField]
    [Range(0,1)]
    private float f_Centre = 0f;

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

    #region Transform

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
                v3_Pos = cl_Vector.Get_Isometric_FixedDepth(
                    v2_Pos + v2_Offset,
                    i_Floor,
                    f_Depth, 
                    f_Centre, 
                    cl_MapString.Get_MapSize());
            }
            else
            {
                v3_Pos = cl_Vector.Get_Isometric_FixedDepth(
                    v2_Pos + v2_Offset, 
                    f_Depth, 
                    f_Centre);
            }
        }
        else
        {
            if (cl_MapManager != null)
            {
                v3_Pos = cl_Vector.Get_Isometric_FixedDepth(
                    v2_Pos + v2_Offset,
                    i_Floor,
                    cl_MapString.Get_MapSize());
            }
            else
            {
                v3_Pos = cl_Vector.Get_Isometric_FixedDepth(
                    v2_Pos + v2_Offset);
            }
        }

        Vector3 v3_Transform = cl_Vector.Get_Isometric_TransformPosition(v3_Pos);
        v3_Transform += new Vector3(v2_Centre.x, v2_Centre.y, 0);
        v3_Transform.y += i_Floor;

        this.transform.position = v3_Transform;
    }

    #endregion

    #region Pos and Floor

    /// <summary>
    /// Set Pos for this Isometric
    /// </summary>
    /// <param name="v2_Pos"></param>
    public void Set_Isometric_Pos(Vector2 v2_Pos)
    {
        this.v2_Pos = v2_Pos;

        Set_Isometric_Transform();
    }

    /// <summary>
    /// Set Pos for this Isometric
    /// </summary>
    /// <param name="f_Pos_x"></param>
    /// <param name="f_Pos_y"></param>
    public void Set_Isometric_Pos(float f_Pos_x, float f_Pos_y)
    {
        Set_Isometric_Pos(new Vector2(f_Pos_x, f_Pos_y));
    }

    /// <summary>
    /// Set Floor for this Isometric
    /// </summary>
    /// <param name="i_Floor"></param>
    public void Set_Isometric_Floor(int i_Floor)
    {
        this.i_Floor = i_Floor;

        Set_Isometric_Transform();
    }

    /// <summary>
    /// Set Pos and Floor for this Isometric
    /// </summary>
    /// <param name="v2_PosOnMap"></param>
    public void Set_Isometric(Vector2 v2_PosOnMap, int i_Floor)
    {
        this.v2_Pos = v2_PosOnMap;
        this.i_Floor = i_Floor;

        Set_Isometric_Transform();
    }

    /// <summary>
    /// Set Pos and Floor for this Isometric
    /// </summary>
    /// <param name="f_PosOnMap_x"></param>
    /// <param name="f_PosOnMap_y"></param>
    public void Set_Isometric(float f_PosOnMap_x, float f_PosOnMap_y, int i_Floor)
    {
        Set_Isometric(new Vector2(f_PosOnMap_x, f_PosOnMap_y), i_Floor);
    }

    /// <summary>
    /// Get Pos of this Isometric
    /// </summary>
    /// <returns></returns>
    public Vector2 Get_Isometric_Pos()
    {
        return v2_Pos;
    }

    /// <summary>
    /// Get Floor of this Isometric
    /// </summary>
    /// <returns></returns>
    public int Get_Isometric_Floor()
    {
        return this.i_Floor;
    }

    #endregion

    #region Offset

    /// <summary>
    /// Set Offset for this Isometric
    /// </summary>
    /// <param name="v2_Pos"></param>
    public void Set_Isometric_OffsetOnMap(Vector2 v2_OffsetOnMap)
    {
        this.v2_Offset = v2_OffsetOnMap;

        Set_Isometric_Transform();
    }

    /// <summary>
    /// Get Offset of this Isometric
    /// </summary>
    /// <returns></returns>
    public Vector2 Get_Isometric_OffsetOnMap()
    {
        return v2_Offset;
    }

    #endregion

    #region Is Object

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

    #endregion

    #region Single Code

    /// <summary>
    /// Get Single Code for this Isometric
    /// </summary>
    /// <returns></returns>
    public char Get_SingleCode()
    {
        return c_SingleCode;
    }

    #endregion
}
