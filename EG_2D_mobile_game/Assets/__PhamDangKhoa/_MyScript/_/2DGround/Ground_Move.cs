using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_Move : MonoBehaviour
//Script giúp một GameObject di chuyển theo 1 trình tự sẵn có
{

    public bool b_Trigger_Start = false;        
    //Chỉ di chuyển khi có một GameObject kích hoạt
    private bool b_Trigged_Start = false;       
    //Kích hoạt Ground di chuyển
    public GameObject g_Tarket;                 
    //Chỉ GameObject mục tiêu mới có thể kích hoạt
    //Nếu không gán mục tiêu, bất kì GameObject nào va chạm cũng có thể kích hoạt

    public GameObject[] g_Point;                
    //Danh sách các vị trí di chuyển

    public float f_SpeedMove = 2;               
    //Tốc độ di chuyển

    public float f_TimeDelay = 1;               
    //Thời gian tạm ngưng di chuyển giữa các Point, đặc biệt là Start và End
    private float f_TimeDelay_Cur;

    public bool b_Delay_Point = false;          
    //Ground tạm ngưng di chuyển khi đến mỗi vị trí
    public bool b_Trigger_Once = false;         
    //Ground chỉ di chuyển 1 lần duy nhất, tức không lặp lại hành trình
    public bool b_Reverse_End = true;           
    //Ground di chuyển ngược lại với hành trình cũ, tức đi từ End - Point - Start
    //Nếu là False, GameOject sẽ đi trực tiếp từ End - Start

    private int i_GoTo = 0;                     
    //Đánh dấu hành trình
    private int i_Reverse = 1;                  
    //Chiều đi của hành trình

    private void Awake()
    {
        f_TimeDelay_Cur = f_TimeDelay;
    }
    private void Update()
    {
        if (!b_Trigger_Start)
            //Nếu không bật b_Trigger_Start thì Ground sẽ tự động di chuyển mà không đợi GameObject kích hoạt
            b_Trigged_Start = true;
        if (b_Trigged_Start)
        //Nếu cho phép kích hoạt
        {
            if (f_TimeDelay_Cur > 0)
                //Nếu chưa hết thời gian trì hoãn
                f_TimeDelay_Cur -= Time.deltaTime;
            else
            {
                Active_Move();
            } 
            //if(f_TimeDelay_Cur <= 0) ...
        } 
        //if(b_Trigged) ...
    }
    private void OnCollisionEnter2D(Collision2D c_Col)
    {
        if (!b_Trigger_Start)
            //Nếu không kích hoạt "Xét va chạm"
            return;
        if (g_Tarket == null) 
            //Nếu không có mục tiêu
            b_Trigged_Start = true;
        else
        if (c_Col.gameObject == g_Tarket) 
            //Nếu đúng mục tiêu
            b_Trigged_Start = true;
    }
    private void Active_Move()
    {
        if (b_Trigger_Once && b_Trigged_Start && i_GoTo > g_Point.Length - 1)
            //Nếu đã đến nơi và chỉ cho phép di chuyển 1 lần
            return;
        Vector2 Point1 = new Vector2(this.transform.position.x, this.transform.position.y);
        Vector2 Point2 = new Vector2(g_Point[i_GoTo].transform.position.x, g_Point[i_GoTo].transform.position.y);
        transform.position = Vector2.MoveTowards(Point1, Point2, f_SpeedMove * Time.deltaTime); //Di chuyển
        if (Point1 == Point2)
        //Nếu đã đến điểm tiếp theo
        {
            i_GoTo += i_Reverse;
            if (b_Delay_Point) 
                //Nếu cho phép dừng lại mỗi điểm
                f_TimeDelay_Cur = f_TimeDelay;
            if (!b_Trigger_Once && i_GoTo > g_Point.Length - 1 || i_GoTo < 0)
            //Nếu không còn Point để xét và cho phép di chuyển nhiều lần
            {
                f_TimeDelay_Cur = f_TimeDelay; 
                //Dừng lại mỗi khi đến Point Start và End
                b_Trigged_Start = false;
                if (b_Reverse_End)
                //Nếu cho phép di chuyển ngược hành trình
                {
                    i_Reverse *= -1;
                    i_GoTo += i_Reverse;
                }
                else 
                //Di chuyển trực tiếp về điểm Start khi đã đến điểm End
                {
                    i_Reverse = 1;
                    i_GoTo = 0;
                } 
                //if(b_Reverse_Point) ...
            } 
            //if(i_GoTo > g_Point.Length - 1 || i_GoTo < 0) ...
        } 
        //if(this.transform.position == g_Point[i_GoTo].transform.position) ...
    }
}