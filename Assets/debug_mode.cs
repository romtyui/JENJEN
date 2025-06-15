using UnityEngine;
using UnityEngine.SceneManagement;

public class debug_mode : MonoBehaviour
{
    public boat_script boat;
    public new_camera_contriol new_Camera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        boat = FindAnyObjectByType<boat_script>();
        new_Camera = FindAnyObjectByType<new_camera_contriol>();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene("SampleScene");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene("twotwo");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SceneManager.LoadScene("three");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && boat != null)
        {
            boat.end = true;
            boat.enabled = false;
            new_Camera.endtrun = true;
        }
    }
}
