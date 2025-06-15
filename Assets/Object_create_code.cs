using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class Object_create_code : MonoBehaviour
{
    public Image_Recognition_script IRS; // 連接 Python TCP 接收腳本

    [Header("接收資料判斷用")]
    public bool trunBT; // 是否啟動資料轉換流程

    [SerializeField] public string Obj_Data_name, Color_Data_name, OGData_name; // 儲存模型與顏色的字串資訊
    public GameObject[] create_Objs; // 可生成的物件（Obstacle、Bridge）
    public GameObject create_pOS; // 預設生成點（未使用）

    public obstacles_code OC; // 擴充類別，控制多格障礙物（可略）

    [SerializeField] public float confirmationTime = 0.5f; // 確認穩定時間
    public float detectionTimer = 0f; // 穩定計時器
    private string lastTCPData = ""; // 上一筆 TCP 字串

    [SerializeField] private GameObject player; // 玩家 GameObject
    [SerializeField] Transform Instantiat_Spawn; // 即時決定的生成點

    // 建立辨識狀態（來自 Python 類型）
    public enum OrderStatus { Obstacle, Bridge,Spring, Triangle, Null };
    [Header("辨識結果狀態")]
    public OrderStatus status;

    public player_control corner; // 玩家控制器（存 count 用）
    public Material Dissolve; // dissolve 動畫材質

    public int receivedTotalBlocks = 0; // 接收來的 total_blocks

    void Start()
    {
        IRS = GetComponent<Image_Recognition_script>();
        player = GameObject.FindWithTag("Player");
        DontDestroyOnLoad(this.gameObject); // 保留物件跨場景
    }

    void Update()
    {
        IRS = GetComponent<Image_Recognition_script>();
        player = GameObject.FindWithTag("Player");

        if (trunBT == true)
        {
            // 如果接收到的資料與上次相同，累計計時器，否則歸零
            if (IRS.TCP_Data == lastTCPData)
            {
                detectionTimer += Time.deltaTime;
            }
            else
            {
                detectionTimer = 0f;
                lastTCPData = IRS.TCP_Data;
            }

            // 如果穩定資料達一定時間
            if (detectionTimer >= confirmationTime)
            {
                string[] parts = IRS.TCP_Data.Split('_');
                if (parts.Length >= 3)
                {
                    Obj_Data_name = parts[0]; // Obstacle 或 Bridge
                    Color_Data_name = parts[1]; // 顏色
                    int.TryParse(parts[2], out receivedTotalBlocks); // 解析數量
                }

                // 設定狀態
                Status_Switch(Obj_Data_name);

                // 取得當前轉角點作為生成位置
                int i = player.gameObject.GetComponent<player_control>().count;
                GameObject TF = player.gameObject.GetComponent<player_control>().Turning_points[i];
                Instantiat_Spawn = TF.GetComponent<countpoint_code>().objtransform.transform;

                // 呼叫生成方法
                Status_List(Instantiat_Spawn);

                // 重置標記
                trunBT = false;
                detectionTimer = 0f;
                lastTCPData = "";
            }
        }
    }

    // 字串轉換為 Enum 狀態
    void Status_Switch(string data)
    {
        if (Enum.TryParse(data, true, out OrderStatus newState))
        {
            status = newState;
            Obj_Data_name = null;
            Color_Data_name = null;
            Debug.Log("成功轉換狀態為: " + status);
        }
        else
        {
            Debug.LogWarning("無法解析狀態: " + data);
        }
    }

    // 根據狀態生成對應物件
    void Status_List(Transform Instantiat_Spawn)
    {
        switch (status)
        {
            case OrderStatus.Obstacle:
                // 生成障礙物並套用 dissolve 材質
                GameObject obj = Instantiate(create_Objs[0], Instantiat_Spawn);
                obj.GetComponent<MeshRenderer>().materials[0] = Dissolve;

                // 設定顏色索引
                if (Color_Data_name == "blue")
                {
                    obj.GetComponent<MeshRenderer>().materials[0].SetFloat("_Color", 1.0f);
                }
                else if (Color_Data_name == "red")
                {
                    obj.GetComponent<MeshRenderer>().materials[0].SetFloat("_Color", 0f);
                }
                else if (Color_Data_name == "green")
                {
                    obj.GetComponent<MeshRenderer>().materials[0].SetFloat("_Color", 2.0f);
                }

                // 啟用淡出動畫（2秒）
                StartCoroutine(DissolveEffect(obj.GetComponent<MeshRenderer>().materials[0], 2f));

                // 重置狀態
                Obj_Data_name = "Null";
                Color_Data_name = "Null";
                Status_Switch("Null");
                break;
            case OrderStatus.Spring:
                Instantiate(create_Objs[2], Instantiat_Spawn);

                // 重置狀態
                Obj_Data_name = "Null";
                Color_Data_name = "Null";
                Status_Switch("Null");
                break; 
            case OrderStatus.Triangle:
                
                // 重置狀態
                Obj_Data_name = "Null";
                Color_Data_name = "Null";
                Status_Switch("Null");
                break;
            case OrderStatus.Bridge:
                // 根據 count 決定橋梁形狀，並生成
                //int i = ((corner.count == 3) ? 0 : 1);
                Instantiate(create_Objs[1], Instantiat_Spawn);

                // 重置狀態
                Obj_Data_name = "Null";
                Color_Data_name = "Null";
                Status_Switch("Null");
                break;

            case OrderStatus.Null:
                // 不執行任何生成
                break;
        }
    }

    // 淡出動畫 Coroutine：將 _Dissolve 參數從 0 → -1
    IEnumerator DissolveEffect(Material mat, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            float dissolveValue = Mathf.Lerp(0f, -1f, t);
            mat.SetFloat("_Dissolve", dissolveValue);
            elapsed += Time.deltaTime;
            yield return null;
        }
        mat.SetFloat("_Dissolve", -1f);
    }
}
