using Unity.Android.Gradle.Manifest;
using Unity.VisualScripting;
using UnityEngine;

public class obstacles_code : MonoBehaviour
{
    public Object_create_code OCC;
    public float height;

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
        if (other.gameObject.tag == "rock" )
        {
 
            bool activeState = false;
           
            var rockScript = other.transform.parent.GetComponent<rock_obstacle_code>();
            rockScript.GetComponent<rock_obstacle_code>().turn = true;
            this.gameObject.SetActive(false);
        }
        if (other.gameObject.tag == "brige")
        {
            //if (OCC.Color_Data_name == "red")
            //{
            //    other.gameObject.SetActive(false);
            //}
            other.gameObject.SetActive(false);
            this.gameObject.SetActive(false);


            //bool activeState = false;
            //var rockScript = other.transform.parent.GetComponent<rock_obstacle_code>();
            //rockScript.GetComponent<rock_obstacle_code>().turn = true;
            //this.gameObject.SetActive(false);
        }
        if (other.gameObject.tag == "water")
        {
            other.GetComponent<No2_code>().turn = true;
            Destroy(this.gameObject);
            //if (OCC.Color_Data_name == "red" || OCC.Color_Data_name == "green")
            //{
            //    other.GetComponent<No2_code>().turn = true;
            //}
            //bool activeState = false;
            //var rockScript = other.transform.parent.GetComponent<rock_obstacle_code>();
            //rockScript.GetComponent<rock_obstacle_code>().turn = true;
            //this.gameObject.SetActive(false);
        }
        if (other.gameObject.tag == "full")
        {
            other.GetComponent<full_code>().turn = true;
            other.GetComponent<full_code>().data = OCC.Color_Data_name;
            this.gameObject.SetActive(false);
            other.gameObject.SetActive(false);
            //if (OCC.Color_Data_name == "red" || OCC.Color_Data_name == "green")
            //{
            //    other.GetComponent<full_code>().turn = true;
            //    other.GetComponent<full_code>().data = OCC.Color_Data_name;
            //}

        }
        if (other.gameObject.tag == "enermy")
        {
            other.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
            //bool activeState = false;
            //var rockScript = other.transform.parent.GetComponent<rock_obstacle_code>();
            //rockScript.GetComponent<rock_obstacle_code>().turn = true;
            //this.gameObject.SetActive(false);
        }
    }
}
