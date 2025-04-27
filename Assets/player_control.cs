using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_control : MonoBehaviour
{
    [Header("Ĳ�o��")]
    public Object_create_code OCC;
    [Header("�X���I")]
    public Transform oringal_position;
    [Header("�����I")]
    public Transform restart_position;
    [Header("���s�P�_�I")]
    public GameObject[] Turning_points;
    [Header("�O�ƾ�")]
    public int next_Counter,last_Counter,count;
    public bool turning_BT;
    [SerializeField]
    private float speed;

    [SerializeField]
    private bool test;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.gameObject.transform.position = oringal_position.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (turning_BT)
        {
            transform.position = Vector3.MoveTowards(
            transform.position,       // �ثe��m
            Turning_points[((test == true) ? next_Counter : last_Counter)].transform.position,
            speed * Time.deltaTime     // �C�����ʦh��
            );
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Turning_poimt") 
        {
            this.gameObject.transform.position = other.gameObject.transform.position;
            count = other.gameObject.GetComponent<countpoint_code>().count_num;
            next_Counter = ((other.gameObject.GetComponent<countpoint_code>().count_num + 1 < Turning_points.Length-1) ? other.gameObject.GetComponent<countpoint_code>().count_num+1 : 0); 
            last_Counter = ((other.gameObject.GetComponent<countpoint_code>().count_num - 1 > -1 ) ? other.gameObject.GetComponent<countpoint_code>().count_num-1 : Turning_points.Length-1);
            turning_BT = false;
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
        }
        if (other.gameObject.tag == "rock")
        {
            this.transform.position = restart_position.position;
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
