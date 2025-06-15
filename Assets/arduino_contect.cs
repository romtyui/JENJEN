using UnityEngine;
using System.IO.Ports;

public class arduino_contect : MonoBehaviour
{
    private SerialPort sp;

    [SerializeField] public int data_name = -1;
    [SerializeField] private int lastDataName = -1;
    [SerializeField] private float lastChangeTime = 0f;
    [SerializeField] private float unchangedThreshold = 3f; // 數值未改變時間門檻（秒）
    [SerializeField] public bool isStable = false;
    public player_control player_Control;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        player_Control = GameObject.FindWithTag("Player").GetComponent<player_control>();

        sp = new SerialPort("COM6", 9600); // 請改成實際 Arduino 所連接的 COM 埠號
        try
        {
            sp.Open();
            sp.ReadTimeout = 50;
            Debug.Log("Serial Port Opened!");
        }
        catch (System.Exception e)
        {
            Debug.LogError("串口無法打開：" + e.Message);
        }
    }

    void Update()
    {
        player_Control = GameObject.FindWithTag("Player").GetComponent<player_control>();

        if (sp != null && sp.IsOpen)
        {
            try
            {
                string data = sp.ReadLine().Trim();
                Debug.Log("從Arduino收到：" + data);

                // 嘗試將收到的字串轉為數字
                if (int.TryParse(data, out int parsedValue))
                {
                    // 只接受 1~4 的有效數字
                    if (parsedValue >= 1 && parsedValue <= 4)
                    {
                        data_name = parsedValue;
                        Debug.Log("解析為有效數字：" + data_name);

                        if (data_name != lastDataName)
                        {
                            lastDataName = data_name;
                            lastChangeTime = Time.time;
                            isStable = false; // 數值變化，重設穩定性
                        }
                        else if (!isStable && Time.time - lastChangeTime >= unchangedThreshold)
                        {
                            isStable = true;
                            player_Control.turning_BT = true;
                            Debug.Log("數值在 " + unchangedThreshold + " 秒內未改變：" + data_name);
                            // 可在此觸發穩定狀態下的事件
                        }
                    }
                    else
                    {
                        Debug.LogWarning("收到不在 1~4 範圍內的數字：" + parsedValue);
                    }
                }
                else
                {
                    Debug.LogWarning("收到非數字格式資料：" + data);
                }
            }
            catch (System.TimeoutException) { }
            catch (System.Exception ex)
            {
                Debug.LogWarning("讀取資料錯誤：" + ex.Message);
            }
        }
    }

    void OnApplicationQuit()
    {
        if (sp != null && sp.IsOpen)
        {
            sp.Close();
            Debug.Log("Serial Port Closed.");
        }
    }
}

