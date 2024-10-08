using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Talk : MonoBehaviour
{
    public TextAsset[] DH;
    public GameObject TalkPrefab;
    public DialogManager dialogmanager;
    public int index = 0;

    public GameObject TalkUI;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        TalkUI.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        TalkUI.SetActive(true);

        if (other.CompareTag("Player") && Input.GetKey(KeyCode.F))
        {

            TalkPrefab.SetActive(true);
            dialogmanager.page = 0;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}