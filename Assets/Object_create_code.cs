using System;
using UnityEngine;
using UnityEngine.Playables;

public class Object_create_code : MonoBehaviour
{
    public Image_Recognition_script IRS;
    [Header("控制生成")]
    public bool trunBT;
    [SerializeField]
    private string Data_name , OGData_name;
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
        int i = -1;
        //測試用代碼
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
        ////實際代碼
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
                /*要生成物件程式*/
                Data_name = null;
                break;
        }
    }
}
