using UnityEngine;

public class rock_obstacle_code : MonoBehaviour
{
    public GameObject[] rocks;
    public GameObject obstacle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SetActiveState(bool isActive)
    {
        obstacle.SetActive(true);
        for (int i = 0; i < rocks.Length; i++) 
        {
            rocks[i].SetActive(isActive);
        }
    }
}
