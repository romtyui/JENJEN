using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class Object_create_code : MonoBehaviour
{
    public Image_Recognition_script IRS;
    [Header("����ͦ�")]
    public bool trunBT;
    [SerializeField]
    private string Obj_Data_name, Color_Data_name, OGData_name;
    public GameObject[] create_Objs;
    public GameObject create_pOS;
    
    [SerializeField]Transform Obstacle_Spawn;
    [SerializeField]Transform[] Bridge_Spawn;

    public enum OrderStatus { Obstacle/*��ê��*/, Bridge/*��*/, Null/*���n�ʧ@*/ };
    [Header("�ͦ����󪬺A")]
    public OrderStatus status;

    public player_control corner;

    // Start is called once before the first execution of Update after the MonoBehaviour is created


    void Start()
    {
        IRS = GetComponent<Image_Recognition_script>();
    }

    // Update is called once per frame
    void Update()
    {
        //int i = -1;
        ////���եΥN�X
        //if(IRS.TCP_Data != null) 
        //{
        //    Data_name = IRS.TCP_Data;
        //}
        //if(Data_name != OGData_name) 
        //{
        //    if (Data_name == "oxxo")
        //    {
        //        i = 0;
        //    }
        //    else 
        //    {
        //        i = 1;
        //    }
        //    trunBT = true;
        //    OGData_name = Data_name;
        //}
        //if (trunBT) 
        //{
        //    Instantiate(create_Objs[i], create_pOS.transform);
        //    trunBT = false;
        //}
        //��ڥN�X
        if (trunBT == true)
        {
            if (Obj_Data_name != "" && Color_Data_name != "") 
            {
                string[] parts = IRS.TCP_Data.Split('_');
                Obj_Data_name = parts[0];
                Color_Data_name = parts[1];
                trunBT = false;
            }
           
        }
        if (Obj_Data_name != "" && Color_Data_name != "")
        {
            Status_Switch(Obj_Data_name);
            Status_List();
        }
    }
    void Status_Switch(string data)
    {
        if (Enum.TryParse(data, true, out OrderStatus newState))
        {
            status = newState;
            Obj_Data_name = null;
            Color_Data_name = null;
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
                Instantiate(create_Objs[0], Obstacle_Spawn);
                /*�n�ͦ�����{��*/
                Obj_Data_name = "Null";
                Color_Data_name = "Null";
                Status_Switch("Null");
                break;
            case OrderStatus.Bridge:
                int i =((corner.count == 3 ) ? 0 : 1);
                Instantiate(create_Objs[1], Bridge_Spawn[i]);
                /*�n�ͦ�����{��*/
                Obj_Data_name = "Null";
                Color_Data_name = "Null";
                Status_Switch("Null");
                break;
            case OrderStatus.Null:


                break;
        }
    }
}
