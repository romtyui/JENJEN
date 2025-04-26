using UnityEngine;

public class player_control : MonoBehaviour
{
    [Header("出生點")]
    public Transform oringal_position;
    [Header("轉彎判斷點")]
    public GameObject[] Turning_points;
    [Header("記數器")]
    public int Counter;
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
            Turning_points[Counter + ((test == true) ? 1 : -1)].transform.position,           // 目標位置
            speed * Time.deltaTime     // 每秒移動多少
            );
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Turning_poimt") 
        {
            Counter = other.gameObject.GetComponent<countpoint_code>().count_num;
            turning_BT = false;
        }
    }
}
