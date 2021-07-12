using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scron : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 0.5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offset = new Vector2(Time.time * speed, 0);
    }
}
