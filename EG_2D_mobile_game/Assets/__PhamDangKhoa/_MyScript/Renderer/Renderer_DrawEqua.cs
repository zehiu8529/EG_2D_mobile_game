using System.Collections.Generic;
using UnityEngine;

public class Renderer_DrawEqua : MonoBehaviour
//Script này dùng để nhận toạ độ các điểm để vẽ trên cung tròn, ứng dụng công thức lượng giác trong việc xác định điểm
{
    public bool b_Debug = true;

    public float f_Duration = 2;

    [Range(0,360)]
    public float f_DegStart = 0;

    [Range(3,36)]
    public int i_PointCount = 3;
    //Số lượng điểm quyết định hình dạng của hình vẽ (Tam giác đều, Tứ giác đều, Lục giác đều,...)
    //Khi số lượng điểm đạt tối đa, sẽ vẽ được Hình tròn (Không được vượt quá 36 điểm để tránh vị lỗi)

    private void OnDrawGizmosSelected()
    //Thể hiện hình vẽ mẫu trên chính GameObject. Và đây cũng là gợi ý cho cách sử dụng danh sách điểm.
    //Đường vẽ màu "Vàng" là giữa điểm Kết thúc và Bắt đầu
    {
        if (!b_Debug)
            return;

        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere((Vector2)transform.position, f_Duration);

        List<Vector2> l_PointDebug = Active_CreatePoint(this.f_Duration, this.f_DegStart, this.i_PointCount);
        //Sử dụng danh sách điểm từ hàm phía dưới

        for (int i = 1; i < l_PointDebug.Count; i++)
        {
            if (i % 2 == 0)
                Gizmos.color = Color.white;
            else
                Gizmos.color = Color.black;
            Gizmos.DrawLine(
                (Vector2)transform.position + l_PointDebug[i - 1],
                (Vector2)transform.position + l_PointDebug[i]);
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(
            (Vector2)transform.position + l_PointDebug[l_PointDebug.Count - 1], 
            (Vector2)transform.position + l_PointDebug[0]);
    }

    //-------------------------------------------------------------------------
    public List<Vector2> Active_CreatePoint(float f_Duration, float f_DegStart, int i_PointCount)
    //Hàm này khi được gọi sẽ trả về danh sách các điểm để vẽ trên cung tròn với tâm O có toạ độ gốc là O(0;0)
    //Khi vẽ các điểm này với tâm có góc toạ độ không là O(0;0), cần cộng cho chúng 1 Vector tâm A(x;y)
    {
        List<Vector2> l_Point = new List<Vector2>();

        float f_RadSpace = (360 / i_PointCount) * (Mathf.PI / 180);
        float f_RadStart = (f_DegStart) * (Mathf.PI / 180);
        float f_RadCur = f_RadStart;

        Vector2 v_StartPoint = new Vector2(Mathf.Cos(f_RadStart) * f_Duration, Mathf.Sin(f_RadStart) * f_Duration);
        Vector2 v_OldPoint = v_StartPoint;

        l_Point.Add(v_StartPoint);

        for (int i = 1; i < i_PointCount; i++)
        {
            f_RadCur += f_RadSpace;
            Vector2 v_NewPoint = new Vector2(Mathf.Cos(f_RadCur) * f_Duration, Mathf.Sin(f_RadCur) * f_Duration);

            l_Point.Add(v_NewPoint);

            v_OldPoint = v_NewPoint;
        }

        return l_Point;
    }

    public List<Vector2> Receive_PointList()
    //Hàm này trả nhanh về danh sách các điểm dựa theo thông số hiện có
    {
        return Active_CreatePoint(this.f_Duration,this.f_DegStart,this.i_PointCount);
    }
}