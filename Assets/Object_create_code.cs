using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class Object_create_code : MonoBehaviour
{
    public Image_Recognition_script IRS;
    [Header("����ͦ�")]
    public bool trunBT;

    [SerializeField] public string Obj_Data_name, Color_Data_name, OGData_name;
    public GameObject[] create_Objs;
    public GameObject create_pOS;

    public obstacles_code OC;
    [SerializeField] public float confirmationTime = 0.5f;
    public float detectionTimer = 0f;
    private string lastTCPData = "";
    [SerializeField] private GameObject player;
    //[SerializeField]Transform Obstacle_Spawn;
    [SerializeField]Transform Instantiat_Spawn;

    public enum OrderStatus { Obstacle/*��ê��*/, Bridge/*��*/, Null/*���n�ʧ@*/ };
    [Header("�ͦ����󪬺A")]
    public OrderStatus status;

    public player_control corner;
    public Material Dissolve;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    void Start()
    {
        IRS = GetComponent<Image_Recognition_script>();
        player = GameObject.FindWithTag("Player");
        DontDestroyOnLoad(this.gameObject); // 切換場景時保留這個物件


    }

    // Update is called once per frame
    void Update()
    {
        IRS = GetComponent<Image_Recognition_script>();
        player = GameObject.FindWithTag("Player");
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
            if (IRS.TCP_Data == lastTCPData)
            {
                detectionTimer += Time.deltaTime;
            }
            else
            {
                detectionTimer = 0f;
                lastTCPData = IRS.TCP_Data;
            }

            if (detectionTimer >= confirmationTime)
            {
                string[] parts = IRS.TCP_Data.Split('_');
                if (parts.Length >= 2)
                {
                    Obj_Data_name = parts[0];
                    Color_Data_name = parts[1];
                }

                Status_Switch(Obj_Data_name);

                int i = player.gameObject.GetComponent<player_control>().count;
                GameObject TF = player.gameObject.GetComponent<player_control>().Turning_points[i];
                Instantiat_Spawn = TF.GetComponent<countpoint_code>().objtransform.transform;

                Status_List(Instantiat_Spawn);

                // 重置旗標
                trunBT = false;
                detectionTimer = 0f;
                lastTCPData = "";
            }

        }
        //if (Obj_Data_name != "" && Color_Data_name != "")
        //{
        //    Status_Switch(Obj_Data_name);
        //    int i = gameObject.GetComponent<player_control>().count;
        //    GameObject TF = gameObject.GetComponent<player_control>().Turning_points[i];
        //    Instantiat_Spawn = TF.GetComponent<countpoint_code>().objtransform.transform;
        //    Status_List(Instantiat_Spawn);
        //}
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
    void Status_List(Transform Instantiat_Spawn) 
    {
        switch (status) 
        {
            case OrderStatus.Obstacle:
                GameObject obj = Instantiate(create_Objs[0], Instantiat_Spawn);
                obj.GetComponent<MeshRenderer>().materials[0] = Dissolve;
                //for (int j = 0; j < obj.GetComponent<obstacles_code>().cubes.Length; j++) 
                //{
                float dissolve = Mathf.PingPong(Time.time * 2f, 2f) - 1f;
                if (Color_Data_name == "blue")
                {
                    //obj.GetComponent<MeshRenderer>().materials[0].SetFloat("_Dissolve", dissolve);
                    obj.GetComponent<MeshRenderer>().materials[0].SetFloat("_Color", 1.0f);
                }
                else if (Color_Data_name == "red")
                {
                    //obj.GetComponent<MeshRenderer>().materials[0].SetFloat("_Dissolve", dissolve);
                    obj.GetComponent<MeshRenderer>().materials[0].SetFloat("_Color", 0f);
                }
                else if (Color_Data_name == "green")
                {
                    //obj.GetComponent<MeshRenderer>().materials[0].SetFloat("_Dissolve", dissolve);
                    obj.GetComponent<MeshRenderer>().materials[0].SetFloat("_Color", 2.0f);
                }
                // 啟動 Dissolve 動畫（一次性從 0 到 -1）
                StartCoroutine(DissolveEffect(obj.GetComponent<MeshRenderer>().materials[0], 2f));  // 2秒內完成 dissolve

                //}
                /*�n�ͦ�����{��*/
                Obj_Data_name = "Null";
                Color_Data_name = "Null";
                Status_Switch("Null");
                break;
            case OrderStatus.Bridge:
                int i =((corner.count == 3 ) ? 0 : 1);
                Instantiate(create_Objs[1], Instantiat_Spawn);
                /*�n�ͦ�����{��*/
                Obj_Data_name = "Null";
                Color_Data_name = "Null";
                Status_Switch("Null");
                break;
            case OrderStatus.Null:


                break;
        }
    }
    IEnumerator DissolveEffect(Material mat, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float t = elapsed / duration;                          // 0 → 1
            float dissolveValue = Mathf.Lerp(0f, -1f, t);          // 線性淡出
            mat.SetFloat("_Dissolve", dissolveValue);
            elapsed += Time.deltaTime;
            yield return null;
        }

        mat.SetFloat("_Dissolve", -1f);  // 確保結束時設為 -1
    }
}
