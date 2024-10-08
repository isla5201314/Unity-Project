using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverChangeImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler //存放于EventSystem里的接口 仅用于UI元素互动
{
    public Sprite normalSprite;
    public Sprite hoverSprite;
    private Image buttonImage;

    public float scaleMultiplierOnHover = 1.2f; // 鼠标悬停时的缩放比例
    private RectTransform rectTransform;
    private Vector3 originalScale;

    
    public AudioClip clickSound; // 音效资源
    private AudioSource audioSource; // 音频源组件

    private void Start()
    {
        buttonImage = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        originalScale = rectTransform.localScale; // 保存原本大小

        audioSource = GetComponent<AudioSource>();
    }

    public void OnPointerEnter(PointerEventData eventData)//传递关于鼠标位置，点击按键，按下时间等信息
    {
        if(hoverSprite)
        buttonImage.sprite = hoverSprite;
        rectTransform.localScale = originalScale * scaleMultiplierOnHover; // 放大
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonImage.sprite = normalSprite;
        rectTransform.localScale = originalScale *0.9f; // 恢复原大小
    }

    public void OnPointerClick()
    {
        // 点击时恢复原始大小
        audioSource.PlayOneShot(clickSound);
        rectTransform.localScale = originalScale;
        

        Invoke("KuoDa", 0.25f);
        //点击完后再次放大
        
        // 如果需要在点击时也切换回原图，可以在这里添加
        //buttonImage.sprite = normalSprite;
    }

    public void KuoDa()
    {
        rectTransform.localScale = originalScale * scaleMultiplierOnHover;
    }

}