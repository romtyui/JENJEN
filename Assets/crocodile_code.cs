using UnityEditor.Rendering;
using UnityEngine;
using static new_camera_contriol;

public class crocodile_code : MonoBehaviour
{
    [SerializeField] private float t;
    [Range(0f, 5f)] public float[] camera_move_time;
    public BoxCollider collider;
    public Transform pos1,pos2;
    public Vector3 startPosition;          // 追蹤者的初始位置
    public bool turn, turn2;
    public Transform target;           // 要追的 A 物件
    [Range(0f, 5f)] public float initialSpeed = 1f;    // 初始速度
    public float acceleration = 1f;    // 每秒加速量
    public float catchDistance = 0.5f; // 抓到的距離判定
    public float timeLimit = 5f;       // 限制在幾秒內抓到

    private float currentSpeed;
    private float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = pos2.position; // 儲存初始位置
    }

    // Update is called once per frame
    void Update()
    {
        if (turn) 
        {
            t += Time.deltaTime;

            this.transform.position = Vector3.Lerp(pos1.position, pos2.position, t / camera_move_time[0]);
            this.transform.rotation = Quaternion.Lerp(pos1.rotation, pos2.rotation, t / camera_move_time[0]);

            if (t / camera_move_time[0] > 1)
            {
                turn = false;
                turn2=true;
                t = 0f;

            }
        }
        if (turn2)
        {
            if (target == null) return;

            timer += Time.deltaTime;
            currentSpeed += acceleration * Time.deltaTime;

            // 移動方向
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * currentSpeed * Time.deltaTime;

            // 判定抓到
            float distance = Vector3.Distance(transform.position, target.position);
            if (distance < catchDistance || timer > timeLimit)
            {
                Debug.Log("抓到了！");
                ResetChase();
                //collider.enabled = false; // 停止追蹤
            }
         
        }
       
    }
    void ResetChase()
    {
        transform.position = startPosition;
        currentSpeed = initialSpeed;
        collider.enabled = true; // 停止追蹤

        timer = 0f;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "boat") 
        {
            collider.enabled = false; // 停止追蹤

            ResetChase();
        }
    }
}
