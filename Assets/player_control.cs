using UnityEngine;

public class player_control : MonoBehaviour
{
    [Header("觸發器")]
    public Object_create_code OCC;
    [Header("出生點")]
    public Transform oringal_position;
    [Header("重生點")]
    public Transform restart_position;
    [Header("轉彎判斷點")]
    public GameObject[] Turning_points;
    [Header("記數器")]
    public int next_Counter,last_Counter;
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
            transform.position,       // 目前位置
            Turning_points[((test == true) ? next_Counter : last_Counter)].transform.position,
            speed * Time.deltaTime     // 每秒移動多少
            );
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Turning_poimt") 
        {
            next_Counter = ((other.gameObject.GetComponent<countpoint_code>().count_num + 1 < Turning_points.Length-1) ? other.gameObject.GetComponent<countpoint_code>().count_num+1 : 0); 
            last_Counter = ((other.gameObject.GetComponent<countpoint_code>().count_num - 1 > -1 ) ? other.gameObject.GetComponent<countpoint_code>().count_num-1 : Turning_points.Length-1);
            turning_BT = false;
        }
        if (other.gameObject.tag == "Teleport_point")
        {
            this.transform.position = other.GetComponent<Take_the_stairs_checkpoint>().TP_position.transform.position;
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
}
