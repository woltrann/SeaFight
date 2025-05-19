using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraScaler : MonoBehaviour
{
    [Header("Referans Tasarým")]
    public float defaultAspect = 9f / 16f;            // 16:9 ekran oraný (dikey mod için)
    public float defaultOrthographicSize = 5f;        // Tasarým yaptýðýn orto size

    private Camera cam;

    void Update()
    {
        cam = GetComponent<Camera>();

        if (cam.orthographic)
        {
            float currentAspect = (float)Screen.width / (float)Screen.height;
            float sizeAdjustment = defaultAspect / currentAspect;
            cam.orthographicSize = defaultOrthographicSize * sizeAdjustment;
        }
    }
}
