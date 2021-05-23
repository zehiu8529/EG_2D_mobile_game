using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* - ISOMETRIC SQUARE:
 * 
 *                  ..
 *    UP(-1;+0)   ......   RIGHT(+0;+1)
 *              ..........
 *            .............. --> SQUARE(0;0)
 *              ..........
 *  LEFT(+0,-1)   ......   DOWN(+1;+0)
 *                  ..
 *  
 */

/// <summary>
/// Save Single Code and Single Isometric GameObject of Square on Matrix Map
/// </summary>
public class Isometric_MapRenderer : MonoBehaviour
{
    #region Public Varible

    /// <summary>
    /// List Isometric GameObject Ground
    /// </summary>
    [Header("Ground Square")]
    [SerializeField]
    private List<GameObject> l_Ground;

    /// <summary>
    /// List Ground Code of List Isometric GameObject Ground
    /// </summary>
    [SerializeField]
    private List<char> l_Code_Ground;

    /// <summary>
    /// List Isometric GameObject Object
    /// </summary>
    [Header("Object Square")]
    [SerializeField]
    private List<GameObject> l_Object;

    /// <summary>
    /// List Object Code of List Isometric GameObject Object
    /// </summary>
    [SerializeField]
    private List<char> l_Code_Object;

    /// <summary>
    /// List Isometric GameObject Fence Up
    /// </summary>
    [Header("Fence Up Square")]
    [SerializeField]
    private List<GameObject> l_Fence_Up;

    /// <summary>
    /// List Fence Up Code of List Isometric Game Object Fence Up
    /// </summary>
    [SerializeField]
    private List<char> l_Code_Fence_Up;

    /// <summary>
    /// List Isometric GameObject Fence Down
    /// </summary>
    [Header("Fence Down Square")]
    [SerializeField]
    private List<GameObject> l_Fence_Down;

    /// <summary>
    /// List Fence Down Code of List Isometric Game Object Fence Down
    /// </summary>
    [SerializeField]
    private List<char> l_Code_Fence_Down;

    /// <summary>
    /// List Isometric GameObject Fence Left
    /// </summary>
    [Header("Fence Left Square")]
    [SerializeField]
    private List<GameObject> l_Fence_Left;

    /// <summary>
    /// List Fence Left Code of List Isometric Game Object Fence Left
    /// </summary>
    [SerializeField]
    private List<char> l_Code_Fence_Left;

    /// <summary>
    /// List Isometric GameObject Fence Right
    /// </summary>
    [Header("Fence Right Square")]
    [SerializeField]
    private List<GameObject> l_Fence_Right;

    /// <summary>
    /// List Fence Right Code of List Isometric Game Object Fence Right
    /// </summary>
    [SerializeField]
    private List<char> l_Code_Fence_Right;

    /// <summary>
    /// Emty Code use for GROUND, OBJECT and FENCE CODE
    /// </summary>
    /// <remarks>
    /// At Normally, Emty Code is a SPACE Chacracter or a ' ' Character
    /// </remarks>
    [Header("Emty Code")]
    [SerializeField]
    private char c_EmtyCode = ' ';

    #endregion

    #region Ground List Manager

    /// <summary>
    /// Get Count List of Ground
    /// </summary>
    /// <returns></returns>
    public int Get_CountList_Ground()
    {
        if (l_Ground.Count == l_Code_Ground.Count)
        {
            return l_Code_Ground.Count;
        }
        Debug.LogError("Get_CountList_Ground: Both List not same Count!");
        return 0;
    }

    /// <summary>
    /// Get Isometric GameObject from List Ground
    /// </summary>
    /// <param name="i_GroundListIndex"></param>
    /// <returns></returns>
    public GameObject Get_GameObject_Ground(int i_GroundListIndex)
    {
        if (i_GroundListIndex < 0 || i_GroundListIndex >= Get_CountList_Ground())
        {
            Debug.LogError("Get_GameObject_Ground: Out Index of List Ground!");
            return null;
        }
        return l_Ground[i_GroundListIndex];
    }

    /// <summary>
    /// Get Isometric Square Code from List Ground
    /// </summary>
    /// <param name="i_GroundListIndex"></param>
    /// <returns></returns>
    public char Get_SingleCode_Ground(int i_GroundListIndex)
    {
        if (i_GroundListIndex < 0 || i_GroundListIndex >= Get_CountList_Ground())
        {
            Debug.LogError("Get_SingleCode_Ground: Out Index of List Ground!");
            return Get_EmtyCode();
        }
        return l_Code_Ground[i_GroundListIndex];
    }

    #endregion

    #region Object List Manager

    /// <summary>
    /// Get Count List of Object
    /// </summary>
    /// <returns></returns>
    public int Get_CountList_Object()
    {
        if (l_Object.Count == l_Code_Object.Count)
        {
            return l_Code_Object.Count;
        }
        Debug.LogError("Get_CountList_Object: Both List not same Count!");
        return 0;
    }

    /// <summary>
    /// Get Isometric GameObject from List Object
    /// </summary>
    /// <param name="i_ObjectListIndex"></param>
    /// <returns></returns>
    public GameObject Get_GameObject_Object(int i_ObjectListIndex)
    {
        if (i_ObjectListIndex < 0 || i_ObjectListIndex >= Get_CountList_Object())
        {
            Debug.LogError("Get_GameObject_Object: Out Index of List Object!");
            return null;
        }
        return l_Object[i_ObjectListIndex];
    }

    /// <summary>
    /// Get Isometric Square Code from List Object
    /// </summary>
    /// <param name="i_ObjectListIndex"></param>
    /// <returns></returns>
    public char Get_SingleCode_Object(int i_ObjectListIndex)
    {
        if (i_ObjectListIndex < 0 || i_ObjectListIndex >= Get_CountList_Object())
        {
            Debug.LogError("Get_SingleCode_Object: Out Index of List Object!");
            return Get_EmtyCode();
        }
        return l_Code_Object[i_ObjectListIndex];
    }

    #endregion

    #region Fence Up List Manager

    /// <summary>
    /// Get Count List of Fence Up
    /// </summary>
    /// <returns></returns>
    public int Get_CountList_Fence_Up()
    {
        if (l_Fence_Up.Count == l_Code_Fence_Up.Count)
        {
            return l_Code_Fence_Up.Count;
        }
        Debug.LogError("Get_CountList_Fence_Up: Both List not same Count!");
        return 0;
    }

    /// <summary>
    /// Get Isometric GameObject from List Fence Up
    /// </summary>
    /// <param name="i_FenceUpListIndex"></param>
    /// <returns></returns>
    public GameObject Get_GameObject_Fence_Up(int i_FenceUpListIndex)
    {
        if (i_FenceUpListIndex < 0 || i_FenceUpListIndex >= Get_CountList_Fence_Up())
        {
            Debug.LogError("Get_GameObject_Fence_Up: Out Index of List Fence_Up!");
            return null;
        }
        return l_Fence_Up[i_FenceUpListIndex];
    }

    /// <summary>
    /// Get Isometric Square Code from List Fence Up
    /// </summary>
    /// <param name="i_FenceUpListIndex"></param>
    /// <returns></returns>
    public char Get_SingleCode_Fence_Up(int i_FenceUpListIndex)
    {
        if (i_FenceUpListIndex < 0 || i_FenceUpListIndex >= Get_CountList_Fence_Up())
        {
            Debug.LogError("Get_SingleCode_Fence_Up: Out Index of List Fence_Up!");
            return Get_EmtyCode();
        }
        return l_Code_Fence_Up[i_FenceUpListIndex];
    }

    #endregion

    #region Fence_Down List Manager

    /// <summary>
    /// Get Count List of Fence_Down
    /// </summary>
    /// <returns></returns>
    public int Get_CountList_Fence_Down()
    {
        if (l_Fence_Down.Count == l_Code_Fence_Down.Count)
        {
            return l_Code_Fence_Down.Count;
        }
        Debug.LogError("Get_CountList_Fence_Down: Both List not same Count!");
        return 0;
    }

    /// <summary>
    /// Get Isometric GameObject from List Fence Down
    /// </summary>
    /// <param name="i_FenceDownListIndex"></param>
    /// <returns></returns>
    public GameObject Get_GameObject_Fence_Down(int i_FenceDownListIndex)
    {
        if (i_FenceDownListIndex < 0 || i_FenceDownListIndex >= Get_CountList_Fence_Down())
        {
            Debug.LogError("Get_GameObject_Fence_Down: Out Index of List Fence_Down!");
            return null;
        }
        return l_Fence_Down[i_FenceDownListIndex];
    }

    /// <summary>
    /// Get Isometric Square Code from List Fence Down
    /// </summary>
    /// <param name="i_FenceDownListIndex"></param>
    /// <returns></returns>
    public char Get_SingleCode_Fence_Down(int i_FenceDownListIndex)
    {
        if (i_FenceDownListIndex < 0 || i_FenceDownListIndex >= Get_CountList_Fence_Down())
        {
            Debug.LogError("Get_SingleCode_Fence_Down: Out Index of List Fence_Down!");
            return Get_EmtyCode();
        }
        return l_Code_Fence_Down[i_FenceDownListIndex];
    }

    #endregion

    #region Fence_Left List Manager

    /// <summary>
    /// Get Count List of Fence_Left
    /// </summary>
    /// <returns></returns>
    public int Get_CountList_Fence_Left()
    {
        if (l_Fence_Left.Count == l_Code_Fence_Left.Count)
        {
            return l_Code_Fence_Left.Count;
        }
        Debug.LogError("Get_CountList_Fence_Left: Both List not same Count!");
        return 0;
    }

    /// <summary>
    /// Get Isometric GameObject from List Fence Left
    /// </summary>
    /// <param name="i_FenceLeftListIndex"></param>
    /// <returns></returns>
    public GameObject Get_GameObject_Fence_Left(int i_FenceLeftListIndex)
    {
        if (i_FenceLeftListIndex < 0 || i_FenceLeftListIndex >= Get_CountList_Fence_Left())
        {
            Debug.LogError("Get_GameObject_Fence_Left: Out Index of List Fence_Left!");
            return null;
        }
        return l_Fence_Left[i_FenceLeftListIndex];
    }

    /// <summary>
    /// Get Isometric Square Code from List Fence Left
    /// </summary>
    /// <param name="i_FenceLeftListIndex"></param>
    /// <returns></returns>
    public char Get_SingleCode_Fence_Left(int i_FenceLeftListIndex)
    {
        if (i_FenceLeftListIndex < 0 || i_FenceLeftListIndex >= Get_CountList_Fence_Left())
        {
            Debug.LogError("Get_SingleCode_Fence_Left: Out Index of List Fence_Left!");
            return Get_EmtyCode();
        }
        return l_Code_Fence_Left[i_FenceLeftListIndex];
    }

    #endregion

    #region Fence_Right List Manager

    /// <summary>
    /// Get Count List of Fence_Right
    /// </summary>
    /// <returns></returns>
    public int Get_CountList_Fence_Right()
    {
        if (l_Fence_Right.Count == l_Code_Fence_Right.Count)
        {
            return l_Code_Fence_Right.Count;
        }
        Debug.LogError("Get_CountList_Fence_Right: Both List not same Count!");
        return 0;
    }

    /// <summary>
    /// Get Isometric GameObject from List Fence Right
    /// </summary>
    /// <param name="i_FenceRightListIndex"></param>
    /// <returns></returns>
    public GameObject Get_GameObject_Fence_Right(int i_FenceRightListIndex)
    {
        if (i_FenceRightListIndex < 0 || i_FenceRightListIndex >= Get_CountList_Fence_Right())
        {
            Debug.LogError("Get_GameObject_Fence_Right: Out Index of List Fence_Right!");
            return null;
        }
        return l_Fence_Right[i_FenceRightListIndex];
    }

    /// <summary>
    /// Get Isometric Square Code from List Fence Right
    /// </summary>
    /// <param name="i_FenceRightListIndex"></param>
    /// <returns></returns>
    public char Get_SingleCode_Fence_Right(int i_FenceRightListIndex)
    {
        if (i_FenceRightListIndex < 0 || i_FenceRightListIndex >= Get_CountList_Fence_Right())
        {
            Debug.LogError("Get_SingleCode_Fence_Right: Out Index of List Fence_Right!");
            return Get_EmtyCode();
        }
        return l_Code_Fence_Right[i_FenceRightListIndex];
    }

    #endregion

    #region Emty Manager

    /// <summary>
    /// Get EMTY CODE for GROUND, OBJECT and FENCE CODE
    /// </summary>
    /// <returns></returns>
    public char Get_EmtyCode()
    {
        return this.c_EmtyCode;
    }

    #endregion
}
