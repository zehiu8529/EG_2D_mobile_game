using UnityEngine;

[RequireComponent(typeof(PlatformEffector2D))]
[RequireComponent(typeof(BoxCollider2D))]

public class Ground_Through : MonoBehaviour
{
    public float f_ThroughTime = 0.5f;
    private float f_ThroughTime_Cur = 0;
    private void Awake()
    {
        GetComponent<BoxCollider2D>().usedByComposite = true;
        GetComponent<PlatformEffector2D>().useColliderMask = false;
        GetComponent<PlatformEffector2D>().useOneWay = true;
        GetComponent<PlatformEffector2D>().rotationalOffset = 0;
        GetComponent<PlatformEffector2D>().surfaceArc = 180;
    }
    private void Update()
    {
        if (f_ThroughTime_Cur > 0)
        {
            f_ThroughTime_Cur -= Time.deltaTime;
            //GetComponent<PlatformEffector2D>().rotationalOffset = 180;
            GetComponent<Collider2D>().isTrigger = true;
        }
        else
        {
            f_ThroughTime_Cur = 0;
            //GetComponent<PlatformEffector2D>().rotationalOffset = 0;
            GetComponent<Collider2D>().isTrigger = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D c_Col)
    {
        if (Input.GetKey(KeyCode.DownArrow))
            f_ThroughTime_Cur = f_ThroughTime;
    }
    private void OnCollisionStay2D(Collision2D c_Col)
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
            f_ThroughTime_Cur = f_ThroughTime;
    }
}