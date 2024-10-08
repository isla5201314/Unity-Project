using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverChangeImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler //�����EventSystem��Ľӿ� ������UIԪ�ػ���
{
    public Sprite normalSprite;
    public Sprite hoverSprite;
    private Image buttonImage;

    public float scaleMultiplierOnHover = 1.2f; // �����ͣʱ�����ű���
    private RectTransform rectTransform;
    private Vector3 originalScale;

    
    public AudioClip clickSound; // ��Ч��Դ
    private AudioSource audioSource; // ��ƵԴ���

    private void Start()
    {
        buttonImage = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        originalScale = rectTransform.localScale; // ����ԭ����С

        audioSource = GetComponent<AudioSource>();
    }

    public void OnPointerEnter(PointerEventData eventData)//���ݹ������λ�ã��������������ʱ�����Ϣ
    {
        if(hoverSprite)
        buttonImage.sprite = hoverSprite;
        rectTransform.localScale = originalScale * scaleMultiplierOnHover; // �Ŵ�
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonImage.sprite = normalSprite;
        rectTransform.localScale = originalScale *0.9f; // �ָ�ԭ��С
    }

    public void OnPointerClick()
    {
        // ���ʱ�ָ�ԭʼ��С
        audioSource.PlayOneShot(clickSound);
        rectTransform.localScale = originalScale;
        

        Invoke("KuoDa", 0.25f);
        //�������ٴηŴ�
        
        // �����Ҫ�ڵ��ʱҲ�л���ԭͼ���������������
        //buttonImage.sprite = normalSprite;
    }

    public void KuoDa()
    {
        rectTransform.localScale = originalScale * scaleMultiplierOnHover;
    }

}