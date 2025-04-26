using UnityEngine;

public class player_control : MonoBehaviour
{
    [Header("�X���I")]
    public Transform oringal_position;
    [Header("���s�P�_�I")]
    public GameObject[] Turning_points;
    [Header("�O�ƾ�")]
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
            transform.position,       // �ثe��m
            Turning_points[Counter + ((test == true) ? 1 : -1)].transform.position,           // �ؼЦ�m
            speed * Time.deltaTime     // �C���ʦh��
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
