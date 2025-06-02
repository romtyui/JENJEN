using UnityEngine;
using UnityEngine.UIElements;

public class full_code : MonoBehaviour
{
    public bool turn;
    public GameObject full;
    public string data;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (turn) 
        {
            full.transform.localScale +=new Vector3(0,0-55f);
            turn = false;
        }
    }
}
