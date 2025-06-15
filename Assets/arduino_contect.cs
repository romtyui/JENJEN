using UnityEngine;
using System.IO.Ports;

public class arduino_contect : MonoBehaviour
{
    private SerialPort sp;

    [SerializeField] public int data_name = -1;
    [SerializeField] private int lastDataName = -1;
    [SerializeField] private float lastChangeTime = 0f;
    [SerializeField] private float unchangedThreshold = 3f; // �ƭȥ����ܮɶ����e�]��^
    [SerializeField] public bool isStable = false;
    public player_control player_Control;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        player_Control = GameObject.FindWithTag("Player").GetComponent<player_control>();

        sp = new SerialPort("COM6", 9600); // �Ч令��� Arduino �ҳs���� COM ��
        try
        {
            sp.Open();
            sp.ReadTimeout = 50;
            Debug.Log("Serial Port Opened!");
        }
        catch (System.Exception e)
        {
            Debug.LogError("��f�L�k���}�G" + e.Message);
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
                Debug.Log("�qArduino����G" + data);

                // ���ձN���쪺�r���ର�Ʀr
                if (int.TryParse(data, out int parsedValue))
                {
                    // �u���� 1~4 �����ļƦr
                    if (parsedValue >= 1 && parsedValue <= 4)
                    {
                        data_name = parsedValue;
                        Debug.Log("�ѪR�����ļƦr�G" + data_name);

                        if (data_name != lastDataName)
                        {
                            lastDataName = data_name;
                            lastChangeTime = Time.time;
                            isStable = false; // �ƭ��ܤơA���]í�w��
                        }
                        else if (!isStable && Time.time - lastChangeTime >= unchangedThreshold)
                        {
                            isStable = true;
                            player_Control.turning_BT = true;
                            Debug.Log("�ƭȦb " + unchangedThreshold + " �������ܡG" + data_name);
                            // �i�b��Ĳ�oí�w���A�U���ƥ�
                        }
                    }
                    else
                    {
                        Debug.LogWarning("���줣�b 1~4 �d�򤺪��Ʀr�G" + parsedValue);
                    }
                }
                else
                {
                    Debug.LogWarning("����D�Ʀr�榡��ơG" + data);
                }
            }
            catch (System.TimeoutException) { }
            catch (System.Exception ex)
            {
                Debug.LogWarning("Ū����ƿ��~�G" + ex.Message);
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

