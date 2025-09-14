using UnityEngine;

/// <summary>
/// 简单的第三人称相机控制脚本
/// </summary>
public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;           
    public float distance = 5.0f;     
    public float height = 1.0f;        
    public float rotationSpeed = 5.0f; 
    public float minYAngle = -20f;     
    public float maxYAngle = 60f;      

    private float currentX = 0f;
    private float currentY = 20f;

    void LateUpdate()
    {
        if (target == null) return;

        // 鼠标输入
        currentX += Input.GetAxis("Mouse X") * rotationSpeed;
        currentY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        currentY = Mathf.Clamp(currentY, minYAngle, maxYAngle);

        // 计算相机位置
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 dir = new Vector3(0, 0, -distance);
        Vector3 position = target.position + Vector3.up * height + rotation * dir;

        transform.position = position;
        transform.LookAt(target.position + Vector3.up * height);
    }
}