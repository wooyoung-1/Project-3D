using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float runSpeed = 8f;
    public float jumpPower;
    private Vector2 curMoveMentInput;
    private float plusSpeed;
    public LayerMask groundLayerMask;

    [Header("Look")]
    public Transform cameraContainer;
    public float minXlook;
    public float maxXlook;
    private float camCurXRot;
    public float lookSensitiv;
    private Vector2 mouseDelta;

    private Rigidbody _rigidbody;

    public bool canMove = true;
    public bool canLook = true;

    public bool viewSingle = true;

    public Animator cameraView;


    [Header("Manager")]
    private Interaction interaction;
    private Player playerStatus;

    public InventoryUI inventoryUI;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        playerStatus = GetComponent<Player>();
        interaction = GetComponent<Interaction>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canMove)
            Move();

        if (GameManager.isRunning && curMoveMentInput.magnitude > 0.1f)
        {
            playerStatus.UseStamina(10f * Time.deltaTime);

            if (playerStatus.currentStamina <= 0)
            {
                GameManager.isRunning = false;
            }
        }
    }

    private void LateUpdate()
    {
        if (canLook)
            CameraLook();
    }

    void Move()
    {
        float Speed = GameManager.isRunning ? runSpeed : moveSpeed;

        Vector3 moveInput = (transform.forward * curMoveMentInput.y + transform.right * curMoveMentInput.x);
        Vector3 dir = moveInput * (Speed + plusSpeed);
        dir.y = _rigidbody.velocity.y;
        _rigidbody.velocity = dir;
        
    }

    void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitiv;
        camCurXRot = Mathf.Clamp(camCurXRot,minXlook,maxXlook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot,0,0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitiv,0);
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        if (!canMove)
        {
            curMoveMentInput = Vector2.zero;
            return;
        }

        if (context.phase == InputActionPhase.Performed) 
        {
            curMoveMentInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled) 
        {
            curMoveMentInput = Vector2.zero;
        }  
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (!canLook)
        {
            mouseDelta = Vector2.zero;
            return;
        }

        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnView(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Started) return;
        if (viewSingle)
            viewSingle = false;
        else
            viewSingle = true;

        cameraView.SetBool("IsViewSingle", viewSingle);
    }

    public void OnInteraction(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Started) return;
        interaction.HandleInteraction();
    }

    public void OnPrevMoveItem(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Started) return;
        inventoryUI.MoveNextItem();
    }

    public void OnNextMoveItem(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Started) return;
        inventoryUI.MovePreviousItem();
    }

    public void UseItem(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Started) return;
        inventoryUI.UseSelectedItem();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!canMove) return;

        //Debug.Log("잠프");

        if (context.phase == InputActionPhase.Started && (IsGrounded() || IsWall()))
        {
            if(IsWall())
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
                _rigidbody.AddForce(Vector2.up * 60, ForceMode.Impulse);
            }
            else
            {
                _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
            }
                
        }
    }

    public void JumpPad(float power)
    {
        Debug.Log("점프대");

        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);
        _rigidbody.AddForce(Vector3.up * power, ForceMode.Impulse);
    }

    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) +(transform.up * 0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            //Debug.DrawRay(rays[i].origin, rays[i].direction * 0.3f, Color.red);

            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }

    bool IsWall()
    {
        Ray[] wallRays = new Ray[5]
        {
        new Ray(transform.position + (transform.up * 0.1f), transform.forward),
        new Ray(transform.position + (transform.up * 0.3f), transform.forward),
        new Ray(transform.position + (transform.up * 0.5f), transform.forward),
        new Ray(transform.position + (transform.up * 0.8f), transform.forward),
        new Ray(transform.position + (transform.up * 1f), transform.forward)
        };

        for (int i = 0; i < wallRays.Length; i++)
        {
            //Debug.DrawRay(wallRays[i].origin, wallRays[i].direction * 3f, Color.red);
            RaycastHit hit;
            if (Physics.Raycast(wallRays[i], out hit, 2f, groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if (playerStatus.currentStamina > 0)
                GameManager.isRunning = true;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            GameManager.isRunning = false;
        }
    }

    public void ItemSpeedUP()
    {
        Debug.Log("스피드업 사용");
        plusSpeed = 2;
    }

    public void EndItemSpeedUP()
    {
        Debug.Log("스피드업 종료");
        plusSpeed = 0;
    }

    public void ItemSuperJump()
    {
        Debug.Log("스피드업 슈퍼점프");
        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);
        _rigidbody.AddForce(Vector3.up * 200f, ForceMode.Impulse);
    }

    public void ToggleCursor(bool toggle)
    {
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }
}
