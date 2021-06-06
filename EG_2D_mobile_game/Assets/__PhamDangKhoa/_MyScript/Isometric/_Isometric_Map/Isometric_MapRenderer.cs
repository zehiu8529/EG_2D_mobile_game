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
    /// List Isometric GameObject Object
    /// </summary>
    [Header("Object Square")]
    [SerializeField]
    private List<GameObject> l_Object;

    /// <summary>
    /// List Isometric GameObject Fence Up
    /// </summary>
    [Header("Fence Up Square")]
    [SerializeField]
    private List<GameObject> l_Fence_Up;

    /// <summary>
    /// List Isometric GameObject Fence Down
    /// </summary>
    [Header("Fence Down Square")]
    [SerializeField]
    private List<GameObject> l_Fence_Down;

    /// <summary>
    /// List Isometric GameObject Fence Left
    /// </summary>
    [Header("Fence Left Square")]
    [SerializeField]
    private List<GameObject> l_Fence_Left;

    /// <summary>
    /// List Isometric GameObject Fence Right
    /// </summary>
    [Header("Fence Right Square")]
    [SerializeField]
    private List<GameObject> l_Fence_Right;

    /// <summary>
    /// Emty Code use for GROUND, OBJECT and FENCE CODE
    /// </summary>
    /// <remarks>
    /// At Normally, Emty Code is a SPACE Chacracter or a ' ' Character
    /// </remarks>
    [Header("Emty Code")]
    //[SerializeField]
    private char c_EmtyCode = '~';

    #endregion

    private bool b_SameSingleCode_CheckDone = false;

    private void Update()
    {
        if (!b_SameSingleCode_CheckDone)
        {
            Set_Check_SingleCode_Same();
            b_SameSingleCode_CheckDone = true;
        }
    }

    #region Check Single Code

    /// <summary>
    /// Check same Single Code on List
    /// </summary>
    public void Set_Check_SingleCode_Same()
    {
        Set_Check_SingleCode_Ground();
        Set_Check_SingleCode_Object();
        Set_Check_SingleCode_Fence_Up();
        Set_Check_SingleCode_Fence_Down();
        Set_Check_SingleCode_Fence_Left();
        Set_Check_SingleCode_Fence_Right();
    }

    /// <summary>
    /// Check same Single Code on Ground List
    /// </summary>
    public void Set_Check_SingleCode_Ground()
    {
        for (int i = 0; i < l_Ground.Count-1; i++)
        {
            for(int j = i + 1; j < l_Ground.Count; j++)
            {
                if(Get_SingleCode_Ground(i) == Get_SingleCode_Ground(j))
                {
                    Debug.LogWarning("Set_Check_SingleCode_Ground: Same Single Code '" + Get_SingleCode_Ground(i) + " ' at index '" + j + "'");
                    break;
                }
            }
        }
    }

    /// <summary>
    /// Check same Single Code on Object List
    /// </summary>
    public void Set_Check_SingleCode_Object()
    {
        for (int i = 0; i < l_Object.Count - 1; i++)
        {
            for (int j = i + 1; j < l_Object.Count; j++)
            {
                if (Get_SingleCode_Object(i) == Get_SingleCode_Object(j))
                {
                    Debug.LogWarning("Set_Check_SingleCode_Object: Same Single Code '" + Get_SingleCode_Object(i) + " ' at index '" + j + "'");
                    break;
                }
            }
        }
    }

    /// <summary>
    /// Check same Single Code on Fence Up List
    /// </summary>
    public void Set_Check_SingleCode_Fence_Up()
    {
        for (int i = 0; i < l_Fence_Up.Count - 1; i++)
        {
            for (int j = i + 1; j < l_Fence_Up.Count; j++)
            {
                if (Get_SingleCode_Fence_Up(i) == Get_SingleCode_Fence_Up(j))
                {
                    Debug.LogWarning("Set_Check_SingleCode_Fence_Up: Same Single Code '" + Get_SingleCode_Fence_Up(i) + " ' at index '" + j + "'");
                    break;
                }
            }
        }
    }

    /// <summary>
    /// Check same Single Code on Fence Down List
    /// </summary>
    public void Set_Check_SingleCode_Fence_Down()
    {
        for (int i = 0; i < l_Fence_Down.Count - 1; i++)
        {
            for (int j = i + 1; j < l_Fence_Down.Count; j++)
            {
                if (Get_SingleCode_Fence_Down(i) == Get_SingleCode_Fence_Down(j))
                {
                    Debug.LogWarning("Set_Check_SingleCode_Fence_Down: Same Single Code '" + Get_SingleCode_Fence_Down(i) + " ' at index '" + j + "'");
                    break;
                }
            }
        }
    }

    /// <summary>
    /// Check same Single Code on Fence Left List
    /// </summary>
    public void Set_Check_SingleCode_Fence_Left()
    {
        for (int i = 0; i < l_Fence_Left.Count - 1; i++)
        {
            for (int j = i + 1; j < l_Fence_Left.Count; j++)
            {
                if (Get_SingleCode_Fence_Left(i) == Get_SingleCode_Fence_Left(j))
                {
                    Debug.LogWarning("Set_Check_SingleCode_Fence_Left: Same Single Code '" + Get_SingleCode_Fence_Left(i) + " ' at index '" + j + "'");
                    break;
                }
            }
        }
    }

    /// <summary>
    /// Check same Single Code on Fence Left List
    /// </summary>
    public void Set_Check_SingleCode_Fence_Right()
    {
        for (int i = 0; i < l_Fence_Right.Count - 1; i++)
        {
            for (int j = i + 1; j < l_Fence_Right.Count; j++)
            {
                if (Get_SingleCode_Fence_Right(i) == Get_SingleCode_Fence_Right(j))
                {
                    Debug.LogWarning("Set_Check_SingleCode_Fence_Right: Same Single Code '" + Get_SingleCode_Fence_Right(i) + " ' at index '" + j + "'");
                    break;
                }
            }
        }
    }

    #endregion

    #region Ground List Manager

    /// <summary>
    /// Get Count List of Ground
    /// </summary>
    /// <returns></returns>
    public int Get_CountList_Ground()
    {
        if (l_Ground.Count == l_Ground.Count)
        {
            return l_Ground.Count;
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
    /// Get Isometric GameObject from List Ground
    /// </summary>
    /// <param name="c_GroundCode"></param>
    /// <returns></returns>
    public GameObject Get_GameObject_Ground(char c_GroundCode)
    {
        if (c_GroundCode == Get_EmtyCode())
            return null;

        for (int i = 0; i < Get_CountList_Ground(); i++)
        {
            if (Get_SingleCode_Ground(i) == c_GroundCode)
            {
                return Get_GameObject_Ground(i);
            }
        }
        Debug.LogError("Get_GameObject_Ground: Not Found Code!");
        return null;
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
        return l_Ground[i_GroundListIndex].GetComponent<Isometric_Single>().Get_SingleCode();
    }

    #endregion

    #region Object List Manager

    /// <summary>
    /// Get Count List of Object
    /// </summary>
    /// <returns></returns>
    public int Get_CountList_Object()
    {
        if (l_Object.Count == l_Object.Count)
        {
            return l_Object.Count;
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
    /// Get Isometric GameObject from List Object
    /// </summary>
    /// <param name="c_ObjectCode"></param>
    /// <returns></returns>
    public GameObject Get_GameObject_Object(char c_ObjectCode)
    {
        if (c_ObjectCode == Get_EmtyCode())
            return null;

        for (int i = 0; i < Get_CountList_Object(); i++)
        {
            if (Get_SingleCode_Object(i) == c_ObjectCode)
            {
                return Get_GameObject_Object(i);
            }
        }
        Debug.LogError("Get_GameObject_Object: Not Found Code!");
        return null;
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
        return l_Object[i_ObjectListIndex].GetComponent<Isometric_Single>().Get_SingleCode();
    }

    #endregion

    #region Fence Up List Manager

    /// <summary>
    /// Get Count List of Fence Up
    /// </summary>
    /// <returns></returns>
    public int Get_CountList_Fence_Up()
    {
        if (l_Fence_Up.Count == l_Fence_Up.Count)
        {
            return l_Fence_Up.Count;
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
    /// Get Isometric GameObject from List Fence Up
    /// </summary>
    /// <param name="c_FenceUpCode"></param>
    /// <returns></returns>
    public GameObject Get_GameObject_Fence_Up(char c_FenceUpCode)
    {
        if (c_FenceUpCode == Get_EmtyCode())
            return null;

        for (int i = 0; i < Get_CountList_Fence_Up(); i++)
        {
            if (Get_SingleCode_Fence_Up(i) == c_FenceUpCode)
            {
                return Get_GameObject_Fence_Up(i);
            }
        }
        Debug.LogError("Get_GameObject_Fence_Up: Not Found Code!");
        return null;
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
        return l_Fence_Up[i_FenceUpListIndex].GetComponent<Isometric_Single>().Get_SingleCode();
    }

    #endregion

    #region Fence_Down List Manager

    /// <summary>
    /// Get Count List of Fence_Down
    /// </summary>
    /// <returns></returns>
    public int Get_CountList_Fence_Down()
    {
        if (l_Fence_Down.Count == l_Fence_Down.Count)
        {
            return l_Fence_Down.Count;
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
    /// Get Isometric GameObject from List Fence Down
    /// </summary>
    /// <param name="c_FenceUpCode"></param>
    /// <returns></returns>
    public GameObject Get_GameObject_Fence_Down(char c_FenceDownCode)
    {
        if (c_FenceDownCode == Get_EmtyCode())
            return null;

        for (int i = 0; i < Get_CountList_Fence_Down(); i++)
        {
            if (Get_SingleCode_Fence_Down(i) == c_FenceDownCode)
            {
                return Get_GameObject_Fence_Down(i);
            }
        }
        Debug.LogError("Get_GameObject_Fence_Down: Not Found Code!");
        return null;
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
        return l_Fence_Down[i_FenceDownListIndex].GetComponent<Isometric_Single>().Get_SingleCode();
    }

    #endregion

    #region Fence_Left List Manager

    /// <summary>
    /// Get Count List of Fence_Left
    /// </summary>
    /// <returns></returns>
    public int Get_CountList_Fence_Left()
    {
        if (l_Fence_Left.Count == l_Fence_Left.Count)
        {
            return l_Fence_Left.Count;
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
    /// Get Isometric GameObject from List Fence Left
    /// </summary>
    /// <param name="c_FenceUpCode"></param>
    /// <returns></returns>
    public GameObject Get_GameObject_Fence_Left(char c_FenceLeftCode)
    {
        if (c_FenceLeftCode == Get_EmtyCode())
            return null;

        for (int i = 0; i < Get_CountList_Fence_Left(); i++)
        {
            if (Get_SingleCode_Fence_Left(i) == c_FenceLeftCode)
            {
                return Get_GameObject_Fence_Left(i);
            }
        }
        Debug.LogError("Get_GameObject_Fence_Left: Not Found Code!");
        return null;
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
        return l_Fence_Left[i_FenceLeftListIndex].GetComponent<Isometric_Single>().Get_SingleCode();
    }

    #endregion

    #region Fence_Right List Manager

    /// <summary>
    /// Get Count List of Fence_Right
    /// </summary>
    /// <returns></returns>
    public int Get_CountList_Fence_Right()
    {
        if (l_Fence_Right.Count == l_Fence_Right.Count)
        {
            return l_Fence_Right.Count;
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
    /// Get Isometric GameObject from List Fence Right
    /// </summary>
    /// <param name="c_FenceUpCode"></param>
    /// <returns></returns>
    public GameObject Get_GameObject_Fence_Right(char c_FenceRightCode)
    {
        if (c_FenceRightCode == Get_EmtyCode())
            return null;

        for (int i = 0; i < Get_CountList_Fence_Right(); i++)
        {
            if (Get_SingleCode_Fence_Right(i) == c_FenceRightCode)
            {
                return Get_GameObject_Fence_Right(i);
            }
        }
        Debug.LogError("Get_GameObject_Fence_Right: Not Found Code!");
        return null;
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
        return l_Fence_Right[i_FenceRightListIndex].GetComponent<Isometric_Single>().Get_SingleCode();
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
