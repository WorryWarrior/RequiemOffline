using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMovementScript : MonoBehaviour
{
    Vector2 moveInput;
    Vector3 faceDirection;
    public float speed;
    private Vector2 moveVelocity;
    [HideInInspector] public bool isAllowedToMove = true;

    [HideInInspector] public Rigidbody2D rb;
    public static TopDownMovementScript Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        moveInput.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speed;
    }

    private void FixedUpdate()
    {
        if (isAllowedToMove)
        {
            rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
            rb.transform.eulerAngles = faceDirection;
            FaceDirection();
        }
    }
    private void FaceDirection()
    {
        if (IsMoving()) 
        { 
            if (moveInput.x < 0)
            {
                faceDirection.Set(0, 180, 0);
            } 
            if (moveInput.x > 0)
            {
                faceDirection.Set(0, 0, 0);
            }
        }
    }

    public bool IsMoving()
    {
        if (moveInput == Vector2.zero)
        {
            return false;
        } else
        {
            return true;
        }
    }
    public void DisablePlayerMovement(float _delay)
    {
        StartCoroutine(DisablePlayerMovementCoroutine(_delay));
    }
    private IEnumerator DisablePlayerMovementCoroutine(float delay)
    {
        var initialSpeed = speed;
        speed = 0;
        yield return new WaitForSeconds(delay);
        speed = initialSpeed;
    }
}
