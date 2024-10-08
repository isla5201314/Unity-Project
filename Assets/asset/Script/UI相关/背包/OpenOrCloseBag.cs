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
            // ������ã���ر�
            Bag.SetActive(false);
        }
        else
        {
            // ����رգ�������
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
