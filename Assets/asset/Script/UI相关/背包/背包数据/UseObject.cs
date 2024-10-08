using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseObject : MonoBehaviour
{
    public Slot slot;
    public PlayerCharacter PlayerCharacter;
    public CharacterMoveMent CM;

    public Inventory PlayerInventory;
    public Items item;//ͨ��֤

    public Task task;

    public float SpIns = 0;

    public Items[] RandomsObjects;

    private IEnumerator UseTheSP()
    {
        SpIns += 0.15f;
        yield return new WaitForSeconds(60.0f);
        SpIns -= 0.15f;
    }

    private IEnumerator UseTheSpeed()
    {
        CM.MinWalkSpeed += 2;
        CM.MaxWalkSpeed += 3;
        yield return new WaitForSeconds(60.0f);
        CM.MinWalkSpeed -= 2;
        CM.MaxWalkSpeed -= 3;
    }

    private IEnumerator UseTheJump()
    {
        CM.JumpForce += 3;
        yield return new WaitForSeconds(60.0f);
        CM.JumpForce -= 3;
    }

    private void Usebag()
    {
        foreach(Items items in RandomsObjects)
        {
            items.Num += Random.Range(0, 3);
        }
        RandomsObjects[4].Num += Random.Range(25, 100);
    }

    private void UseHP()
    {
        PlayerCharacter.HP += 35;
    }

   
    public void Onclick()
    {
        if (slot != null)
        {
            slot.slotItems.Num--;
            switch (slot.slotItems.id)
            {
                case 0: //�����ٶȵ�
                    StartCoroutine(UseTheSpeed());
                    break;

                case 1: //����������
                    StartCoroutine(UseTheSP());
                    break;

                case 2://������Ծ��
                    StartCoroutine(UseTheJump());
                    break;

                case 3: //���ûظ���
                    UseHP();
                    break;

                case 4: //ʹ�ý���
                    Usebag();
                    break;
                case 10://ʹ�ô���� 
                    if (!PlayerInventory.itemsList.Contains(item))
                    {
                        PlayerInventory.itemsList.Add(item);
                        item.Num += 1;
                    }
                    else
                    {
                        item.Num += 1;
                    }
                    Debug.Log("�������");
                    task.IsFinish = true;
                    break;

               
                default: break;
            }



        }
    }
}
