using System.Net;
using System.Threading;
using UnityEngine;

public class new_camera_contriol : MonoBehaviour
{
    [SerializeField] private Transform[] camera_transforms;
    public boat_script boat;
    [SerializeField] private float t;
    [Range(0f,5f)]public float[] camera_move_time;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.transform.position = camera_transforms[0].position;
        this.transform.rotation = camera_transforms[0].rotation;

    }

    // Update is called once per frame
    void Update()
    {
        if ((boat.timer / boat.total_time) > 0.35f) 
        {
            camera_move(camera_transforms[0], camera_transforms[1], camera_move_time[0]);
        }
    }
    void camera_move(Transform camera1, Transform camera2,float timer) 
    {
        t += Time.deltaTime;

        this.transform.position = Vector3.Lerp(camera1.position, camera2.position, t / timer);
        this.transform.rotation = Quaternion.Lerp(camera1.rotation, camera2.rotation, t / timer);


    }
}
