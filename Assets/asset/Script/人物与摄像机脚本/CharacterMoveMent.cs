using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class CharacterMoveMent : MonoBehaviour
{
    public Rigidbody rb;

    public Vector3 CurrentInput { get; private set; }
    public float MaxWalkSpeed = 5;
    public float MinWalkSpeed = 2;
    public int iswalk = 3 ;
    public float JumpForce = 5f; // 跳跃力量
    public bool isGrounded = true; // 是否接触地面
    public LayerMask groundLayer; // 在Inspector中指定地面层
    public float raycastDistance = 0.1f; // 射线检测的距离
    public PlayerCharacter character;



    // Start is called before the first frame update

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        character = GetComponent<PlayerCharacter>();
    }

    private void FixedUpdate()
    {
        if (character.isspeak == false)
        {
            if (iswalk == 5)
                rb.MovePosition(rb.position + CurrentInput * MaxWalkSpeed * Time.fixedDeltaTime);
            if (iswalk == 3)
                rb.MovePosition(rb.position + CurrentInput * MinWalkSpeed * Time.fixedDeltaTime);
            CheckGround();

        }
    }

    public void SetMovementInput(Vector3 input)
    {
        CurrentInput = Vector3.ClampMagnitude(input, maxLength: 1);
    }

    // 添加一个公共方法来执行跳跃
    public void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            isGrounded = false; // 跳跃后设置为非地面状态
        }
    }

    // 假设有一个地方来判断角色是否落地，这里简单模拟一个示例
    private void CheckGround()
    {
        // 从角色的位置向下发射射线
        RaycastHit hit;
        if (Physics.Raycast(transform.position + new Vector3 (0, 2, 0), Vector3.down, out hit, raycastDistance, groundLayer))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
}
