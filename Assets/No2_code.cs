using UnityEngine;
using static new_camera_contriol;

public class No2_code : MonoBehaviour
{
    public GameObject water;
    public Animation snake;
    public AnimationClip clip;
    public bool turn;
    public float timer,t;
    public new_camera_contriol NCC;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (turn)
        {
            t += Time.deltaTime;
            water.SetActive(true);
            if (t > timer) 
            {
                snake.clip = clip;
                snake.Play();
                turn = false;
            }
        }
    }
    public void openmouth() 
    {
        NCC.status = Now_Scenes.snake2;
    }
}
