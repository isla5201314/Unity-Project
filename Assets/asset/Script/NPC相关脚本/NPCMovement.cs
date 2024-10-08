using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public Transform startPos; // ���  
    public Transform endPos;   // �յ�  
    public float speed = 2f;   // �ƶ��ٶ�  
    private bool isMovingToEnd = true; // ����NPC���ƶ�����  

    private void Update()
    {
        Transform targetPos = isMovingToEnd ? endPos : startPos; // �����ƶ�����ѡ��Ŀ���  
        

        // ����Ŀ���  
        Vector3 direction = (targetPos.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f); // ƽ����ת��Ŀ�곯��  

        // ��Ŀ����ƶ�  
        transform.position = Vector3.MoveTowards(transform.position, targetPos.position, speed * Time.deltaTime);

        // ����Ƿ񵽴�Ŀ���  
        if (Vector3.Distance(transform.position, targetPos.position) < 0.1f)
        {
            // �л��ƶ�����  
            isMovingToEnd = !isMovingToEnd;

            // ����Ŀ��㣬��ת180��  
            //transform.Rotate(0f, 180f, 0f, Space.World);
        }
    }
}