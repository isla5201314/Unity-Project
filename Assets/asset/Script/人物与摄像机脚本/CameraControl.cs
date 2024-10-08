using UnityEngine;
using Cinemachine;

public class CameraControl: MonoBehaviour
{
    public CinemachineFreeLook cinemachineFreeLook; // �������CinemachineFreeLook���  
    public float turnSpeed = 100f; // ���þ�ͷת���ٶ�  

    void Update()
    {
        // ���Q������ס����ͷ����ת  
        if (Input.GetKey(KeyCode.Q))
        {
            cinemachineFreeLook.m_XAxis.Value -= turnSpeed * Time.deltaTime;
        }
        // ���E������ס����ͷ����ת  
        if (Input.GetKey(KeyCode.E))
        {
            cinemachineFreeLook.m_XAxis.Value += turnSpeed * Time.deltaTime;
        }
    }
}