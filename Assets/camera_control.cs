using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static new_camera_contriol;

public class camera_control : MonoBehaviour
{
    public Transform centerPoint; // 要繞的中心點
    [Range(-100,100)]
    public float radius = 5f;     // 半徑
    [Range(0, 5)]
    public float speed = 1f;      // 旋轉速度（圈速）
    [SerializeField]private float angle = 0f;
    [SerializeField] private Transform end_position;
    [SerializeField] private Transform camera_transforms2;
    [SerializeField] private Transform camera_3;
    [SerializeField] private Transform camera_4;

    [SerializeField] private float t;
    [Range(0f, 5f)] public float camera_move_time;


    public enum AngleOption
    {
        Deg0 = 0,
        Deg90 = 90,
        Deg180 = 180,
        Deg270 = 270,
        Deg360 = 360,
    }

    public AngleOption selectedAngle;
    public bool turn,event_playing, event_playing2;
    public player_control player;
    void Update()
    {
        if (!event_playing)
        {
            this.transform.LookAt(centerPoint);
            if (turn)
            {
                if (angle < (float)selectedAngle * Mathf.Deg2Rad)
                {
                    Debug.Log((float)selectedAngle * Mathf.Deg2Rad);
                    angle += speed * Time.deltaTime;
                    if (angle > (float)selectedAngle * Mathf.Deg2Rad)
                    {
                        turn = false;
                    }
                }
                else if (angle > (float)selectedAngle * Mathf.Deg2Rad)
                {
                    Debug.Log((float)selectedAngle * Mathf.Deg2Rad);

                    angle -= speed * Time.deltaTime;
                    if (angle < (float)selectedAngle * Mathf.Deg2Rad)
                    {
                        turn = false;
                    }
                }

            }

            //

            // 計算新的位置（繞 Y 軸平面旋轉）
            float x = centerPoint.position.x + Mathf.Cos(angle) * radius;
            float z = centerPoint.position.z + Mathf.Sin(angle) * radius;
            float y = this.transform.position.y; // 如果不繞 Y 軸方向可以固定

            transform.position = new Vector3(x, y, z);
        }
        else if(event_playing == true && event_playing2 == false)
        {
            this.gameObject.transform.position = end_position.position;
            this.gameObject.transform.rotation = end_position.rotation;
            this.transform.position = Vector3.Lerp(end_position.position, camera_transforms2.position, t / camera_move_time);
            this.transform.rotation = Quaternion.Lerp(end_position.rotation, camera_transforms2.rotation, t / camera_move_time);
            player.transform.position = Vector3.Lerp(camera_3.position, camera_4.position, t / camera_move_time);
            player.transform.rotation = Quaternion.Lerp(camera_3.rotation, camera_4.rotation, t / camera_move_time);
            t += Time.deltaTime;
            if (t / camera_move_time > 1)
            {
                event_playing2 = true;
                event_playing =false;
                t = 0f;

            }
        }
        if (event_playing2)
        {
            t += Time.deltaTime;

           

            
            if (t / camera_move_time > 1f)
            {
                t = 0f;
                SceneManager.LoadScene("twotwo");
                event_playing2 = false;
            }
        }

    }
}
