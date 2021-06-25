using System;
using UnityEngine;

/// <summary>
/// Working on Keyboard Improve
/// </summary>
public class Class_KeyCode
{
    public Class_KeyCode()
    {

    }

    #region Key Pressed Down

    /// <summary>
    /// Get Key Down
    /// </summary>
    /// <returns></returns>
    public KeyCode Get_KeyCode_Pressed_Down()
    {
        foreach (KeyCode k_KeyCode in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(k_KeyCode))
                return k_KeyCode;
        }
        return KeyCode.None;
    }

    /// <summary>
    /// Get Key Up
    /// </summary>
    /// <returns></returns>
    public KeyCode Get_KeyCode_Pressed_Up()
    {
        foreach (KeyCode k_KeyCode in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyUp(k_KeyCode))
                return k_KeyCode;
        }
        return KeyCode.None;
    }

    /// <summary>
    /// Get Key Hold
    /// </summary>
    /// <returns></returns>
    public KeyCode Get_KeyCode_Pressed_Hold()
    {
        foreach (KeyCode k_KeyCode in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKey(k_KeyCode))
                return k_KeyCode;
        }
        return KeyCode.None;
    }

    #endregion

    #region Mouse

    /// <summary>
    /// Mouse Pos
    /// </summary>
    /// <returns></returns>
    public Vector3 Get_Mouse()
    {
        return Input.mousePosition;
    }

    #region Mouse Any

    /// <summary>
    /// Get Mouse Down
    /// </summary>
    /// <returns></returns>
    public bool Get_Mouse_Any_Down()
    {
        return
            Input.GetKeyDown(KeyCode.Mouse0) || //Left Mouse
            Input.GetKeyDown(KeyCode.Mouse1) || //Right Mouse
            Input.GetKeyDown(KeyCode.Mouse2) || //Midle Mouse
            Input.GetKeyDown(KeyCode.Mouse3) ||
            Input.GetKeyDown(KeyCode.Mouse4) ||
            Input.GetKeyDown(KeyCode.Mouse5) ||
            Input.GetKeyDown(KeyCode.Mouse6);
    }

    /// <summary>
    /// Get Mouse Up
    /// </summary>
    /// <returns></returns>
    public bool Get_Mouse_Any_Up()
    {
        return
            Input.GetKeyUp(KeyCode.Mouse0) || //Left Mouse
            Input.GetKeyUp(KeyCode.Mouse1) || //Right Mouse
            Input.GetKeyUp(KeyCode.Mouse2) || //Midle Mouse
            Input.GetKeyUp(KeyCode.Mouse3) ||
            Input.GetKeyUp(KeyCode.Mouse4) ||
            Input.GetKeyUp(KeyCode.Mouse5) ||
            Input.GetKeyUp(KeyCode.Mouse6);
    }

    /// <summary>
    /// Get Mouse Hold
    /// </summary>
    /// <returns></returns>
    public bool Get_Mouse_Any_Hold()
    {
        return
            Input.GetKey(KeyCode.Mouse0) || //Left Mouse
            Input.GetKey(KeyCode.Mouse1) || //Right Mouse
            Input.GetKey(KeyCode.Mouse2) || //Midle Mouse
            Input.GetKey(KeyCode.Mouse3) ||
            Input.GetKey(KeyCode.Mouse4) ||
            Input.GetKey(KeyCode.Mouse5) ||
            Input.GetKey(KeyCode.Mouse6);
    }

    #endregion

    #region Mouse Left

    /// <summary>
    /// Get Mouse Left Down
    /// </summary>
    /// <returns></returns>
    public bool Get_Mouse_Left_Down()
    {
        return Input.GetKeyDown(KeyCode.Mouse0);
    }

    /// <summary>
    /// Get Mouse Left Up
    /// </summary>
    /// <returns></returns>
    public bool Get_Mouse_Left_Up()
    {
        return Input.GetKeyUp(KeyCode.Mouse0);
    }

    /// <summary>
    /// Get Mouse Left Hold
    /// </summary>
    /// <returns></returns>
    public bool Get_Mouse_Left_Hold()
    {
        return Input.GetKey(KeyCode.Mouse0);
    }

    #endregion

    #region Mouse Right

    /// <summary>
    /// Get Mouse Right Down
    /// </summary>
    /// <returns></returns>
    public bool Get_Mouse_Right_Down()
    {
        return Input.GetKeyDown(KeyCode.Mouse1);
    }

    /// <summary>
    /// Get Mouse Right Up
    /// </summary>
    /// <returns></returns>
    public bool Get_Mouse_Right_Up()
    {
        return Input.GetKeyUp(KeyCode.Mouse1);
    }

    /// <summary>
    /// Get Mouse Right Hold
    /// </summary>
    /// <returns></returns>
    public bool Get_Mouse_Right_Hold()
    {
        return Input.GetKey(KeyCode.Mouse1);
    }

    #endregion

    #region Mouse Mid

    /// <summary>
    /// Get Mouse Mid Down
    /// </summary>
    /// <returns></returns>
    public bool Get_Mouse_Mid_Down()
    {
        return Input.GetKeyDown(KeyCode.Mouse2);
    }

    /// <summary>
    /// Get Mouse Mid Up
    /// </summary>
    /// <returns></returns>
    public bool Get_Mouse_Mid_Up()
    {
        return Input.GetKeyUp(KeyCode.Mouse2);
    }

    /// <summary>
    /// Get Mouse Mid Hold
    /// </summary>
    /// <returns></returns>
    public bool Get_Mouse_Mid_Hold()
    {
        return Input.GetKey(KeyCode.Mouse2);
    }

    #endregion

    #endregion

    #region Keyboard

    /// <summary>
    /// Get Keyboard Hold
    /// </summary>
    /// <param name="k_Keyboard"></param>
    /// <returns></returns>
    public bool Get_Keyboard_Hold(KeyCode k_Keyboard)
    {
        return Input.GetKey(k_Keyboard);
    }

    /// <summary>
    /// Get Keyboard Down
    /// </summary>
    /// <param name="k_Keyboard"></param>
    /// <returns></returns>
    public bool Get_Keyboard_Down(KeyCode k_Keyboard)
    {
        return Input.GetKeyDown(k_Keyboard);
    }

    /// <summary>
    /// Get Keyboard Up
    /// </summary>
    /// <param name="k_Keyboard"></param>
    /// <returns></returns>
    public bool Get_Keyboard_Up(KeyCode k_Keyboard)
    {
        return Input.GetKeyUp(k_Keyboard);
    }

    #endregion

    #region Key Simple

    public string Get_KeyCode_SimpleChar(KeyCode k_Key)
    {
        if (k_Key == KeyCode.LeftBracket)
            return "[";
        if (k_Key == KeyCode.RightBracket)
            return "]";
        if (k_Key == KeyCode.Escape)
            return "Esc";
        if (k_Key == KeyCode.Return)
            return "Enter";
        if (k_Key == KeyCode.Delete)
            return "Del";
        return k_Key.ToString();
    }

    #endregion
}