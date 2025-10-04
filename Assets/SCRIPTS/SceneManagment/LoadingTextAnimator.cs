using UnityEngine;
using TMPro;

public class LoadingTextAnimator : MonoBehaviour
{
    public TextMeshProUGUI loadingText;
    public float dotInterval = 0.5f;

    private float timer;
    private int dotCount;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= dotInterval)
        {
            timer = 0f;
            dotCount = (dotCount + 1) % 4;
            loadingText.text = "Cargando" + new string('.', dotCount);
        }
    }
}
