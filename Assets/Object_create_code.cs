using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class Object_create_code : MonoBehaviour
{
    public Image_Recognition_script IRS;
    [Header("控制生成")]
    public bool trunBT;
    [SerializeField]
    private string Obj_Data_name, Color_Data_name, OGData_name;
    public GameObject[] create_Objs;
    public GameObject create_pOS;

    public enum OrderStatus { Obstacle/*障礙物*/, Bridge/*橋*/, Null/*不要動作*/ };
    [Header("生成物件狀態")]
    public OrderStatus status;

    // Start is called once before the first execution of Update after the MonoBehaviour is created


    void Start()
    {
        IRS = GetComponent<Image_Recognition_script>();
    }

    // Update is called once per frame
    void Update()
    {
        //int i = -1;
        ////測試用代碼
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
        //實際代碼
        if (trunBT == true)
        {
            string[] parts = IRS.TCP_Data.Split('_');
            Obj_Data_name = parts[0];
            Color_Data_name = parts[1];
            //trunBT = false;
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
            Debug.Log("狀態已改為：" + status);
        }
        else
        {
            Debug.LogWarning("無效的狀態：" + data);
        }
    }
    void Status_List() 
    {
        switch (status) 
        {
            case OrderStatus.Obstacle:
                Instantiate(create_Objs[0], create_pOS.transform);
                /*要生成物件程式*/
                Obj_Data_name = "Null";
                Color_Data_name = "Null";
                Status_Switch("Null");
                break;
            case OrderStatus.Bridge:
                Instantiate(create_Objs[1], create_pOS.transform);
                /*要生成物件程式*/
                Obj_Data_name = "Null";
                Color_Data_name = "Null";
                Status_Switch("Null");
                break;
            case OrderStatus.Null:


                break;
        }
    }
}
