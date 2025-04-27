using UnityEngine;
using System.IO.Ports;

public class arduino_contect : MonoBehaviour
{
    SerialPort sp = new SerialPort("COM3", 9600); // COM3要換成你實際連接的埠號

    void Start()
    {
        sp.Open();
        sp.ReadTimeout = 50;
        Debug.Log("Serial Port Opened!");
    }

    void Update()
    {
        if (sp.IsOpen)
        {
            try
            {
                string data = sp.ReadLine();
                Debug.Log("從Arduino收到：" + data);

                if (data.Contains("MAGNET_DETECTED"))
                {
                    // 執行你要做的事，比如顯示提示
                    Debug.Log("偵測到磁鐵！");
                }
                else if (data.Contains("NO_MAGNET"))
                {
                    // 磁鐵移開，可以做別的反應
                    Debug.Log("沒有偵測到磁鐵。");
                }
            }
            catch (System.Exception) { }
        }
    }

    void OnApplicationQuit()
    {
        if (sp.IsOpen)
            sp.Close();
    }
}

