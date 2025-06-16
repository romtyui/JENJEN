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

    [Header("第三關參數")]
    public float baseY = 0f;            // 原始高度
    public float risePerBlock = 0.2f;   // 每塊積木浮力高度
    public float smoothSpeed = 2f;      // 浮力上升平滑速度
    public float maxY = 3.0f;           // 達到此高度後觸發移動
    public Transform moveTarget;        // 移動目標位置
    public float moveSpeed = 2.0f;      // 前進速度
    public int requiredBlocks;
    public bool smoothBT;

    [SerializeField] private bool startMoving = false;   // 是否開始前進

    [SerializeField] private float[] turning_number;
    [SerializeField]
    private float speed;

    [SerializeField]
    public bool test, debug;

    public arduino_contect arduinor;
    public camera_control camera;
    public new_camera_contriol Newcamera;
    public rank3_camera rank3_C;
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
            if (rank3)
            {
                if (!startMoving)
                {
                    // 浮起階段
                    baseY = transform.position.y;
                    int blocks = OCC.receivedTotalBlocks;
                    float targetY = baseY + blocks * risePerBlock;
                    maxY = baseY+ requiredBlocks * 10;

                    // 限制不要超過 maxY
                    //targetY = Mathf.Min(targetY, maxY);
                    Debug.Log("targetY = " + targetY);
                    Vector3 targetPos = new Vector3(transform.position.x, targetY, transform.position.z);
                    transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * smoothSpeed);

                    // 判斷是否達成移動條件
                    if (transform.position.y >=  - 0.05f)  // 加點容錯
                    {
                        transform.position = targetPos;
                        startMoving = true;
                        if (count == 0) 
                        {
                            if (test)
                            {
                                rank3_C.status = rank3_camera.cam2_Now_Scenes.camera2;
                            }

                        }
                        else if(count == 1)
                        {
                            if (test)
                            {
                                rank3_C.status = rank3_camera.cam2_Now_Scenes.camera3;
                            }
                            else
                            {
                                rank3_C.status = rank3_camera.cam2_Now_Scenes.camera1;
                            }
                        }
                        else if (count == 2)
                        {
                            if (test)
                            {
                                rank3_C.status = rank3_camera.cam2_Now_Scenes.camera4;
                            }
                            else
                            {
                                rank3_C.status = rank3_camera.cam2_Now_Scenes.camera2;
                            }
                        }
                        else if (count == 2)
                        {
                            if (test)
                            {
                                rank3_C.status = rank3_camera.cam2_Now_Scenes.camera4;
                            }
                            
                        }
                    }
                }
                else
                {
                    // 往目標點移動
                    if (moveTarget != null)
                    {
                        transform.position = Vector3.MoveTowards(this.transform.position, moveTarget.position, Time.deltaTime * moveSpeed);

                        if (Vector3.Distance(transform.position, moveTarget.position) < 0.1f)
                        {
                            if (OCC.receivedTotalBlocks != requiredBlocks && debug == false)
                            {
                                this.transform.position = restart_position.position;
                                startMoving = false;

                                turning_BT = false;

                            }
                            else 
                            {
                                smoothBT = true;
                                startMoving = false;
                                turning_BT = false;

                            }

                        }
                    }
                }
            }
            else
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

        }
        
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    SceneManager.LoadScene("twotwo");

        //}
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
                    this.GetComponent<rank2_end>().turn = true;
                    Newcamera.enabled = false;
                    this.GetComponent<player_control>().enabled = false;
                }
                else 
                {
                    Debug.Log(other.gameObject.name);
                    restart_position = other.GetComponent<countpoint_code>().Save_Point;

                    //this.gameObject.transform.position = other.gameObject.transform.position;
                    count = other.gameObject.GetComponent<countpoint_code>().count_num;
                    next_Counter = other.gameObject.GetComponent<countpoint_code>().next;
                    last_Counter = other.gameObject.GetComponent<countpoint_code>().last;
                    turning_BT = false;
                    Newcamera.status = Now_Scenes.remains;
                }
                

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
            if (other.GetComponent<countpoint_code>().Save_Point != null) 
            {
                restart_position = other.GetComponent<countpoint_code>().Save_Point;
            }
        }
        if (other.gameObject.tag == "rock")
        {
            this.transform.position = restart_position.position;
        }
        if (other.gameObject.tag == "spring")
        {
            this.transform.position = restart_position.position;
        }
        if (other.gameObject.tag == "event_end")
        {
            turning_BT = false ;
            this.gameObject.transform.position = Turning_points[count].transform.position;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Turning_poimt")
        {
            if (rank3)
            {
                if (smoothBT) 
                {
                    requiredBlocks = other.gameObject.GetComponent<countpoint_code>().num;
                    count = other.gameObject.GetComponent<countpoint_code>().count_num;

                    smoothBT = false ;
                }
                rank3_C.num = other.gameObject.GetComponent<countpoint_code>().count_num;

                if (other.GetComponent<countpoint_code>().Save_Point != null)
                {
                    restart_position = other.GetComponent<countpoint_code>().Save_Point;
                }
                if (test)
                {
                    if (other.gameObject.GetComponent<countpoint_code>().next_transform != null)
                    {
                        rank3_C.num = other.gameObject.GetComponent<countpoint_code>().num;
                        if (OCC.receivedTotalBlocks > requiredBlocks && debug == false)
                        {
                            moveTarget = other.gameObject.GetComponent<countpoint_code>().next_transformses[0];
                        }
                        else if (OCC.receivedTotalBlocks < requiredBlocks && debug == false)
                        {
                            moveTarget = other.gameObject.GetComponent<countpoint_code>().next_transformses[1];
                        }
                        else if (OCC.receivedTotalBlocks == requiredBlocks || debug == true)
                        {
                            moveTarget = other.gameObject.GetComponent<countpoint_code>().next_transform;
                        }
                    }
                }
                else
                {
                    if (other.gameObject.GetComponent<countpoint_code>().last_transform != null)
                    {
                        if (OCC.receivedTotalBlocks > requiredBlocks && debug == false)
                        {
                            moveTarget = other.gameObject.GetComponent<countpoint_code>().last_transformses[0];
                        }
                        else if (OCC.receivedTotalBlocks < requiredBlocks && debug == false)
                        {
                            moveTarget = other.gameObject.GetComponent<countpoint_code>().last_transformses[1];
                        }
                        else if (OCC.receivedTotalBlocks == requiredBlocks || debug == true)
                        {
                            moveTarget = other.gameObject.GetComponent<countpoint_code>().last_transform;
                        }
                    }


                }
            }
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
