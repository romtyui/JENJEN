using UnityEditor.Rendering;
using UnityEngine;
using static new_camera_contriol;

public class crocodile_code : MonoBehaviour
{
    [SerializeField] private float t;
    [Range(0f, 5f)] public float[] camera_move_time;
    public BoxCollider collider;
    public Transform pos1,pos2;
    public Vector3 startPosition;          // �l�ܪ̪���l��m
    public bool turn, turn2;
    public Transform target;           // �n�l�� A ����
    [Range(0f, 5f)] public float initialSpeed = 1f;    // ��l�t��
    public float acceleration = 1f;    // �C��[�t�q
    public float catchDistance = 0.5f; // ��쪺�Z���P�w
    public float timeLimit = 5f;       // ����b�X�����

    private float currentSpeed;
    private float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = pos2.position; // �x�s��l��m
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

            // ���ʤ�V
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * currentSpeed * Time.deltaTime;

            // �P�w���
            float distance = Vector3.Distance(transform.position, target.position);
            if (distance < catchDistance || timer > timeLimit)
            {
                Debug.Log("���F�I");
                ResetChase();
                //collider.enabled = false; // ����l��
            }
         
        }
       
    }
    void ResetChase()
    {
        transform.position = startPosition;
        currentSpeed = initialSpeed;
        collider.enabled = true; // ����l��

        timer = 0f;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "boat") 
        {
            collider.enabled = false; // ����l��

            ResetChase();
        }
    }
}
