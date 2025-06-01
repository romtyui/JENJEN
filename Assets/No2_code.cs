using UnityEngine;

public class No2_code : MonoBehaviour
{
    public GameObject water;
    public bool turn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (turn)
        {
            water.SetActive(true);
            turn = false;
        }
    }
}
