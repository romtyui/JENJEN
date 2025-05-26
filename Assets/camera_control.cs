using Unity.VisualScripting;
using UnityEngine;

public class camera_control : MonoBehaviour
{
    public Transform centerPoint; // �n¶�������I
    [Range(-100,100)]
    public float radius = 5f;     // �b�|
    [Range(0, 5)]
    public float speed = 1f;      // ����t�ס]��t�^
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

            // �p��s����m�]¶ Y �b��������^
            float x = centerPoint.position.x + Mathf.Cos(angle) * radius;
            float z = centerPoint.position.z + Mathf.Sin(angle) * radius;
            float y = this.transform.position.y; // �p�G��¶ Y �b��V�i�H�T�w

            transform.position = new Vector3(x, y, z);
        }
        else 
        {
            this.gameObject.transform.position = end_position.position;
            this.gameObject.transform.rotation = end_position.rotation;

        }


    }
}
