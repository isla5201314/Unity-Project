using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public Transform startPos; // 起点  
    public Transform endPos;   // 终点  
    public float speed = 2f;   // 移动速度  
    private bool isMovingToEnd = true; // 控制NPC的移动方向  

    private void Update()
    {
        Transform targetPos = isMovingToEnd ? endPos : startPos; // 根据移动方向选择目标点  
        

        // 朝向目标点  
        Vector3 direction = (targetPos.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f); // 平滑旋转到目标朝向  

        // 向目标点移动  
        transform.position = Vector3.MoveTowards(transform.position, targetPos.position, speed * Time.deltaTime);

        // 检查是否到达目标点  
        if (Vector3.Distance(transform.position, targetPos.position) < 0.1f)
        {
            // 切换移动方向  
            isMovingToEnd = !isMovingToEnd;

            // 到达目标点，旋转180°  
            //transform.Rotate(0f, 180f, 0f, Space.World);
        }
    }
}