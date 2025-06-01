using UnityEngine;
using static new_camera_contriol;

public class remains_animation_code : MonoBehaviour
{
    [SerializeField] private float t;
    [Range(0f,5f)][SerializeField] private float  animation_time;
    public Transform[] pos;
    public Transform player;
    public bool turn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (turn) 
        {
            camera_move(pos[0], pos[1], animation_time);
        }
    }
    void camera_move(Transform camera1, Transform camera2, float timer)
    {
        t += Time.deltaTime;

        player.transform.position = Vector3.Lerp(camera1.position, camera2.position, t / timer);
        player.transform.rotation = Quaternion.Lerp(camera1.rotation, camera2.rotation, t / timer);

        if (t / timer > 1)
        {
            turn = false;
        }

    }
}
