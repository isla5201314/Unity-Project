using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    private Transform _uiRoot;
    //·�������ֵ�

    private Dictionary<string, string> pathDict;
    //Ԥ�Ƽ������ֵ�

    private Dictionary<string, GameObject> prefabDict;
    //�Ѵ򿪽���Ļ����ֵ�
    
    public Dictionary<string, BasePanel> panelDict;

    public static UIManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new UIManager();
            }
            return _instance;
        }
    }

    public Transform UIRoot
    {
        get
        {
            if(_uiRoot == null)
            {
                if (GameObject.Find("Canvas"))
                {
                    _uiRoot = GameObject.Find("Canvas").transform;
                }
                else
                {
                    _uiRoot = new GameObject("Canvas").transform;
                }
            }
            return _uiRoot;
        }
    }

    private UIManager()
    {
        InitDicts();
    }

    private void InitDicts()
    {
        prefabDict = new Dictionary<string, GameObject>();
        panelDict = new Dictionary<string, BasePanel>();

        pathDict = new Dictionary<string, string>()
        {

        };
    }

    public BasePanel GetPanel(string name)
    {
        BasePanel panel = null;
        //����Ƿ��Ѵ�
        if(panelDict.TryGetValue(name,out panel))
        {
            return panel;
        }
        return null;
    }

    public BasePanel OpenPanel(string name)
    {
        BasePanel panel = null;
        //����Ƿ��Ѵ�
        if(panelDict.TryGetValue(name, out panel))
        {
            Debug.Log("�����Ѵ�" + name);
            return null;
        }

        string path = "";
        if(!pathDict.TryGetValue(name, out path))
        {
            Debug.Log("�������ƴ��󣬻�δ����·��: " + name);
            return null;
        }

        return null; //��ʱ���
    }



}
