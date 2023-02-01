using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private CharacterController movController;
    public float baseSpeed;
    public float runSpeed;
    public Vector2 turn;
    public float turnSensitivity;
    private Animator animator;
    private PlayerLogic player;

    void Start()
    {
        movController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        player = GetComponent<PlayerLogic>();
    }

    public void FreezeMovement()
    {
        animator.SetFloat("MotionMagnitude", 0);
    }

    void Update()
    {
        if (!player.isActive) return;

        turn.x += Input.GetAxis("Mouse X") * turnSensitivity;
        turn.y += Input.GetAxis("Mouse Y") * turnSensitivity;

        transform.localRotation = Quaternion.Euler(Mathf.Clamp(-turn.y, -20, 20), turn.x, 0);

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Jump");
        }

        Vector3 movement = (transform.right * horizontal + transform.forward * vertical).normalized *
            (Input.GetKey(KeyCode.LeftShift) ? runSpeed : baseSpeed) * Time.deltaTime;

        movement.y = 0;

        animator.SetFloat("MotionMagnitude", movController.velocity.magnitude);

        movController.Move(movement);

    }
}
