using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EG_CharacterManager : MonoBehaviour
{
    /// <summary>
    /// List Prefab of Client Character Choice
    /// </summary>
    [Header("Prefab of Character")]
    [SerializeField]
    private List<GameObject> lg_Client;

    /// <summary>
    /// List Prefab of Remote Character Choice
    /// </summary>
    [SerializeField]
    private List<GameObject> lg_Remote;

    /// <summary>
    /// Client Character Choice
    /// </summary>
    private int i_ClientCharacterChoice = 0;

    /// <summary>
    /// Button Choice Client Character
    /// </summary>
    /// <param name="i_ClientCharacterChoice"></param>
    public void Button_CharacterChoice(int i_ClientCharacterChoice)
    {
        this.i_ClientCharacterChoice = i_ClientCharacterChoice;
    }

    /// <summary>
    /// Get Index of Client Character Choice
    /// </summary>
    /// <returns></returns>
    public int Get_ClientCharacterChoice()
    {
        return this.i_ClientCharacterChoice;
    }

    /// <summary>
    /// Get Prefab of Client Character Choice
    /// </summary>
    /// <param name="i_CharacterChoice"></param>
    /// <returns></returns>
    public GameObject Get_Prefab_Client(int i_CharacterChoice)
    {
        return lg_Client[i_CharacterChoice];
    }

    /// <summary>
    /// Get Prefab of Remote Character Choice
    /// </summary>
    /// <param name="i_CharacterChoice"></param>
    /// <returns></returns>
    public GameObject Get_Prefab_Remote(int i_CharacterChoice)
    {
        return lg_Remote[i_CharacterChoice];
    }
}
