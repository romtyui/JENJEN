using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;
using static new_camera_contriol;

public class camera_control : MonoBehaviour
{
    [SerializeField] private Transform[] camera_transforms;
    [SerializeField] private float t;
    [Range(0f, 5f)] public float[] camera_move_time;
    public player_control player_Control;
    public int num,num2;


    public enum cam1_Now_Scenes { camera1, camera2, camera3, camera4, end1, end2 ,none}
    public cam1_Now_Scenes status,orgnal_status;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        status = cam1_Now_Scenes.camera1;
    }

    // Update is called once per frame
    void Update()
    {
       
        Switch_state();
    }
    void camera_move(Transform camera1, Transform camera2, float timer,int n)
    {
        t += Time.deltaTime;

        this.transform.position = Vector3.Lerp(camera1.position, camera2.position, t / timer);
        this.transform.rotation = Quaternion.Lerp(camera1.rotation, camera2.rotation, t / timer);

        if (t / timer > 1)
        {
            orgnal_status = status;
            num = n;
            status = cam1_Now_Scenes.none;
            t = 0f;

        }

    }
    void Switch_state()
    {
        switch (status)
        {
            case cam1_Now_Scenes.camera1:
                num2 = 0;
                camera_move(camera_transforms[num], camera_transforms[0], camera_move_time[0], num2);

                break;
            case cam1_Now_Scenes.camera2:
                num2 = 1;

                camera_move(camera_transforms[num], camera_transforms[1], camera_move_time[1], num2);

                break;
            case cam1_Now_Scenes.camera3:
                num2 = 2;

                camera_move(camera_transforms[num], camera_transforms[2], camera_move_time[2], num2);

                break;
            case cam1_Now_Scenes.camera4:
                //num2 = 3;

                t += Time.deltaTime;

                this.transform.position = Vector3.Lerp(camera_transforms[num].position, camera_transforms[3].position, t / camera_move_time[3]);
                this.transform.rotation = Quaternion.Lerp(camera_transforms[num].rotation, camera_transforms[3].rotation, t / camera_move_time[3]);

                if (t / camera_move_time[3] > 1)
                {
                    orgnal_status = status;
                    num = 3;
                    status = cam1_Now_Scenes.end1;
                    t = 0f;

                }

                break;
            case cam1_Now_Scenes.end1:
                num2 = 4;

                camera_move(camera_transforms[num], camera_transforms[4], camera_move_time[4], num2);

                break;
            case cam1_Now_Scenes.end2:
                num2 = 5;

                camera_move(camera_transforms[num], camera_transforms[5], camera_move_time[5], num2);

                break;
            case cam1_Now_Scenes.none:
                break;

        }
    }
    //public Transform centerPoint; // 要繞的中心點
    //[Range(-100,100)]
    //public float radius = 5f;     // 半徑
    //[Range(0, 5)]
    //public float speed = 1f;      // 旋轉速度（圈速）
    //[SerializeField]private float angle = 0f;
    //[SerializeField] private Transform end_position;
    //[SerializeField] private Transform camera_transforms2;
    //[SerializeField] private Transform camera_3;
    //[SerializeField] private Transform camera_4;

    //[SerializeField] private float t;
    //[Range(0f, 5f)] public float camera_move_time;


    //public enum AngleOption
    //{
    //    Deg0 = 0,
    //    Deg90 = 90,
    //    Deg180 = 180,
    //    Deg270 = 270,
    //    Deg360 = 360,
    //}

    //public AngleOption selectedAngle;
    //public bool turn,event_playing, event_playing2;
    //public player_control player;
    //void Update()
    //{
    //    if (!event_playing)
    //    {
    //        this.transform.LookAt(centerPoint);
    //        if (turn)
    //        {
    //            if (angle < (float)selectedAngle * Mathf.Deg2Rad)
    //            {
    //                Debug.Log((float)selectedAngle * Mathf.Deg2Rad);
    //                angle += speed * Time.deltaTime;
    //                if (angle > (float)selectedAngle * Mathf.Deg2Rad)
    //                {
    //                    turn = false;
    //                }
    //            }
    //            else if (angle > (float)selectedAngle * Mathf.Deg2Rad)
    //            {
    //                Debug.Log((float)selectedAngle * Mathf.Deg2Rad);

    //                angle -= speed * Time.deltaTime;
    //                if (angle < (float)selectedAngle * Mathf.Deg2Rad)
    //                {
    //                    turn = false;
    //                }
    //            }

    //        }

    //        //

    //        // 計算新的位置（繞 Y 軸平面旋轉）
    //        float x = centerPoint.position.x + Mathf.Cos(angle) * radius;
    //        float z = centerPoint.position.z + Mathf.Sin(angle) * radius;
    //        float y = this.transform.position.y; // 如果不繞 Y 軸方向可以固定

    //        transform.position = new Vector3(x, y, z);
    //    }
    //    else if(event_playing == true && event_playing2 == false)
    //    {
    //        this.gameObject.transform.position = end_position.position;
    //        this.gameObject.transform.rotation = end_position.rotation;
    //        this.transform.position = Vector3.Lerp(end_position.position, camera_transforms2.position, t / camera_move_time);
    //        this.transform.rotation = Quaternion.Lerp(end_position.rotation, camera_transforms2.rotation, t / camera_move_time);
    //        player.transform.position = Vector3.Lerp(camera_3.position, camera_4.position, t / camera_move_time);
    //        player.transform.rotation = Quaternion.Lerp(camera_3.rotation, camera_4.rotation, t / camera_move_time);
    //        t += Time.deltaTime;
    //        if (t / camera_move_time > 1)
    //        {
    //            event_playing2 = true;
    //            event_playing =false;
    //            t = 0f;

    //        }
    //    }
    //    if (event_playing2)
    //    {
    //        t += Time.deltaTime;




    //        if (t / camera_move_time > 1f)
    //        {
    //            t = 0f;
    //            SceneManager.LoadScene("twotwo");
    //            event_playing2 = false;
    //        }
    //    }

    //}
}
