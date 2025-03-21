using UnityEngine;

public class Object_create_code : MonoBehaviour
{
    public Image_Recognition_script IRS;
    [Header("±±¨î¥Í¦¨")]
    public bool trunBT;
    [SerializeField]
    private string Data_name , OGData_name;
    public GameObject[] create_Objs;
    public GameObject create_pOS;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IRS = GetComponent<Image_Recognition_script>();
    }

    // Update is called once per frame
    void Update()
    {
        int i = -1;
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
    }
}
