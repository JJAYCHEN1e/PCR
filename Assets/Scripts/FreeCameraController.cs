using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCameraController : MonoBehaviour
{
    // 游戏对象
    public Transform model;
    // 默认距离
    private const float default_distance = 1f;
    // Start is called before the first frame update
    void Start()
    {
        // 旋转归零
        transform.rotation = Quaternion.identity;
        // 初始位置是模型
        Vector3 position = model.position;
        position.z -= default_distance;
        transform.position = position;
    }

    // Update is called once per frame
    void Update()
    {
        float dx = Input.GetAxis("Mouse X");
        float dy = Input.GetAxis("Mouse Y");
        // 鼠标右键旋转
        if (Input.GetMouseButton(1))
        {
                if (Mathf.Abs(dx) > 0 || Mathf.Abs(dy) > 0)
                {
                        // 获取摄像机欧拉角
                        Vector3 angles = transform.rotation.eulerAngles;
                        // 欧拉角表示按照坐标顺序旋转，比如angles.x=30，表示按x轴旋转30°，dy改变引起x轴的变化
                        angles.x = Mathf.Repeat(angles.x + 180f, 360f) - 180f;
                        angles.y += dx;
                        angles.x -= dy;
                        // 设置摄像头旋转
                        Quaternion rotation = Quaternion.identity;
                        rotation.eulerAngles = new Vector3(angles.x, angles.y, 0);
                        transform.rotation = rotation;
                        // 重新设置摄像头位置
                        Vector3 position = model.position;
                        Vector3 distance = rotation * new Vector3(0, 0, default_distance);
                        transform.position = position - distance;
                }
        }
    }
}
