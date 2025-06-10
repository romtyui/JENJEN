using UnityEngine;

public class dimond : MonoBehaviour
{
    [Range(0f, 3f)] public float amplitude = 0.5f;  // 浮動高度
    [Range(0f,5f)] public float frequency = 1f;    // 浮動頻率

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // 上下浮動：只有Y軸變動
        float newY = startPos.y + Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }
}
