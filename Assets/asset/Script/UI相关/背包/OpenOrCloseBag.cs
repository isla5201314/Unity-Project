using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenOrCloseBag : MonoBehaviour
{
    public GameObject Bag;

    // Start is called before the first frame update

    public void OpenOrClose()
    {
        if (Bag.activeSelf)
        {
            // 如果启用，则关闭
            Bag.SetActive(false);
        }
        else
        {
            // 如果关闭，则启用
            Bag.SetActive(true);
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
