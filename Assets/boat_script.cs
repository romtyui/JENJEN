using UnityEngine;

public class boat_script : MonoBehaviour
{
    [SerializeField]private Transform orignal_point, endpoint;
    [SerializeField]public float total_time;
    [SerializeField] public float timer;
    [SerializeField] private float t;
    public bool stardo;
    public Object_create_code occ;
    public GameObject canva;
    public Transform savepoint;
    public bool end;

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
            stardo = false;
            if (other.GetComponent<countpoint_code>().Save_Point != null) 
            {
                savepoint = other.GetComponent<countpoint_code>().Save_Point;
                t = timer;
            }
        }
        if (other.gameObject.tag == "full")
        {
            Debug.Log(other.gameObject.name);
            timer = t;
            //this.transform.position = savepoint.position;
        }
        if (other.gameObject.tag == "event_end")
        {
            canva.SetActive(true);
            //canva.GetComponent<Animation>().Play("Transitions");
            end = true;
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
