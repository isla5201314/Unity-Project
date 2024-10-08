using UnityEngine;
using Cinemachine;

public class MouseScrollCinemachineControl: MonoBehaviour
{
    public CinemachineFreeLook freeLookCam; // 这个需要在Inspector中拖拽赋值

    private void Update()
    {
        // 获取鼠标滚轮的滚动值
        float scrollDelta = Input.GetAxis("Mouse ScrollWheel");

        // 如果滚轮有滚动，则调整Y轴偏移
        if (scrollDelta != 0f)
        {
            // 根据你的需要调整滚动速度系数
            float ySpeed = 10f * scrollDelta;
            freeLookCam.m_YAxis.m_InputAxisValue = ySpeed;
        }
        else
        {
            // 当没有滚动时，重置Y轴输入为0，防止持续移动
            freeLookCam.m_YAxis.m_InputAxisValue = 0f;
        }
    }
}