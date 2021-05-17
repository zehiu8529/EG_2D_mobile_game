using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(Renderer_DrawEqua))]

//Script dùng để điều khiển việc vẽ trên Renderer
public class Renderer_Component : MonoBehaviour
{
    private Renderer_DrawEqua cs_Point;
    private LineRenderer l_LineRenderer;

    public float f_LineWidth = 0.1f;

    private void Start()
    {
        cs_Point = GetComponent<Renderer_DrawEqua>();
        l_LineRenderer = GetComponent<LineRenderer>();
    }

    [System.Obsolete]
    private void Update()
    {
        Active_LineRenderer();
    }

    [System.Obsolete]
    private void Active_LineRenderer()
    {
        l_LineRenderer.SetWidth(f_LineWidth, f_LineWidth);

        int i_PointCount = cs_Point.Receive_PointList().Count;

        l_LineRenderer.positionCount = i_PointCount + 1;

        Vector2 v_Vector2;
        Vector3 v_Vector3;

        for(int i = 0; i < i_PointCount; i++)
        {
            v_Vector2 = cs_Point.Receive_PointList()[i];
            v_Vector3 = new Vector3(v_Vector2.x, v_Vector2.y, 0) + transform.position;
            l_LineRenderer.SetPosition(i, v_Vector3);
        }

        v_Vector2 = cs_Point.Receive_PointList()[0];
        v_Vector3 = new Vector3(v_Vector2.x, v_Vector2.y, 0) + transform.position;
        l_LineRenderer.SetPosition(i_PointCount, v_Vector3);
    }
}
