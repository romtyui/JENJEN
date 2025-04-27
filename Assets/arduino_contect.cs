using UnityEngine;
using System.IO.Ports;

public class arduino_contect : MonoBehaviour
{
    SerialPort sp = new SerialPort("COM3", 9600); // COM3�n�����A��ڳs������

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
                Debug.Log("�qArduino����G" + data);

                if (data.Contains("MAGNET_DETECTED"))
                {
                    // ����A�n�����ơA��p��ܴ���
                    Debug.Log("��������K�I");
                }
                else if (data.Contains("NO_MAGNET"))
                {
                    // ���K���}�A�i�H���O������
                    Debug.Log("�S����������K�C");
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

