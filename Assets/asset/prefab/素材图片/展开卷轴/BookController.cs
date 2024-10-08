using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookController : MonoBehaviour
{
    public Animator anim;
    public GameObject NoobTXT;
    public GameObject CloseButton;

    [Header("∑≠“≥À˘”√")]
    public GameObject[] Page;
    public GameObject TurnLeftButton;
    public GameObject TurnRightButton;
    public int PageIndex = 0;
    public Text PageText;

    // Start is called before the first frame update
    private void Awake()
    {
        anim = NoobTXT.GetComponent<Animator>();
        NoobTXT.SetActive(false);
        
    }

    public void UpdateTheText()
    {
        for(int i = 0; i < Page.Length; i++)
        {
            Page[i].SetActive(false);
        }
        Page[PageIndex].SetActive(true);

        PageText.text = $"{PageIndex + 1} / {Page.Length}";
    }

    public void TurnTheLeft()
    {
        PageIndex--;
        anim.SetTrigger("IsTurn");
        //Invoke("UpdateTheText", 1.58f);
    }

    public void TurnTheRight()
    {
        PageIndex++;
        anim.SetTrigger("IsTurn");
        //Invoke("UpdateTheText", 1.58f);
    }

    private void OpenButton()
    {
        //if (!CloseButton.activeSelf)
        CloseButton.SetActive(true);
        
    }

    private void CloseTheButton()
    {
        CloseButton.SetActive(false);
    }

    public void OpenBook()
    {
        NoobTXT.SetActive(true);
        anim.SetTrigger("Isopen");

        Invoke("OpenButton", 2.0f);
    }

    public void CloseBook()
    {
        anim.SetTrigger("Isclose");
        Invoke("Close", 2.0f);

        Invoke("CloseTheButton", 0.3f);
    }

    private void Close()
    {
        NoobTXT.SetActive(false);
    }

    public void TurnBook()
    {
        anim.SetTrigger("Isturn");
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PageIndex >= (Page.Length - 1))
        {
            TurnRightButton.SetActive(false);
        }
        else TurnRightButton.SetActive(true);

        if(PageIndex == 0)
        {
            TurnLeftButton.SetActive(false);
        }
        else TurnLeftButton.SetActive(true);
    }


}
