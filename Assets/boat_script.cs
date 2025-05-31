using UnityEngine;

public class boat_script : MonoBehaviour
{
    [SerializeField]private Transform orignal_point, endpoint;
    [SerializeField]public float total_time;
    [SerializeField] public float timer;
    public bool stardo;
    public Object_create_code occ;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.transform.position = orignal_point.position;
        occ = FindAnyObjectByType<Object_create_code>();

    }

    // Update is called once per frame
    void Update()
    {
        if (stardo) 
        {
            if (timer < total_time) 
            {
                timer += Time.deltaTime;
            }
            this.transform.position = Vector3.Lerp(orignal_point.position, endpoint.position, timer/total_time);

        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enermy") 
        {
            if (other.gameObject.name == "Crocodile")
            {
                timer = 0.2f*total_time;
            }
        }
        if (other.gameObject.tag == "event_checkpoint")
        {
            Debug.Log(other.gameObject.name);

        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "event_checkpoint")
        {
            if (occ.detectionTimer >= occ.confirmationTime)
            {
                stardo = true;
            }
        }
    }
}
