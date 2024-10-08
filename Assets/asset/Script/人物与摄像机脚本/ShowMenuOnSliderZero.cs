using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class ShowMenuOnSliderZero : MonoBehaviour
{
    public Slider slider;
    public GameObject imageObjectToShowHide; // 修改为引用包含Image和Button的整个GameObject

    // 在Awake或Start方法中初始化引用并设置初始状态
    private void Start()
    {
        // 确保已经正确关联Slider和包含Image、Button的GameObject
        if (slider == null)
            slider = GetComponent<Slider>();
        if (imageObjectToShowHide == null)
            imageObjectToShowHide = GetComponent<GameObject>(); // 或者直接在Unity编辑器中拖拽包含Image和Button的GameObject到这里

        // 设置初始状态，隐藏包含Image和Button的GameObject
        imageObjectToShowHide.SetActive(false);

        // 注册Slider的ValueChanged事件
        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    // 当Slider的值改变时调用此函数
    private void OnSliderValueChanged(float value)
    {
        // 如果Slider的值等于0，则显示包含Image和Button的GameObject；否则，隐藏它
        imageObjectToShowHide.SetActive(value == 0);
    }
}
