using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("참조용")]
    public P_Equipment PEquip;
    public P_InteractionFinder PInterFind;
    public P_Stat PStat;
    public PlayerInteraction PInteract;
    public P_AniHandler pAnimationHandler;
    [Header("회전")]
    [SerializeField] float rotateSpeed = 10f;
    Camera cam;

    [Header("이동")]
    [SerializeField] float curSpeed;
    [SerializeField] float speedChangeValue;
    [SerializeField] float MoveAngle;
    Vector2 curMovementInput;
    CharacterController charControl;

    private void Awake()
    {
        PEquip = GetComponent<P_Equipment>();
        PInterFind = GetComponent<P_InteractionFinder>();
        PStat = GetComponent<P_Stat>();
        PInteract = GetComponent<PlayerInteraction>();
        pAnimationHandler = GetComponent<P_AniHandler>();
    }

    private void Start()
    {
        cam = Camera.main;
        charControl = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Look();        
    }

    private void FixedUpdate()
    {
        Move();   
    }

    void Look()
    {
        //가상의 Plane을 만들어 레이캐스트로 충돌 후에 좌표 구하기.
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);

        if(plane.Raycast(ray,out float distance))
        {
            Vector3 targetPoint = ray.GetPoint(distance);
            targetPoint.y = transform.position.y;

            Vector3 direction = targetPoint - transform.position;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), rotateSpeed * Time.deltaTime);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if(context.canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }

    void Move()
    {
        //입력 값이 들어오면 현재 스피드를 천천히 상승 후에 velocity에 반영.
        float targetSpeed = curMovementInput.magnitude;
        curSpeed = Mathf.Lerp(curSpeed, targetSpeed, Time.deltaTime * speedChangeValue);
        Vector3 direction = Vector3.forward * curMovementInput.y + Vector3.right * curMovementInput.x;
        direction *= PStat.Speed;
        direction.y = 0;

        float angle = Vector3.Angle(transform.forward, direction);
        Vector3 crossProduct = Vector3.Cross(transform.forward, direction);
        if (crossProduct.y > 0)
        {
            MoveAngle = angle;
        }
        else if (crossProduct.y < 0)
        {
            MoveAngle = -angle;
        }
        else
        {
            float dot = Vector3.Dot(transform.forward, direction);
            if (dot > 0)
            {
                MoveAngle = 0;
            }
            else if (dot < 0)
            {
                MoveAngle = 180;
            }
        }
        pAnimationHandler.ChangeMoveValue(curSpeed);
        pAnimationHandler.ChangeMoveAngle(MoveAngle);
        charControl.Move(direction * Time.deltaTime);
    }
}