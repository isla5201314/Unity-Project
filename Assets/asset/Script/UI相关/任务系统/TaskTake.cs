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

    public TMP_Text Name;//任务名称
    public bool IsFinish;//任务是否完成
}
