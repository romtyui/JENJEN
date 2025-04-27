using UnityEngine;

public class obstacles_code : MonoBehaviour
{

    public float height;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.transform.position = new Vector3(2.1099999f, -4.92999983f, -0.550000012f); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "rock")
        {
            bool activeState = false;
            other.SendMessage("SetActiveState", activeState);
        }
    }
}
