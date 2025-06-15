using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.SceneManagement;
using static camera_control;

public class end_script : MonoBehaviour
{
    public Transform end_pos, end2_pos, boat_pos;
    public bool turn, turn2;
    public float speed,t, camera_move_time;
    public camera_control camera;
    public GameObject obj;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (turn) 
        {
            transform.position = Vector3.MoveTowards(
                transform.position,       // �ثe��m
                end_pos.position,
                speed * Time.deltaTime     // �C�����ʦh��
                );
            if (Vector3.Distance(transform.position, end_pos.position) < 0.01f)
            {
                turn2 = true;
                turn = false;

                Debug.Log("抵達轉彎點！");
            }
        }
        if (turn2)
        {
            camera.status = cam1_Now_Scenes.end2;
            transform.position = boat_pos.position;
            t += Time.deltaTime;

            boat_pos.transform.position = Vector3.Lerp(boat_pos.position, end2_pos.position, t / camera_move_time);
            boat_pos.transform.rotation = Quaternion.Lerp(boat_pos.rotation, end2_pos.rotation, t / camera_move_time);
            if (t / camera_move_time > 0.2f)
            {
                    obj.SetActive(true);

            }
            if (t / camera_move_time > 0.5f)
            {
                SceneManager.LoadScene("twotwo");
                t = 0f;

            }
        }
    }
}
