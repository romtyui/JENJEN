using UnityEngine;

public class obstacles_code : MonoBehaviour
{
    public Object_create_code OCC;
    public float height;
    public MeshRenderer[] cubes;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OCC = FindAnyObjectByType< Object_create_code>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.gameObject.tag == "rock" && OCC.Color_Data_name == "blue")
        {
 
            bool activeState = false;
           
            var rockScript = other.transform.parent.GetComponent<rock_obstacle_code>();
            rockScript.GetComponent<rock_obstacle_code>().turn = true;
            this.gameObject.SetActive(false);
        }
        if (other.gameObject.tag == "brige")
        {

            bool activeState = false;
            var rockScript = other.transform.parent.GetComponent<rock_obstacle_code>();
            rockScript.GetComponent<rock_obstacle_code>().turn = true;
            this.gameObject.SetActive(false);
        }
    }
}
