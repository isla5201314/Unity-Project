using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 人物2剧情判定 : MonoBehaviour
{
    public DialogManager DialogManager;
    public Talk talk;
    public duihuaData_SO juqingData;
    public Animator animator;

    public Misson misson;
    public Task task;

    public bool IsUpdated;
    // Start is called before the first frame update

    private void Awake()
    {
        animator = GetComponent<Animator>();
        juqingData.青年书生剧情判定 = false;
    }
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        if(talk.index ==1&&juqingData.青年书生剧情判定==false)
        {
            DialogManager.plot = false;
            if (!misson.TaskList.Contains(task))
            {
                MissonManager missionManager = MissonManager.instance;
                missionManager.AddNewMission(task);
                
            }
        }
        if (juqingData.青年书生剧情判定 == true)
        {
            if(!IsUpdated)
            {
                IsUpdated = true;
            }
            DialogManager.plot = true;
        }    

        Over();
    }

    public void Over()
    {
        if(task.IsFinish == true)
        juqingData.青年书生剧情判定 = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKey(KeyCode.F))
        {

            animator.SetTrigger("talk");
        }
    }

}
