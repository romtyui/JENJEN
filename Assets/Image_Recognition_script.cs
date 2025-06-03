using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public partial class Image_Recognition_script : MonoBehaviour
{
    private TcpClient client;
    private NetworkStream stream;
    private Thread receiveThread;
    private bool isRunning = true; // 控制執行緒的變數

    [Header("TCP連接網址")]
    [SerializeField]
    private string TCPIP_name;

    [Header("傳送參數管理")]
    public string TCP_Data;
    //public delivery

    void Start()
    {
        DontDestroyOnLoad(this.gameObject); // 切換場景時保留這個物件
        ConnectToServer();
    }

    void ConnectToServer()
    {
        try
        {
            client = new TcpClient(TCPIP_name, 65432); // 連接 Python 伺服器"192.168.104.44"
            stream = client.GetStream();
            receiveThread = new Thread(new ThreadStart(ReceiveData)); // 建立新的執行緒
            receiveThread.IsBackground = true; // 設定為背景執行緒，避免影響 Unity
            receiveThread.Start();
            Debug.Log("Connected to server.");
        }
        catch (Exception e)
        {
            Debug.LogError("Could not connect to server: " + e.Message);
        }
    }

    void ReceiveData()
    {
        byte[] buffer = new byte[1024];
        while (isRunning)
        {
            try
            {
                int bytesRead = stream.Read(buffer, 0, buffer.Length); // 讀取資料
                if (bytesRead > 0)
                {
                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    TCP_Data = message;
                    Debug.Log("Received: " + message);
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Receive error: " + e.Message);
                break;
            }
        }
    }

    void OnApplicationQuit()
    {
        isRunning = false; // 結束執行緒
        if (receiveThread != null) receiveThread.Join(); // 確保執行緒安全結束
        if (stream != null) stream.Close();
        if (client != null) client.Close();
    }

    void OnDestroy()
    {
        isRunning = false;
    }
}
