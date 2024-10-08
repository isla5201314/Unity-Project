using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Composites;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System.Runtime.CompilerServices;

public class DialogManager : MonoBehaviour
{
    public HideLockCursor hidelockcursor;

    public GameObject duihuaui;

    private TextAsset dialogDataFile;//对话文本，csv格式

    public TMP_Text dialogText;//对话内容文本

    public TMP_Text nameText;

    public int dialogIndex;//保存当前对话索引值

    public string[] dialogRows;//对话文本，按行分割

    public Button nextButton;//继续按钮

    private Talk currentNPCTalkScript = null;

    public bool isread = false;

    public PlayerCharacter character;

    public bool plot = true;

    public GameObject optionButton;//选项按钮预制体

    public Transform buttonGroup;//选项按钮父节点，用于自动排列
    //public int textnumber = 0;

    public int page;
    // Start is called before the first frame update
    private void Awake()
    {
        plot = true;    
    }

    void Start()
    {

        //UpdateText("start");
    }

    public void OnTriggerStay(Collider other)
    {
        CheckAndAssignNPCTalkScript(other.gameObject);
    }

    private void CheckAndAssignNPCTalkScript(GameObject otherGameObject)
    {


        // 检查碰撞的物体是否是NPC
        if (otherGameObject.CompareTag("NPC"))
        {

            // 尝试获取NPC上的Talk脚本
            currentNPCTalkScript = otherGameObject.GetComponent<Talk>();

            // 如果成功获取到Talk脚本，输出DH变量的值
            if (currentNPCTalkScript != null && !isread)
            {
                //接收外部传来的脚本
                Talk DialogTalk = currentNPCTalkScript;

                isread = true; //关闭通道确保函数只执行一次

                dialogDataFile = DialogTalk.DH[DialogTalk.index];

                ReadText(dialogDataFile);
                ShowDialogRow();

                if (DialogTalk.index < (DialogTalk.DH.Length - 1)&&plot == true) // 如果人物还有下一段对话就索引下一个
                    DialogTalk.index++; //人物身上索引加1下次访问下一段文本

            }
        }
        else
        {
            // 如果不是NPC，确保currentNPCTalkScript被清空
            currentNPCTalkScript = null;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateText(string _name, string _text)
    {
        dialogText.text = _text;
        nameText.text = _name;
    }

    public void ReadText(TextAsset _textAsset)
    {

        if (!nextButton.gameObject.activeSelf)
        {
            nextButton.gameObject.SetActive(true);
        }
        if (!duihuaui.gameObject.activeSelf)
        {
            duihuaui.SetActive(true);
        }
        if (_textAsset != null)
            dialogRows = _textAsset.text.Split('\n');

        //foreach(var row in rows)
        //{
        //    string[] cell = row.Split(",");
        //}
        Debug.Log("success");
    }

    public void ShowDialogRow() //读取文本
    {
        //打开鼠标控制
        hidelockcursor.isEnabled = false;
        hidelockcursor.isLocked = false;
        //关闭操作输入
        character.isspeak = true;

        for (page = 0; page < dialogRows.Length; page++)
        {
            string[] cells = dialogRows[page].Split(',');
            if (cells[0] == "#" && int.Parse(cells[1]) == dialogIndex)
            {
                UpdateText(cells[2], cells[3]);
                dialogIndex = int.Parse(cells[4]);
                nextButton.gameObject.SetActive(true);
                break;
            }
            else if (cells[0] == "&" && int.Parse(cells[1]) == dialogIndex)
            {
                nextButton.gameObject.SetActive(false);
                GenerateOption(page);
            }
            else if (cells[0] == "END" && int.Parse(cells[1]) == dialogIndex)
            {
                duihuaui.gameObject.SetActive(false);
                nextButton.gameObject.SetActive(false);

                dialogIndex = 0;
                isread = false;

                hidelockcursor.isEnabled = true;
                hidelockcursor.isLocked = true;
                character.isspeak = false;
                gameObject.SetActive(false);
            }
        }

    }

    public void OnClickNext()
    {
        ShowDialogRow();
    }

    public void GenerateOption(int _index)
    {
        string[] cells = dialogRows[_index].Split(",");
        if (cells[0] == "&")
        {
            GameObject button = Instantiate(optionButton, buttonGroup);
            //绑定按钮事件
            button.GetComponentInChildren<Text>().text = cells[3];
            button.GetComponent<Button>().onClick.AddListener
            (
             delegate
             {
                 OnOptionClick(int.Parse(cells[4]));
             }
            );
            GenerateOption(_index + 1);

        }
    }

    public void OnOptionClick(int _id)
    {
        dialogIndex = _id;
        ShowDialogRow();
        for (int i = 0; i < buttonGroup.childCount; i++)
        {
            Destroy(buttonGroup.GetChild(i).gameObject);
        }
    }


}