using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class ShowMenuOnSliderZero : MonoBehaviour
{
    public Slider slider;
    public GameObject imageObjectToShowHide; // �޸�Ϊ���ð���Image��Button������GameObject

    // ��Awake��Start�����г�ʼ�����ò����ó�ʼ״̬
    private void Start()
    {
        // ȷ���Ѿ���ȷ����Slider�Ͱ���Image��Button��GameObject
        if (slider == null)
            slider = GetComponent<Slider>();
        if (imageObjectToShowHide == null)
            imageObjectToShowHide = GetComponent<GameObject>(); // ����ֱ����Unity�༭������ק����Image��Button��GameObject������

        // ���ó�ʼ״̬�����ذ���Image��Button��GameObject
        imageObjectToShowHide.SetActive(false);

        // ע��Slider��ValueChanged�¼�
        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    // ��Slider��ֵ�ı�ʱ���ô˺���
    private void OnSliderValueChanged(float value)
    {
        // ���Slider��ֵ����0������ʾ����Image��Button��GameObject������������
        imageObjectToShowHide.SetActive(value == 0);
    }
}
