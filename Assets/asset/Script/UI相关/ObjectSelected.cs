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

    public float scaleMultiplierOnHover = 1.2f; // 鼠标悬停时的缩放比例
    private RectTransform rectTransform;
    private Vector3 originalScale;


    public AudioClip clickSound; // 音效资源
    private AudioSource audioSource; // 音频源组件


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
            return; // 如果已经是选中状态，不执行任何操作，避免重复触发

        // 遍历所有具有ObjectSelected脚本的游戏对象，将它们的IsSelected设为false
        ObjectSelected[] allSelectors = FindObjectsOfType<ObjectSelected>();
        foreach (ObjectSelected selector in allSelectors)
        {
            if (selector != this)
            {
                selector.IsSelected = false;
                selector.OnSelectionChanged.Invoke();
            }
        }

        // 设置当前按钮为选中状态
        IsSelected = true;
        OnSelectionChanged.Invoke();
    }

    private void Start()
    {
        buttonImage = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        originalScale = rectTransform.localScale; // 保存原本大小

        audioSource = GetComponent<AudioSource>();
    }

    public void OnPointerEnter(PointerEventData eventData)//传递关于鼠标位置，点击按键，按下时间等信息
    {
        
        rectTransform.localScale = originalScale * scaleMultiplierOnHover; // 放大
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
        rectTransform.localScale = originalScale * 0.9f; // 恢复原大小
    }

    public void OnPointerClick()
    {
        
        // 点击时恢复原始大小
        audioSource.PlayOneShot(clickSound);
        rectTransform.localScale = originalScale;


        Invoke("KuoDa", 0.25f);
        //点击完后再次放大

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
