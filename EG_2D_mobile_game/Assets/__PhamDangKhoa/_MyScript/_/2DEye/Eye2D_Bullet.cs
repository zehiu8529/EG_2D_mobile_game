using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Sound_Component))]

public class Eye2D_Bullet : MonoBehaviour
{
    //Gán Script này vào GameObject có gán Collider bật IsTrigger
    //Script này hỗ trợ việc tự động nhắm bắn vào 1 muc tiêu cố định

    public GameObject g_Bullet;         
    //Kéo thả GameObject Frepab vào đây

    public int i_BulletDame = 2;        
    //Sát thương Bullet khi trúng mục tiêu
    public GameObject g_Target;         
    //Mục tiêu cần nhắm vào, nếu không có thì tự chọn mục tiêu đầu tiên

    public float f_BulletSpeed = 2;     
    //Tốc độ Bullet sau khi khai hòa

    public float f_Delay = 2;           
    //Thời gian trì hoãn giữa mỗi phát bắn
    private float f_Delay_Cur;

    public AudioClip a_Shoot;           
    //Âm thanh mỗi khi khai hỏa
    public float f_Volumn_Shoot = 0.2f; 
    //Âm lượng mỗi khi khai hỏa

    private void Awake()
    {
        f_Delay_Cur = f_Delay;
    }
    private void OnTriggerEnter2D(Collider2D c_Col)
    //Mục tiêu vào tầm khai hỏa
    {
        if (g_Target != null && c_Col.gameObject == g_Target) 
            //Nếu có mục tiêu và đúng mục tiêu
            Active_Shoot();
    }
    private void OnTriggerStay2D(Collider2D c_Col)
    //Mục tiêu vẫn còn nằm trong tầm khai hỏa
    {
        if (g_Target != null && c_Col.gameObject == g_Target) 
            //Nếu có mục tiêu và đúng mục tiêu
            Active_Shoot();
    }
    private void OnTriggerExit2D(Collider2D c_Col)
    //Mục tiêu rời khỏi tầm khai hỏa
    {
        if (c_Col.gameObject == g_Target)
            f_Delay_Cur = f_Delay;
    }
    private void Active_Shoot()
    //Khai hỏa!
    {
        if (f_Delay_Cur > 0)
            f_Delay_Cur -= Time.deltaTime; 
        //Trì hoãn khai hỏa
        else
        {
            f_Delay_Cur = f_Delay;
            Active_Bullet();
        }
    }
    private void Active_Bullet()
    //Kích hoạt sinh Bullet (Tạo ra nhiều Bullet)
    {
        if (a_Shoot != null) 
        {
            GetComponent<Sound_Component>().Set_PlaySound(a_Shoot, f_Volumn_Shoot, false); 
            //Kích hoạt âm thanh khai hỏa
        }
        
        Vector2 v_Direction;
        v_Direction = g_Target.transform.position - transform.position;
        //Nhắm hướng có mục tiêu
        v_Direction.Normalize();
        //Bình thường hóa hướng khai hỏa

        GameObject g_BulletClone;
        g_BulletClone = Instantiate(g_Bullet, transform.position, transform.rotation) as GameObject;
        //Tạo ra một Clone Bullet mới, thừa hưởng thuộc tính Component (Transform, Sprite,...)

        g_BulletClone.transform.position = new Vector3
            (transform.position.x, transform.position.y, transform.position.z - 1);
        //Đảm bảo Bullet bắn ra không bị GameObject chính che mất

        g_BulletClone.GetComponent<Rigidbody2D>().velocity = v_Direction * f_BulletSpeed;
        //Khai hỏa về hướng có Tarket
    }
}