using System;
using UnityEngine;
using UnityEngine.Playables;

public class Object_create_code : MonoBehaviour
{
    public Image_Recognition_script IRS;
    [Header("����ͦ�")]
    public bool trunBT;
    [SerializeField]
    private string Data_name , OGData_name;
    public GameObject[] create_Objs;
    public GameObject create_pOS;

    public enum OrderStatus { Obstacle/*��ê��*/, Bridge/*��*/, Null/*���n�ʧ@*/ };
    [Header("�ͦ����󪬺A")]
    public OrderStatus status;

    // Start is called once before the first execution of Update after the MonoBehaviour is created


    void Start()
    {
        IRS = GetComponent<Image_Recognition_script>();
    }

    // Update is called once per frame
    void Update()
    {
        int i = -1;
        //���եΥN�X
        if(IRS.TCP_Data != null) 
        {
            Data_name = IRS.TCP_Data;
        }
        if(Data_name != OGData_name) 
        {
            if (Data_name == "oxxo")
            {
                i = 0;
            }
            else 
            {
                i = 1;
            }
            trunBT = true;
            OGData_name = Data_name;
        }
        if (trunBT) 
        {
            Instantiate(create_Objs[i], create_pOS.transform);
            trunBT = false;
        }
        ////��ڥN�X
        //if (trunBT = true)
        //{
        //    Data_name = IRS.TCP_Data;
        //    trunBT = false ;
        //}
        //if (Data_name != null) 
        //{
        //    Status_Switch(Data_name);
        //}
    }
    void Status_Switch(string data)
    {
        if (Enum.TryParse(data, true, out OrderStatus newState))
        {
            status = newState;
            Debug.Log("���A�w�אּ�G" + status);
        }
        else
        {
            Debug.LogWarning("�L�Ī����A�G" + data);
        }
    }
    void Status_List() 
    {
        switch (status) 
        {
            case OrderStatus.Obstacle:
                /*�n�ͦ�����{��*/
                Data_name = null;
                break;
        }
    }
}
