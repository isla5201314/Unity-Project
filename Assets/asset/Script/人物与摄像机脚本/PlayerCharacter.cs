using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class PlayerCharacter : MonoBehaviour
{
    private Animator animator;
    private float rotationDegrees = 45f; // ������ת�Ļ����Ƕ�
    private float additionalRotationForBackward = 180f; // ���ʱ������ת�ĽǶ�
    public float WalkSpeed;
    public bool isspeak = false;

    [Header("����ֵ���")]
    [SerializeField]
    private Slider HpSlider;
    public float HP = 100;
    public TMP_Text HpTxt;

    [Header("�������")]
    [SerializeField]
    private Slider SpSlider;
    public UseObject UO;
    public float SP=100;
    public float Des = 0f;
    public TMP_Text SpTxt;
    private bool IsRun;


    private CharacterMoveMent characterMove;
    [SerializeField]
    private Photographer photographer;
    [SerializeField]
    private Transform followingTarget;//����������λ
                                      // Start is called before the first frame update


    private void ChangeTheSP()
    {
        SP -= Des; // ��ִ�м�������
        SP = Mathf.Clamp(SP, 0f, 100f); // ����Լ��SP��ֵ��0��100֮��
        SpSlider.value = SP; // �����»����ֵ
        SpTxt.text = "SP:" + SP.ToString("0.0") + "/100";

        if (IsRun && SP>10 && !isspeak)
        {
            characterMove.iswalk = 5;
            Des = 0.2f +UO.SpIns;

        }
        else if(!isspeak)
        {
            characterMove.iswalk = 3;
            Des = -0.1f + UO.SpIns;
        }
        else
        {
            Des = -0.1f + UO.SpIns;
        }
    }

    private void ChangeTheHP()
    {
        HP = Mathf.Clamp(HP, 0f, 100f);
        HpSlider.value = HP; // �����»����ֵ
        HpTxt.text = "HP:" + HP.ToString("0.0") + "/100";
    }

    private void Awake()
    {
        characterMove = GetComponent<CharacterMoveMent>();

        photographer.InitCamera(followingTarget);//��ʼ�����������

        animator = GetComponent<Animator>();


       
    }

    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        ChangeTheSP();
        ChangeTheHP();
    }

    // Update is called once per frame
    void Update()
    {
        if (isspeak == false)
        {

            UpdateMovementInput();
            if (Input.GetKeyDown(KeyCode.Space) && characterMove.isGrounded)
            {
                animator.SetTrigger("jump");
                characterMove.Jump();
            }
            // �����ƶ��ٶȵ�����ɫ���򣨽�ˮƽ����
            RotateAccordingToMovement();
        }

        //// ���ֽ�ɫ�����������ˮƽ����
        //RotateToCameraDirection();


        float horizontalSpeed = CalculateHorizontalSpeed();
        animator.SetFloat("speed", horizontalSpeed);

    }

    //�ƶ�����
    private void UpdateMovementInput()
    {
        Quaternion rot = Quaternion.Euler(x: 0, photographer.Yaw, z: 0);
        characterMove.SetMovementInput(rot * Vector3.forward * Input.GetAxis("Vertical")+
                                       rot * Vector3.right * Input.GetAxis("Horizontal"));
        if (Input.GetKeyDown(KeyCode.LeftShift) )
        {
            IsRun = true;          
           
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            IsRun = false;
           
        }


    }

    private void RotateToCameraDirection()
    {
        Vector3 cameraForward = photographer.transform.forward;
        cameraForward.y = transform.forward.y; // ������ɫ��ǰ�����᷽��
        transform.forward = cameraForward.normalized;
    }

    private float CalculateHorizontalSpeed()
    {
        if (isspeak == false)
        {
            if (characterMove.iswalk == 3)
                WalkSpeed = 3;
            if (characterMove.iswalk == 5)
                WalkSpeed = 5;
        }
        else if (isspeak == true) { WalkSpeed = 0; }


        Vector3 velocity = characterMove.CurrentInput*WalkSpeed;
        // ��ȡx���z����ٶȷ���
        float xSpeed = Mathf.Abs(velocity.x);
        float zSpeed = Mathf.Abs(velocity.z);

        // �Ƚϲ�ȡ�����нϴ���ٶ�ֵ
        float maxSpeed = Mathf.Max(xSpeed, zSpeed);

        return maxSpeed;
    }
    private void RotateAccordingToMovement()
    {
        Vector3 cameraForwardFlat = new Vector3(photographer.transform.forward.x, 0f, photographer.transform.forward.z).normalized;
        Vector3 desiredDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) // ǰ��
        {
            desiredDirection = cameraForwardFlat;

            if (Input.GetKey(KeyCode.A)) // WA��ϣ���ת45�㣨��������
            {
                desiredDirection = Quaternion.Euler(0, -45f, 0) * desiredDirection;
            }
            else if (Input.GetKey(KeyCode.D)) // WD��ϣ���ת45�㣨��������
            {
                desiredDirection = Quaternion.Euler(0, 45f, 0) * desiredDirection;
            }
        }
        else if (Input.GetKey(KeyCode.S)) // ����
        {
            desiredDirection = -cameraForwardFlat;

            if (Input.GetKey(KeyCode.A)) // AS��ϣ���ת135�㣨�����ϣ�
            {
                desiredDirection = Quaternion.Euler(0, 45f, 0) * desiredDirection;
            }
            else if (Input.GetKey(KeyCode.D)) // SD��ϣ���ת135�㣨�����ϣ�
            {
                desiredDirection = Quaternion.Euler(0, -45f, 0) * desiredDirection;
            }
        }
        else // ���û��W��S�������£���������AD����ת��
        {
            if (Input.GetKey(KeyCode.A)) // A����ֱ����������
            {
                desiredDirection = Quaternion.Euler(0, -90f, 0) * cameraForwardFlat;
            }
            else if (Input.GetKey(KeyCode.D)) // D����ֱ�����򶫷�
            {
                desiredDirection = Quaternion.Euler(0, 90f, 0) * cameraForwardFlat;
            }
        }

        // ȷ���õ�һ����Ч�ķ�����������ֹ����Ϊ��ʱ��������
        if (desiredDirection != Vector3.zero)
        {
            desiredDirection.Normalize();

            // ��������ֻ����Y�����ת��ֱ������localEulerAngles.y����
            float targetYaw = Mathf.Atan2(desiredDirection.x, desiredDirection.z) * Mathf.Rad2Deg; // ����Ŀ����ת�Ƕ�
            transform.localEulerAngles = new Vector3(0, targetYaw, 0); // ֻ�ı�Y����ת������X��Z
        }
    }
    }

