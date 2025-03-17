using UnityEngine;
using UnityEngine.InputSystem;

public class P_CameraController : MonoBehaviour
{
    [Header("타깃 추적")]
    [SerializeField] Transform target;
    [SerializeField] float followingSpeed = 20f;
    [SerializeField] float rotationSpeed = 5f;
    [SerializeField] float minDiffZ;
    [SerializeField] float maxDiffZ = 5.5f;
    public bool IsFollowing;
    Camera cam;

    [Header("카메라 시점 축소/확대")]
    [SerializeField] float scrollSpeed;
    [SerializeField] float moveSpeed;
    [SerializeField] float curAngle = 0.5f;
    [SerializeField] float maxY;
    [SerializeField] float minY;
    [SerializeField] AnimationCurve curve;
    float mouseScroll;

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        cam = Camera.main;
        IsFollowing = true;
    }

    private void FixedUpdate()
    {
        CameraScrollMove();
        if (IsFollowing)
        {
            TargetFollowing();
        }
    }

    void TargetFollowing()
    {
        Vector3 targetPos = new Vector3(target.position.x, cam.transform.position.y, target.position.z - 5.5f);
        //Vector3 targetPos = new Vector3(target.position.x, cam.transform.position.y, target.position.z - (minDiffZ + (maxDiffZ - minDiffZ) * curve.Evaluate(curAngle)));
        cam.transform.position = Vector3.Lerp(cam.transform.position, targetPos, followingSpeed * Time.deltaTime);
        Quaternion targetrotation = Quaternion.LookRotation(target.position - cam.transform.position);
        targetrotation.y = 0;
        targetrotation.z = 0;
        cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, targetrotation, rotationSpeed * Time.fixedDeltaTime);
    }

    public void OnScroll(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            mouseScroll = Mathf.Clamp(-context.ReadValue<float>(), -1, 1);
        }
        else if (context.canceled)
        {
            mouseScroll = 0;
        }
        curAngle = Mathf.Clamp(curAngle + mouseScroll * scrollSpeed, 0f, 1f);
    }

    void CameraScrollMove()
    {
        Vector3 camPos = cam.transform.position;
        camPos.y = minY + (maxY - minY) * curAngle;
        cam.transform.position = Vector3.Lerp(cam.transform.position, camPos, moveSpeed * Time.fixedDeltaTime);
    }
}