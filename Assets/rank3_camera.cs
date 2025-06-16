using UnityEngine;

public class rank3_camera : MonoBehaviour
{
    [SerializeField] private Transform[] camera_transforms;
    [SerializeField] private float t;
    [Range(0f, 5f)] public float[] camera_move_time;
    public player_control player_Control;

    public enum cam2_Now_Scenes { camera1, camera2, camera3, camera4,  none }
    public cam2_Now_Scenes status, orgnal_status;

    public int num;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        num = player_Control.count;
        Switch_state();
    }
    void camera_move(Transform camera1, Transform camera2, float timer)
    {
        t += Time.deltaTime;

        this.transform.position = Vector3.Lerp(camera1.position, camera2.position, t / timer);
        this.transform.rotation = Quaternion.Lerp(camera1.rotation, camera2.rotation, t / timer);

        if (t / timer > 1)
        {
            orgnal_status = status;
            status = cam2_Now_Scenes.none;
            t = 0f;

        }

    }
    void Switch_state()
    {
        switch (status)
        {
            case cam2_Now_Scenes.camera1:
                camera_move(camera_transforms[num], camera_transforms[0], camera_move_time[0]);

                break;
            case cam2_Now_Scenes.camera2:

                camera_move(camera_transforms[num], camera_transforms[1], camera_move_time[1]);

                break;
            case cam2_Now_Scenes.camera3:

                camera_move(camera_transforms[num], camera_transforms[2], camera_move_time[2]);

                break;
            case cam2_Now_Scenes.camera4:

                camera_move(camera_transforms[num], camera_transforms[3], camera_move_time[3]);

                break;
            //case cam1_Now_Scenes.camera4:
            //    //num2 = 3;

            //    t += Time.deltaTime;

            //    this.transform.position = Vector3.Lerp(camera_transforms[num].position, camera_transforms[3].position, t / camera_move_time[3]);
            //    this.transform.rotation = Quaternion.Lerp(camera_transforms[num].rotation, camera_transforms[3].rotation, t / camera_move_time[3]);

            //    if (t / camera_move_time[3] > 1)
            //    {
            //        orgnal_status = status;
            //        num = 3;
            //        status = cam1_Now_Scenes.end1;
            //        t = 0f;

            //    }

            //    break;
            //case cam1_Now_Scenes.end1:
            //    num2 = 4;

            //    camera_move(camera_transforms[num], camera_transforms[4], camera_move_time[4], num2);

            //    break;
            //case cam1_Now_Scenes.end2:
            //    num2 = 5;

            //    camera_move(camera_transforms[num], camera_transforms[5], camera_move_time[5], num2);

            //    break;
            case cam2_Now_Scenes.none:
                break;

        }
    }
}
