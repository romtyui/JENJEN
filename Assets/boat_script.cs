using UnityEngine;

public class boat_script : MonoBehaviour
{
    [SerializeField]private Transform orignal_point, endpoint;
    [SerializeField]public float total_time;
    [SerializeField] public float timer;
    public bool stardo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.transform.position = orignal_point.position;
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
        if (other.gameObject.tag == "enemy") 
        {
            timer= 0;
        }

    }
}
