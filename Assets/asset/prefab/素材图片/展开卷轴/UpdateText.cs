using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateText : MonoBehaviour
{
    private BookController BC;
    public GameObject TextManager;
    // Start is called before the first frame update
    void Start()
    {
        BC = TextManager.GetComponent<BookController>();
    }

    public void UpdaTheText()
    {
        BC.UpdateTheText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
