using UnityEngine;
using UnityEngine.UI;

public class first_animation : MonoBehaviour
{
    public Image[] images;
    public static float animation_totle_time ;
    [Range(0f, 5f)]
    public float[] animations_time;
    private int currentIndex = 0;
    private float currentTimer = 0f;

    public boat_script boat_Script;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < animations_time.Length;i++) 
        {
            animation_totle_time += animations_time[i];
        }
        // 初始化所有圖片為透明且不顯示
        foreach (var img in images)
        {
            img.color = new Color(img.color.r, img.color.g, img.color.b, 0f);
            img.enabled = false;
        }

        // 開始第一張
        if (images.Length > 0)
        {
            images[0].enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentIndex >= images.Length) return;

        currentTimer += Time.deltaTime;
        float fadeDuration = animations_time[currentIndex];

        if (fadeDuration > 0)
        {
            float alpha = Mathf.Clamp01(currentTimer / fadeDuration);
            Image currentImage = images[currentIndex];
            Color color = currentImage.color;
            color.a = alpha;
            currentImage.color = color;

            // 如果完成淡入，準備下一張
            if (alpha >= 1f)
            {
                currentIndex++;
                currentTimer = 0f;

                if (currentIndex < images.Length)
                {
                    images[currentIndex].enabled = true;
                }
                else
                {
                    // ✅ 所有圖片播放完畢後，關閉所有圖片
                    foreach (var img in images)
                    {
                        this.gameObject.SetActive(false);
                        if (boat_Script != null) 
                        {
                            boat_Script.stardo = true;
                        }
                    }

                    // 若需要執行其他邏輯可在此加上，例如：
                    // gameObject.SetActive(false);
                    // onAllImagesFinished.Invoke(); // 觸發事件等
                }
            }
        }
    }
}
