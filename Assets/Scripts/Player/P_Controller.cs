using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class PlayerController : MonoBehaviour
{
    [Header("참조용")]
    public P_Equipment PEquip;
    public P_InteractionFinder PInterFind;
    public P_Stat PStat;
    public PlayerInteraction PInteract;
    public P_AniHandler pAnimationHandler;
    public ProjectileHandler projectile;
    public P_Action PlayerAction;
    [Header("회전")]
    [SerializeField] float rotateSpeed = 10f;
    Vector3 direction;
    Camera cam;

    [Header("이동")]
    [SerializeField] float curSpeed;
    [SerializeField] float speedChangeValue;
    [SerializeField] float MoveAngle;
    public float CurSpeed { get { return curSpeed; } }
    public float SpeedChangeValue { get { return speedChangeValue; } }
    Vector2 curMovementInput;
    CharacterController charControl;
    public CharacterController CharacterController { get { return charControl; } }
    public Vector2 CurMovementInput { get { return curMovementInput; } }

    private void Awake()
    {
        PEquip = GetComponent<P_Equipment>();
        PInterFind = GetComponent<P_InteractionFinder>();
        PStat = GetComponent<P_Stat>();
        PInteract = GetComponent<PlayerInteraction>();
        pAnimationHandler = GetComponent<P_AniHandler>();
        projectile = GetComponent<ProjectileHandler>();
        PlayerAction = GetComponent<P_Action>();
    }

    private void Start()
    {
        cam = Camera.main;
        charControl = GetComponent<CharacterController>();

        Managers.Game.Player = this;
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
        if (pAnimationHandler.isAnimationing || PlayerAction.IsChasing)
            return;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);

        if (plane.Raycast(ray, out float distance))
        {
            Vector3 targetPoint = ray.GetPoint(distance);
            targetPoint.y = transform.position.y;

            direction = targetPoint - transform.position;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), rotateSpeed * Time.deltaTime);
        }
    }

    #region 플레이어 인풋 받기
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
            PlayerAction.CancelBuilding();
        }
        else if (context.canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }

    public void onInteract(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            PInteract.Interact();
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started) 
        {
            StartCoroutine(Attack());
        }
    }

    public void OnBuild(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            (Managers.UI.CurrentSceneUI as UI_Build).Button_Spin();
        }
    }

    public void OnPressedOne(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Managers.Item.Use(1);
        }
    }

    public void OnPressedTwo(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Managers.Item.Use(2);
        }
    }

    public void OnPressedThree(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Managers.Item.Use(3);
        }
    }
    public void OnPressedFour(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Managers.Item.Use(4);
        }
    }
    public void OnPressedFive(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Managers.Item.Use(5);
        }
    }
    public void OnPressedSix(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Managers.Item.Use(6);
        }
    }
    public void OnPressedSeven(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Managers.Item.Use(7);
        }
    }

    public void OnPressedEight(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Managers.Item.Use(8);
        }
    }

    public void OnPressedNine(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Managers.Item.Use(9);
        }
    }
    #endregion
    void Move()
    {
        if (pAnimationHandler.isAnimationing)
        {
            curSpeed = 0; // 속도 0으로 설정
            pAnimationHandler.ChangeMoveValue(curSpeed); // 애니메이션 속도 업데이트
            return;
        }
        //입력 값이 들어오면 현재 스피드를 천천히 상승 후에 velocity에 반영.
        float targetSpeed = curMovementInput.magnitude;
        curSpeed = Mathf.Lerp(curSpeed, targetSpeed, Time.fixedDeltaTime * speedChangeValue);
        if ((targetSpeed == 0 || curSpeed < 0.02f))
        {
            curSpeed = 0;
        }
        Vector3 direction = Vector3.forward * curMovementInput.y + Vector3.right * curMovementInput.x;
        direction *= PStat.curSpeed;
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
        charControl.Move(direction * Time.fixedDeltaTime);
    }

    IEnumerator Attack()
    {
        yield return new WaitForEndOfFrame();
        if (IsPointerOverUI())
            yield break;
        if (PEquip.curEquipmentType == WeaponType.Melee)
        {
             pAnimationHandler.PlayAnim("MeleeAttack");
        }
        else if (PEquip.curEquipmentType == WeaponType.Projectile)
        {
            pAnimationHandler.PlayAnim("Throw");
        }
        else
            yield break;
    }

    private bool IsPointerOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    public void ForceMovePlayer(Vector3 pos)
    {
        PlayerAction.ForceMove(pos);
    }
}