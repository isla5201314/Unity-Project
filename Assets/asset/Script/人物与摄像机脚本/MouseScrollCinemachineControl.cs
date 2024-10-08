using UnityEngine;
using Cinemachine;

public class MouseScrollCinemachineControl: MonoBehaviour
{
    public CinemachineFreeLook freeLookCam; // �����Ҫ��Inspector����ק��ֵ

    private void Update()
    {
        // ��ȡ�����ֵĹ���ֵ
        float scrollDelta = Input.GetAxis("Mouse ScrollWheel");

        // ��������й����������Y��ƫ��
        if (scrollDelta != 0f)
        {
            // ���������Ҫ���������ٶ�ϵ��
            float ySpeed = 10f * scrollDelta;
            freeLookCam.m_YAxis.m_InputAxisValue = ySpeed;
        }
        else
        {
            // ��û�й���ʱ������Y������Ϊ0����ֹ�����ƶ�
            freeLookCam.m_YAxis.m_InputAxisValue = 0f;
        }
    }
}