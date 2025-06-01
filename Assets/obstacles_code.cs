using UnityEngine;

public class obstacles_code : MonoBehaviour
{

    public float height;
    public MeshRenderer[] cubes;
    //public Object_create_code Occ;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         FindAnyObjectByType<Object_create_code>().OC = this.GetComponent<obstacles_code>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.gameObject.tag == "rock")
        {
 
            bool activeState = false;
           
            var rockScript = other.transform.parent.GetComponent<rock_obstacle_code>();
            rockScript.GetComponent<rock_obstacle_code>().turn = true;
            this.gameObject.SetActive(false);
        }
    }
}
