using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;        // Nhân vật cần theo dõi
    public Vector3 offset = new Vector3(3f, 0f, -10f); // Khoảng cách camera với nhân vật
    public float smoothSpeed = 5f;  // Tốc độ theo mượt

    void LateUpdate()
    {
        if (target == null) return;

        // Vị trí lý tưởng
        Vector3 desiredPosition = target.position + offset;

        // Di chuyển mượt từ vị trí hiện tại đến vị trí lý tưởng
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Cập nhật vị trí camera
        transform.position = smoothedPosition;
    }
}