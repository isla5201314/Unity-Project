using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideLockCursor : MonoBehaviour
{
    public bool isLocked = true;//锁定视角
    public MonoBehaviour photographer;
    public bool isEnabled = true;//开关摄像机脚本

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftAlt))
        {
            isLocked = !isLocked;
            isEnabled = !isEnabled; // 切换状态
        }
        photographer.enabled = isEnabled; // 根据状态启用或禁用脚本
        GameManager.LockCursor = isLocked;
    }


}
