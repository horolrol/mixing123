using UnityEngine;

public class ZoomController : MonoBehaviour
{
    [Header("Zoom Settings")]
    public Camera mainCamera;
    public float normalFOV = 60f;
    public float zoomFOV = 30f;
    public float zoomSpeed = 10f;

    private bool isZooming = false;

    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        mainCamera.fieldOfView = normalFOV;
    }

    void Update()
    {
        // ✅ 우클릭을 "한 번" 눌렀을 때 줌 토글
        if (Input.GetMouseButtonDown(1))
        {
            isZooming = !isZooming;
        }

        // ✅ 현재 상태에 따라 FOV를 변경
        float targetFOV = isZooming ? zoomFOV : normalFOV;
        mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, targetFOV, Time.deltaTime * zoomSpeed);
    }
}
