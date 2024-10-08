using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideLockCursor : MonoBehaviour
{
    public bool isLocked = true;//�����ӽ�
    public MonoBehaviour photographer;
    public bool isEnabled = true;//����������ű�

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftAlt))
        {
            isLocked = !isLocked;
            isEnabled = !isEnabled; // �л�״̬
        }
        photographer.enabled = isEnabled; // ����״̬���û���ýű�
        GameManager.LockCursor = isLocked;
    }


}
