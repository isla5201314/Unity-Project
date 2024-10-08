using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    #region Ëø¶¨/Òþ²ØÊó±ê

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