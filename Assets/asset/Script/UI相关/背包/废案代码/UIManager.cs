using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    private Transform _uiRoot;
    //路径配置字典

    private Dictionary<string, string> pathDict;
    //预制件缓存字典

    private Dictionary<string, GameObject> prefabDict;
    //已打开界面的缓存字典
    
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
        //检查是否已打开
        if(panelDict.TryGetValue(name,out panel))
        {
            return panel;
        }
        return null;
    }

    public BasePanel OpenPanel(string name)
    {
        BasePanel panel = null;
        //检查是否已打开
        if(panelDict.TryGetValue(name, out panel))
        {
            Debug.Log("界面已打开" + name);
            return null;
        }

        string path = "";
        if(!pathDict.TryGetValue(name, out path))
        {
            Debug.Log("界面名称错误，或未配置路径: " + name);
            return null;
        }

        return null; //临时添加
    }



}
