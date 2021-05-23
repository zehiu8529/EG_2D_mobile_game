using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Hightlight Button when Pressed Key
/// </summary>
public class Button_Hightlight : MonoBehaviour
{
    /// <summary>
    /// Button Keycode
    /// </summary>
    [Header("Keyboard")]
    public KeyCode k_ButtonPress;

    /// <summary>
    /// If True, Button can be switch between "Primary" and "Active" state
    /// </summary>
    public bool b_ButtonActive = true;

    /// <summary>
    /// Lock Button
    /// </summary>
    public bool b_LockButton = false;

    /// <summary>
    /// Color Primary when in 'Normal State'
    /// </summary>
    [Header("Color")]
    public Color c_Color_ButtonPrimary = Color.white;

    /// <summary>
    /// Color when Button Up and in 'Active State'
    /// </summary>
    public Color c_Color_ButtonActive = Color.green;

    /// <summary>
    /// Color when Button Hold
    /// </summary>
    public Color c_Color_ButtonHold = Color.yellow;

    /// <summary>
    /// Color when in 'Lock state'
    /// </summary>
    public Color c_Color_ButtonLock = Color.red;

    /// <summary>
    /// Is Button in 'Active State'?
    /// </summary>
    private bool b_ActiveState = false;

    private SpriteRenderer s_SpriteRenderer;
    private Image i_Image;

    private void Start()
    {
        s_SpriteRenderer = GetComponent<SpriteRenderer>();
        i_Image = GetComponent<Image>();

        if (b_LockButton)
            s_SpriteRenderer.color = c_Color_ButtonLock;
    }

    private void Update()
    {
        if (s_SpriteRenderer == null && i_Image == null)
            return;

        Set_ColorByKeyboard();
    }

    private void Set_ColorByKeyboard()
    {
        if (b_LockButton)
        //Button Lock >> Button can't chance
        {
            Set_ColorToComponent(c_Color_ButtonLock);
            b_ActiveState = false;
        }
        else
        {
            if (Input.GetKey(k_ButtonPress))
                //Button Hold
                Set_ColorToComponent(c_Color_ButtonHold);
            else
            if (Input.GetKeyUp(k_ButtonPress))
            //Button Up
            {
                if (b_ButtonActive)
                {
                    b_ActiveState = !b_ActiveState;

                    if (b_ActiveState)
                        Set_ColorToComponent(c_Color_ButtonActive);
                    else
                        Set_ColorToComponent(c_Color_ButtonPrimary);
                }
            }
            //Chance State
            else
            {
                if (b_ActiveState)
                    Set_ColorToComponent(c_Color_ButtonActive);
                else
                    Set_ColorToComponent(c_Color_ButtonPrimary);
            }
            //Chance Color by State
        }
    }

    private void Set_ColorToComponent(Color c_ColorSet)
    {
        if (s_SpriteRenderer != null)
        {
            s_SpriteRenderer.color = c_ColorSet;
        }
        else
        if (i_Image != null)
        {
            i_Image.color = c_ColorSet;
        }
        else
        {
            Debug.LogError("Set_ColorToComponent: Not found 'SpriteRenderer' of 'Image' Component!");
        }
    }

    /// <summary>
    /// Set Lock Button
    /// </summary>
    /// <param name="b_LockButton"></param>
    public void Set_LockButton(bool b_LockButton)
    {
        this.b_LockButton = b_LockButton;
    }

    /// <summary>
    /// Get Lock Button
    /// </summary>
    /// <returns></returns>
    public bool Get_LockButton()
    {
        return b_LockButton;
    }

    /// <summary>
    /// Get Button Choice
    /// </summary>
    /// <returns></returns>
    public bool Get_ButtonChoice()
    {
        return b_ActiveState;
    }

    /// <summary>
    /// Set Button Choice Primary
    /// </summary>
    /// <param name="b_ActiveState"></param>
    public void Set_ButtonChoice(bool b_ActiveState)
    {
        this.b_ActiveState = b_ActiveState;
    }

    /// <summary>
    /// Get Button Allow Chance
    /// </summary>
    /// <returns></returns>
    public bool Get_ButtonActive()
    {
        return b_ButtonActive;
    }

    /// <summary>
    /// Set Button Allow Chance
    /// </summary>
    /// <param name="b_ButtonActive"></param>
    public void Set_ButtonActive(bool b_ButtonActive)
    {
        this.b_ButtonActive = b_ButtonActive;
    }
}
