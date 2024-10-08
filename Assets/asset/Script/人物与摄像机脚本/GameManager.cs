using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    #region ����/�������

    public static bool LockCursor
    {
        get => Cursor.lockState == CursorLockMode.Locked;

        set
        {
            Cursor.visible = !value;
            Cursor.lockState = value ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }

    #endregion
}