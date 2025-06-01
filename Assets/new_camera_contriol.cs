using System.Net;
using System.Threading;
using System.Timers;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class new_camera_contriol : MonoBehaviour
{
    [SerializeField] private Transform[] camera_transforms;
    public boat_script boat;
    [SerializeField] private float t;
    [Range(0f, 5f)] public float[] camera_move_time;
    private bool hasCrocodileTriggered = false;
    private bool hasSwitchToFallTriggered = false;
    [SerializeField] private bool hasremains_firstTriggered = false;
    public remains_animation_code rac;
    public player_control player_Control;

    public enum Now_Scenes { Crocodile, Crocodile_appear, SwitchToFall,first,remains_first, remains, none }
    public Now_Scenes status;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.transform.position = camera_transforms[0].position;
        this.transform.rotation = camera_transforms[0].rotation;
        status = Now_Scenes.first;
    }

    // Update is called once per frame
    void Update()
    {
        if ((boat.timer / boat.total_time) > 0.2f && !hasCrocodileTriggered)
        {
            status = Now_Scenes.Crocodile_appear;
            hasCrocodileTriggered =true;
        }
        if ((boat.timer / boat.total_time) > 0.5f && !hasSwitchToFallTriggered)
        {
            status = Now_Scenes.SwitchToFall;
            hasSwitchToFallTriggered = true;

        }
        if (boat.end == true && !hasremains_firstTriggered) 
        {
            status = Now_Scenes.remains_first;
            hasremains_firstTriggered = true;
        }
        Switch_state();
    }
    void camera_move(Transform camera1, Transform camera2, float timer)
    {
        t += Time.deltaTime;

        this.transform.position = Vector3.Lerp(camera1.position, camera2.position, t / timer);
        this.transform.rotation = Quaternion.Lerp(camera1.rotation, camera2.rotation, t / timer);
        boat.stardo = false;

        if (t / timer > 1)
        {
            boat.stardo = true;

            status = Now_Scenes.none;
            t= 0f;

        }

    }
    void Switch_state() 
    {
        switch (status) 
        {
            case Now_Scenes.Crocodile:
                camera_move(camera_transforms[3], camera_transforms[2], camera_move_time[1]);

                break;
            case Now_Scenes.Crocodile_appear:
                this.transform.position = camera_transforms[3].position;
                this.transform.rotation = camera_transforms[3].rotation;
                t += Time.deltaTime;

               
                boat.stardo = false;

                if (t >  camera_move_time[2] )
                {

                    status = Now_Scenes.Crocodile;
                    t = 0f;

                }
                break;
            case Now_Scenes.SwitchToFall:
                camera_move(camera_transforms[2], camera_transforms[1], camera_move_time[0]);

                break;
            case Now_Scenes.first:
                this.transform.position = camera_transforms[0].position;
                this.transform.rotation = camera_transforms[0].rotation;
                break;
            case Now_Scenes.remains_first:
                camera_move(camera_transforms[1], camera_transforms[4], camera_move_time[3]);
                boat.canva.GetComponent<Animation>().Play("Transitions");
                rac.turn = true;
                break;
            case Now_Scenes.remains:
                int next = player_Control.next_Counter + 3;
                int last = player_Control.last_Counter + 3;
                int count = player_Control.count + 3;

                if (player_Control.test)
                {
                    if (next < camera_transforms.Length && count != 3) 
                    {
                        if (count == 5)
                        {
                            camera_move(camera_transforms[count], camera_transforms[next + 1], camera_move_time[4]);
                        }
                        else if (count == 7) 
                        {
                            camera_move(camera_transforms[count], camera_transforms[7], camera_move_time[4]);
                        }
                        else if (count == 8)
                        {
                            camera_move(camera_transforms[count], camera_transforms[8], camera_move_time[4]);
                        }
                        else
                        {
                            camera_move(camera_transforms[count], camera_transforms[next], camera_move_time[4]);
                        }
                    }
                }
                else if(!player_Control.test)
                {
                    if (last > 3)
                    {
                        if (count == 4)
                        {
                            camera_move(camera_transforms[next+1], camera_transforms[count+1], camera_move_time[4]);
                        }
                        else if (count == 6)
                        {
                            camera_move(camera_transforms[count], camera_transforms[5], camera_move_time[4]);
                        }
                        else
                        {
                            camera_move(camera_transforms[next], camera_transforms[count], camera_move_time[4]);
                        }
                    }
                }
                //rac.turn = true;
                break;
            case Now_Scenes.none:
                break;
        }
    }
}
