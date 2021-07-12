using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showpan : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Panel;
    int counter;

    public void showpanel()
    {
        counter++;
        if(counter % 2==1)
        {
            Panel.gameObject.SetActive(false);
        }else
        {
            Panel.gameObject.SetActive(true);
        }
        
    }    
}
