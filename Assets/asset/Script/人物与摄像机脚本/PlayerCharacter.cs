using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class PlayerCharacter : MonoBehaviour
{
    private Animator animator;
    private float rotationDegrees = 45f; // 按键旋转的基本角度
    private float additionalRotationForBackward = 180f; // 向后时额外旋转的角度
    public float WalkSpeed;
    public bool isspeak = false;

    [Header("健康值相关")]
    [SerializeField]
    private Slider HpSlider;
    public float HP = 100;
    public TMP_Text HpTxt;

    [Header("体力相关")]
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
    private Transform followingTarget;//摄像机跟随点位
                                      // Start is called before the first frame update


    private void ChangeTheSP()
    {
        SP -= Des; // 先执行减法操作
        SP = Mathf.Clamp(SP, 0f, 100f); // 立即约束SP的值在0到100之间
        SpSlider.value = SP; // 最后更新滑块的值
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
        HpSlider.value = HP; // 最后更新滑块的值
        HpTxt.text = "HP:" + HP.ToString("0.0") + "/100";
    }

    private void Awake()
    {
        characterMove = GetComponent<CharacterMoveMent>();

        photographer.InitCamera(followingTarget);//初始化摄像机跟随

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
            // 根据移动速度调整角色朝向（仅水平方向）
            RotateAccordingToMovement();
        }

        //// 保持角色面向摄像机的水平方向
        //RotateToCameraDirection();


        float horizontalSpeed = CalculateHorizontalSpeed();
        animator.SetFloat("speed", horizontalSpeed);

    }

    //移动函数
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
        cameraForward.y = transform.forward.y; // 保留角色当前的纵轴方向
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
        // 获取x轴和z轴的速度分量
        float xSpeed = Mathf.Abs(velocity.x);
        float zSpeed = Mathf.Abs(velocity.z);

        // 比较并取两者中较大的速度值
        float maxSpeed = Mathf.Max(xSpeed, zSpeed);

        return maxSpeed;
    }
    private void RotateAccordingToMovement()
    {
        Vector3 cameraForwardFlat = new Vector3(photographer.transform.forward.x, 0f, photographer.transform.forward.z).normalized;
        Vector3 desiredDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) // 前进
        {
            desiredDirection = cameraForwardFlat;

            if (Input.GetKey(KeyCode.A)) // WA组合，左转45°（朝西北）
            {
                desiredDirection = Quaternion.Euler(0, -45f, 0) * desiredDirection;
            }
            else if (Input.GetKey(KeyCode.D)) // WD组合，右转45°（朝东北）
            {
                desiredDirection = Quaternion.Euler(0, 45f, 0) * desiredDirection;
            }
        }
        else if (Input.GetKey(KeyCode.S)) // 后退
        {
            desiredDirection = -cameraForwardFlat;

            if (Input.GetKey(KeyCode.A)) // AS组合，左转135°（朝西南）
            {
                desiredDirection = Quaternion.Euler(0, 45f, 0) * desiredDirection;
            }
            else if (Input.GetKey(KeyCode.D)) // SD组合，右转135°（朝东南）
            {
                desiredDirection = Quaternion.Euler(0, -45f, 0) * desiredDirection;
            }
        }
        else // 如果没有W或S键被按下，单独处理AD键的转向
        {
            if (Input.GetKey(KeyCode.A)) // A键，直接面向西方
            {
                desiredDirection = Quaternion.Euler(0, -90f, 0) * cameraForwardFlat;
            }
            else if (Input.GetKey(KeyCode.D)) // D键，直接面向东方
            {
                desiredDirection = Quaternion.Euler(0, 90f, 0) * cameraForwardFlat;
            }
        }

        // 确保得到一个有效的方向向量（防止向量为零时发生错误）
        if (desiredDirection != Vector3.zero)
        {
            desiredDirection.Normalize();

            // 由于我们只关心Y轴的旋转，直接设置localEulerAngles.y即可
            float targetYaw = Mathf.Atan2(desiredDirection.x, desiredDirection.z) * Mathf.Rad2Deg; // 计算目标旋转角度
            transform.localEulerAngles = new Vector3(0, targetYaw, 0); // 只改变Y轴旋转，忽略X和Z
        }
    }
    }

