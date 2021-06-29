using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EG_UICharacter : MonoBehaviour
{
    #region Public Varible

    [Header("Game Manager")]
    [SerializeField]
    private EG_CharacterManager cl_CharacterManager;

    /// <summary>
    /// Color Choice
    /// </summary>
    [Header("Color Choice")]
    [SerializeField]
    private Color c_Choice = Color.yellow;

    /// <summary>
    /// Color Not Choice
    /// </summary>
    [SerializeField]
    private Color c_NotChoice = Color.white;

    /// <summary>
    /// List Of Button Character Choice (Same at List Character Prefab)
    /// </summary>
    [Header("Button Character Choice")]
    [SerializeField]
    private List<Image> lg_ButtonCharacter;

    #endregion

    private int i_OldCharacterChoice = -1;

    /// <summary>
    /// Set Color of Button Character Choice
    /// </summary>
    /// <param name="i_NewClientCharacterChoice"></param>
    public void Button_UICharacterChoice(int i_NewClientCharacterChoice)
    {
        if(i_OldCharacterChoice != -1)
        {
            lg_ButtonCharacter[i_OldCharacterChoice].color = c_NotChoice;
        }

        i_OldCharacterChoice = i_NewClientCharacterChoice;

        lg_ButtonCharacter[i_NewClientCharacterChoice].color = c_Choice;        
    }
}
