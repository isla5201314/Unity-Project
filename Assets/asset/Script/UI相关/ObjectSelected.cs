using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

public class ObjectSelected : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    public bool IsSelected;
    public UnityEvent OnSelectionChanged;

    //public Sprite hoverSprite;
    private Image buttonImage;

    public float scaleMultiplierOnHover = 1.2f; // �����ͣʱ�����ű���
    private RectTransform rectTransform;
    private Vector3 originalScale;


    public AudioClip clickSound; // ��Ч��Դ
    private AudioSource audioSource; // ��ƵԴ���


    private void OnEnable()
    {
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(SelectButton);
        }
    }

    private void OnDisable()
    {
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.RemoveListener(SelectButton);
        }
    }

    public virtual void SelectButton()
    {
        if (IsSelected)
            return; // ����Ѿ���ѡ��״̬����ִ���κβ����������ظ�����

        // �������о���ObjectSelected�ű�����Ϸ���󣬽����ǵ�IsSelected��Ϊfalse
        ObjectSelected[] allSelectors = FindObjectsOfType<ObjectSelected>();
        foreach (ObjectSelected selector in allSelectors)
        {
            if (selector != this)
            {
                selector.IsSelected = false;
                selector.OnSelectionChanged.Invoke();
            }
        }

        // ���õ�ǰ��ťΪѡ��״̬
        IsSelected = true;
        OnSelectionChanged.Invoke();
    }

    private void Start()
    {
        buttonImage = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        originalScale = rectTransform.localScale; // ����ԭ����С

        audioSource = GetComponent<AudioSource>();
    }

    public void OnPointerEnter(PointerEventData eventData)//���ݹ������λ�ã��������������ʱ�����Ϣ
    {
        
        rectTransform.localScale = originalScale * scaleMultiplierOnHover; // �Ŵ�
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
        rectTransform.localScale = originalScale * 0.9f; // �ָ�ԭ��С
    }

    public void OnPointerClick()
    {
        
        // ���ʱ�ָ�ԭʼ��С
        audioSource.PlayOneShot(clickSound);
        rectTransform.localScale = originalScale;


        Invoke("KuoDa", 0.25f);
        //�������ٴηŴ�

    }

    private void Update()
    {
        if (IsSelected)
            rectTransform.localScale = originalScale * scaleMultiplierOnHover;
        else rectTransform.localScale = originalScale * 0.9f;
        //    buttonImage.sprite = hoverSprite;
        //else buttonImage.sprite = null;
    }

    public void KuoDa()
    {
        rectTransform.localScale = originalScale * scaleMultiplierOnHover;
    }
}
