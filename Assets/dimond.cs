using UnityEngine;

public class dimond : MonoBehaviour
{
    [Range(0f, 3f)] public float amplitude = 0.5f;  // �B�ʰ���
    [Range(0f,5f)] public float frequency = 1f;    // �B���W�v

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // �W�U�B�ʡG�u��Y�b�ܰ�
        float newY = startPos.y + Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }
}
