using UnityEngine;
using UnityEngine.UI;

public class KeyCode_UI : MonoBehaviour
//Add on GameObject Button UI
{
    [Header("Keyboard")]
    public KeyCode k_ButtonPress;
    public bool b_ButtonActive = true;
    //If True, Button can be switch between "Primary" and "Active" state
    public bool b_LockButton = false;
    //Lock Button

    [Header("Color")]
    public Color c_Color_ButtonPrimary = Color.white;
    //Color Primary
    public Color c_Color_ButtonActive = Color.green;
    //Color Button Up
    public Color c_Color_ButtonHold = Color.yellow;
    //Color Button Hold
    public Color c_Color_ButtonLock = Color.red;
    //Color Button Lock

    private bool b_ActiveState = false;
    //Button on Choice

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
    }

    public void Set_LockButton(bool b_LockButton)
    //Set Lock Button
    {
        this.b_LockButton = b_LockButton;
    }

    public bool Get_LockButton()
    //Get Lock Button
    {
        return b_LockButton;
    }

    public bool Get_ButtonChoice()
    //Get Button Choice
    {
        return b_ActiveState;
    }

    public void Set_ButtonChoice(bool b_ActiveState)
    //Set Button Choice Primary
    {
        this.b_ActiveState = b_ActiveState;
    }

    public bool Get_ButtonActive()
    //Get Button Allow Chance
    {
        return b_ButtonActive;
    }

    public void Set_ButtonActive(bool b_ButtonActive)
    //Set Button Allow Chance
    {
        this.b_ButtonActive = b_ButtonActive;
    }
}
