using UnityEngine;
using Cinemachine;

public class CameraControl: MonoBehaviour
{
    public CinemachineFreeLook cinemachineFreeLook; // 引用你的CinemachineFreeLook组件  
    public float turnSpeed = 100f; // 设置镜头转向速度  

    void Update()
    {
        // 如果Q键被按住，镜头向左转  
        if (Input.GetKey(KeyCode.Q))
        {
            cinemachineFreeLook.m_XAxis.Value -= turnSpeed * Time.deltaTime;
        }
        // 如果E键被按住，镜头向右转  
        if (Input.GetKey(KeyCode.E))
        {
            cinemachineFreeLook.m_XAxis.Value += turnSpeed * Time.deltaTime;
        }
    }
}