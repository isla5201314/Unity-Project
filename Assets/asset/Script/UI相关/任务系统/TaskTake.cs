using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskTake : MonoBehaviour
{
    public Task task;

    public Image Finish;
    public Sprite NotFinishSprite;
    public Sprite ISFinishSprite;

    public TMP_Text Name;//��������
    public bool IsFinish;//�����Ƿ����
}
