using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Photographer : MonoBehaviour
{
    public float Pitch { get; private set; }
    public float Yaw { get; private set; }

    public float mouseSensitivity = 5;

    public float camerRotatingSpeed = 80;
    public float cameraYspeed = 5;
    private Transform _target;
    private Transform _camera;
    [SerializeField]
    private AnimationCurve armLengthCurve;
    // Start is called before the first frame update

    private void Awake()
    {
        _camera = transform.GetChild(0);
    }
    void Start()
    {
        
    }


    public void InitCamera(Transform target)
    {
        _target = target;
        transform.position = target.position;

    }
    // Update is called once per frame
    void Update()
    {
        UpdateRotation();
        UpdatePosition();
        UpdateArmLength();
    }

    private void UpdateRotation()
    {
        Yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        Yaw += Input.GetAxis("Camera Rate X") * camerRotatingSpeed * Time.deltaTime;
        Pitch += Input.GetAxis("Mouse Y") * mouseSensitivity;
        Pitch += Input.GetAxis("Camera Rate Y") * camerRotatingSpeed * Time.deltaTime;
        Pitch = Mathf.Clamp(value: Pitch, min: 0, max: 45);

        transform.rotation = Quaternion.Euler(x: Pitch, y: Yaw, z: 0);
    }

    private void UpdatePosition()
    {
        Vector3 position = _target.position; 
        float newY = Mathf.Lerp(a: transform.position.y, b: position.y, t: Time.deltaTime * cameraYspeed);
        transform.position = new Vector3(position.x,newY,position.z);
    }

    private void UpdateArmLength()
    {
        _camera.localPosition = new Vector3 (x:0,y:0,z:armLengthCurve.Evaluate(Pitch)* -1);
    }
}
