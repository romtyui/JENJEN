using UnityEngine;
using System.IO.Ports;

public class arduino_contect : MonoBehaviour
{
    SerialPort sp = new SerialPort("COM3", 9600); // COM3�n�����A��ڳs������
    [SerializeField] public int data_name;
    [SerializeField] private int lastDataName = -1;
    [SerializeField] private float lastChangeTime = 0f;
    [SerializeField] private float unchangedThreshold = 3f; // �]�w��3��
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
               
                Debug.Log("�qArduino����G" + data);
                if (int.TryParse(data, out data_name))
                {
                    Debug.Log("�ѪR���Ʀr�G" + data_name);

                    // �p�G�ƭȦ�����
                    if (data_name != lastDataName)
                    {
                        lastDataName = data_name;
                        lastChangeTime = Time.time;
                        isStable = false; // ���mí�w���A
                    }

                    // �p�G�ƭȨS�ܡA�B�w�g�W�L�H�Ȭ��
                    else if (!isStable && Time.time - lastChangeTime >= unchangedThreshold)
                    {
                        isStable = true; // �аO��í�w
                        Debug.Log("�ƭȦb 3 ���S�����ܡG" + data_name);

                        // �b�o�̥i�HĲ�o�A�n���ƥ�
                    }
                }
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

