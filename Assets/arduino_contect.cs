using UnityEngine;
using System.IO.Ports;

public class arduino_contect : MonoBehaviour
{
    SerialPort sp = new SerialPort("COM3", 9600); // COM3要換成你實際連接的埠號
    [SerializeField] public int data_name;
    [SerializeField] private int lastDataName = -1;
    [SerializeField] private float lastChangeTime = 0f;
    [SerializeField] private float unchangedThreshold = 3f; // 設定為3秒
    [SerializeField] public bool isStable = false;
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
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
                if (int.TryParse(data, out data_name))
                {
                    Debug.Log("解析為數字：" + data_name);

                    // 如果數值有改變
                    if (data_name != lastDataName)
                    {
                        lastDataName = data_name;
                        lastChangeTime = Time.time;
                        isStable = false; // 重置穩定狀態
                    }

                    // 如果數值沒變，且已經超過閾值秒數
                    else if (!isStable && Time.time - lastChangeTime >= unchangedThreshold)
                    {
                        isStable = true; // 標記為穩定
                        Debug.Log("數值在 3 秒內沒有改變：" + data_name);

                        // 在這裡可以觸發你要的事件
                    }
                }
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

