using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static camera_control;
using static new_camera_contriol;

public class player_control : MonoBehaviour
{
    [Header("TCP設定")]
    public Object_create_code OCC;
    [Header("位置設置")]
    public Transform oringal_position;
    [Header("存檔點")]
    public Transform restart_position;
    [Header("轉角判斷點列表")]
    public GameObject[] Turning_points;
    [Header("轉彎參數")]
    public int next_Counter,last_Counter,count;
    public bool turning_BT;
    public bool rank1,rank2,rank3;

    [SerializeField] private float[] turning_number;
    [SerializeField]
    private float speed;

    [SerializeField]
    public bool test;

    public arduino_contect arduinor;
    public camera_control camera;
    public new_camera_contriol Newcamera;
    public Transform Endtransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.gameObject.transform.position = oringal_position.position;
        arduinor = FindAnyObjectByType<arduino_contect>();
        OCC = FindAnyObjectByType<Object_create_code>();

    }

    // Update is called once per frame
    void Update()
    {
        if (arduinor.isStable) 
        {
            if (turning_number[0] == arduinor.data_name)
            {
                test = true;
            }
            else if (turning_number[1] == arduinor.data_name) 
            {
                test = false;
            }
        }
        if (turning_BT)
        {
            Transform targetPoint = Turning_points[
                (test == true) ? next_Counter : last_Counter
            ].transform;
            transform.position = Vector3.MoveTowards(
                transform.position,       // �ثe��m
                Turning_points[((test == true) ? next_Counter : last_Counter)].transform.position,
                speed * Time.deltaTime     // �C�����ʦh��
                );
            if (Vector3.Distance(transform.position, targetPoint.position) < 0.01f)
            {
                turning_BT = false;
                Debug.Log("抵達轉彎點！");
            }

        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            SceneManager.LoadScene("twotwo");

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Turning_poimt") 
        {
            if (rank1)
            {

                if (other.gameObject.name != "Turning_C_end")
                {
                    //camera.event_playing = true;
                    //camera.selectedAngle = AngleOption.Deg90;
                    this.gameObject.transform.position = other.gameObject.transform.position;
                    count = other.gameObject.GetComponent<countpoint_code>().count_num;
                    next_Counter = ((other.gameObject.GetComponent<countpoint_code>().count_num + 1 < Turning_points.Length - 1) ? other.gameObject.GetComponent<countpoint_code>().count_num + 1 : 0);
                    last_Counter = ((other.gameObject.GetComponent<countpoint_code>().count_num - 1 > -1) ? other.gameObject.GetComponent<countpoint_code>().count_num - 1 : Turning_points.Length - 1);
                    turning_BT = false;
                    if (other.gameObject.name == "Turning_A")
                    {
                        camera.status = cam1_Now_Scenes.camera1;
                        //camera.selectedAngle = AngleOption.Deg0;
                    }
                    else if (other.gameObject.name == "Turning_B")
                    {
                        camera.status = cam1_Now_Scenes.camera2;
                        //camera.selectedAngle = AngleOption.Deg180;
                    }
                    //camera.selectedAngle = AngleOption.Deg180;

                    else if (other.gameObject.name == "Turning_D")
                    {
                        camera.status = cam1_Now_Scenes.camera3;
                        //camera.selectedAngle = AngleOption.Deg90;
                    }
                }
                else 
                {
                    camera.status = cam1_Now_Scenes.camera4;
                    this.GetComponent<end_script>().turn = true;
                    this.GetComponent<player_control>().enabled = false;
                }
            }
                
            else if (rank2)
            {
                if (other.gameObject.name == "remains_turning2" && Newcamera.endtrun == true)
                {
                    //camera.status = cam1_Now_Scenes.camera1;
                    //camera.selectedAngle = AngleOption.Deg0;
                }
                else 
                {
                    Debug.Log(other.gameObject.name);
                    //this.gameObject.transform.position = other.gameObject.transform.position;
                    count = other.gameObject.GetComponent<countpoint_code>().count_num;
                    next_Counter = other.gameObject.GetComponent<countpoint_code>().next;
                    last_Counter = other.gameObject.GetComponent<countpoint_code>().last;
                    turning_BT = false;
                    Newcamera.status = Now_Scenes.remains;
                }
                

            }
            else if (rank3)
            {

            }
            
        }
        if (other.gameObject.tag == "Teleport_point")
        {
            var tp = other.GetComponent<Take_the_stairs_checkpoint>();
            StartCoroutine(WaitAndDoSomething(tp.TP_position));
            this.transform.position = other.GetComponent<Take_the_stairs_checkpoint>().TP_position.transform.position;
            
            other.GetComponent<Take_the_stairs_checkpoint>().TP_position.SetActive(false);
            
        }
        if (other.gameObject.tag == "event_checkpoint")
        {
            OCC.trunBT = true;
            if (other.GetComponent<countpoint_code>() != null) 
            {
                restart_position = other.GetComponent<countpoint_code>().Save_Point;
            }
        }
        if (other.gameObject.tag == "rock")
        {
            this.transform.position = restart_position.position;
        }
        if (other.gameObject.tag == "event_end")
        {
            turning_BT = false ;
            this.gameObject.transform.position = Turning_points[count].transform.position;
        }
    }
    IEnumerator WaitAndDoSomething(GameObject other)
    {
        // 等待 2 秒
        yield return new WaitForSeconds(2f);

        // 2秒後執行這段
        other.SetActive(true);
    }
}
