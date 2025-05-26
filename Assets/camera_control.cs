using Unity.VisualScripting;
using UnityEngine;

public class camera_control : MonoBehaviour
{
    public Transform centerPoint; // 要繞的中心點
    [Range(-100,100)]
    public float radius = 5f;     // 半徑
    [Range(0, 5)]
    public float speed = 1f;      // 旋轉速度（圈速）
    [SerializeField]private float angle = 0f;
    [SerializeField] private Transform end_position;
    public enum AngleOption
    {
        Deg0 = 0,
        Deg90 = 90,
        Deg180 = 180,
        Deg270 = 270,
        Deg360 = 360,
    }

    public AngleOption selectedAngle;
    public bool turn,event_playing;
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
        else 
        {
            this.gameObject.transform.position = end_position.position;
            this.gameObject.transform.rotation = end_position.rotation;

        }


    }
}
