using UnityEngine;

public class obstacles_code : MonoBehaviour
{
    public Transform player_transform;
    public float height;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player_transform = GameObject.FindWithTag("Player").transform;
        this.transform.position = player_transform.position+new Vector3(0, height, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
