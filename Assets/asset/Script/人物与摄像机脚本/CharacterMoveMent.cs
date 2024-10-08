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
    public float JumpForce = 5f; // ��Ծ����
    public bool isGrounded = true; // �Ƿ�Ӵ�����
    public LayerMask groundLayer; // ��Inspector��ָ�������
    public float raycastDistance = 0.1f; // ���߼��ľ���
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

    // ���һ������������ִ����Ծ
    public void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            isGrounded = false; // ��Ծ������Ϊ�ǵ���״̬
        }
    }

    // ������һ���ط����жϽ�ɫ�Ƿ���أ������ģ��һ��ʾ��
    private void CheckGround()
    {
        // �ӽ�ɫ��λ�����·�������
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
