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

    private TextAsset dialogDataFile;//�Ի��ı���csv��ʽ

    public TMP_Text dialogText;//�Ի������ı�

    public TMP_Text nameText;

    public int dialogIndex;//���浱ǰ�Ի�����ֵ

    public string[] dialogRows;//�Ի��ı������зָ�

    public Button nextButton;//������ť

    private Talk currentNPCTalkScript = null;

    public bool isread = false;

    public PlayerCharacter character;

    public bool plot = true;

    public GameObject optionButton;//ѡ�ťԤ����

    public Transform buttonGroup;//ѡ�ť���ڵ㣬�����Զ�����
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


        // �����ײ�������Ƿ���NPC
        if (otherGameObject.CompareTag("NPC"))
        {

            // ���Ի�ȡNPC�ϵ�Talk�ű�
            currentNPCTalkScript = otherGameObject.GetComponent<Talk>();

            // ����ɹ���ȡ��Talk�ű������DH������ֵ
            if (currentNPCTalkScript != null && !isread)
            {
                //�����ⲿ�����Ľű�
                Talk DialogTalk = currentNPCTalkScript;

                isread = true; //�ر�ͨ��ȷ������ִֻ��һ��

                dialogDataFile = DialogTalk.DH[DialogTalk.index];

                ReadText(dialogDataFile);
                ShowDialogRow();

                if (DialogTalk.index < (DialogTalk.DH.Length - 1)&&plot == true) // ������ﻹ����һ�ζԻ���������һ��
                    DialogTalk.index++; //��������������1�´η�����һ���ı�

            }
        }
        else
        {
            // �������NPC��ȷ��currentNPCTalkScript�����
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

    public void ShowDialogRow() //��ȡ�ı�
    {
        //��������
        hidelockcursor.isEnabled = false;
        hidelockcursor.isLocked = false;
        //�رղ�������
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
            //�󶨰�ť�¼�
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