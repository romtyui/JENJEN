﻿using UnityEngine;
using static camera_control;
using UnityEngine.SceneManagement;

public class rank2_end : MonoBehaviour
{
    public Transform start_pos,end_pos, end2_pos,end3_pos, camera_pos,camera_pos2;
    public bool turn, turn2;
    public float speed, t,t2, camera_move_time;
    public Camera camera;
    public GameObject obj,turningIMG;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (turn)
        {
            camera.transform.position = start_pos.position;
            camera.transform.rotation = start_pos.rotation;

            transform.position = Vector3.MoveTowards(
                transform.position,       // �ثe��m
                end_pos.position,
                speed * Time.deltaTime     // �C�����ʦh��
                );
            turningIMG.SetActive( true );
            if (Vector3.Distance(transform.position, end_pos.position) < 0.01f)
            {
                camera.transform.position = camera_pos.position;
                camera.transform.rotation = camera_pos.rotation;

                turn2 = true;
                turn = false;
                transform.position = end2_pos.position;

                Debug.Log("抵達轉彎點！");
            }
        }
        if (turn2)
        {
            //camera.status = cam1_Now_Scenes.end2;
            //transform.position = boat_pos.position;
            t += Time.deltaTime;

            transform.position = Vector3.Lerp(end2_pos.position, end3_pos.position, t / camera_move_time);
            transform.rotation = Quaternion.Lerp(end2_pos.rotation, end3_pos.rotation, t / camera_move_time);

            camera_move();
        }
    }
    void camera_move() 
    {
        t2 += Time.deltaTime;

        camera.transform.position = Vector3.Lerp(camera_pos.transform.position, camera_pos2.position, t / camera_move_time);
        camera.transform.rotation = Quaternion.Lerp(camera_pos.transform.rotation, camera_pos2.rotation, t / camera_move_time);

        if (t / camera_move_time > 1f)
        {
            SceneManager.LoadScene("three");
            t = 0f;

        }
    }
}
