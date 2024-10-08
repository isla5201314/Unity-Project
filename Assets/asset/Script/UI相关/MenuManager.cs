using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private Animator anim;

    private bool isCurrentlyOpen;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        isCurrentlyOpen = false;
    }

    public void ToggleIsClose()
    {
        // 根据当前状态来决定激活哪个Trigger
        if (isCurrentlyOpen)
        {
            anim.SetTrigger("isClose");
            isCurrentlyOpen = false;
        }
        else
        {
            anim.SetTrigger("isOpen");
            isCurrentlyOpen = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
