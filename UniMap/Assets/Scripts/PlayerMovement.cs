using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float m_MoveSpeed;
    [SerializeField] private float m_WalkSpeed = 5f;
    [SerializeField] private float m_RunSpeed = 10f;

    private Vector3 m_MoveDirection;
    private Vector3 m_Velocity;

    [SerializeField] private bool m_IsGrounded;
    [SerializeField] private float m_GroundCheckDistance = 0.2f;
    [SerializeField] private LayerMask m_GroundMask;
    [SerializeField] private float m_Gravity = -9.81f;
    
    [SerializeField] private float m_JumpHeight = 1.5f;

    [SerializeField] private Transform m_ShootingPoint;
    [SerializeField] private GameObject m_BulletPrefab;
    [SerializeField] private GameObject m_MagicPrefab;
    [SerializeField] private float m_BulletSpeed = 20f;

    private CharacterController m_Controller;
    private Animator m_Anim;


    private void Start() 
    {
        m_Controller = GetComponent<CharacterController>();
        m_Anim = GetComponent<Animator>();
    }

    private void Update() 
    {
        Move();  

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // StartCoroutine(Attack());
            Attack();
        } 
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            Magic();
        }
    }

    private void Move() 
    {
        m_IsGrounded = Physics.CheckSphere(transform.position, 
                                           m_GroundCheckDistance, 
                                           m_GroundMask);

        if (m_IsGrounded && m_Velocity.y < 0)
        {
            m_Velocity.y = -2f;
        }

        float moveZ = Input.GetAxis("Vertical") * m_WalkSpeed;
        float moveX = Input.GetAxis("Horizontal") * m_WalkSpeed;
        
        // m_MoveDirection = new Vector3(0, 0, moveZ);
        m_MoveDirection = new Vector3(moveX, 0, moveZ);
        m_MoveDirection = transform.TransformDirection(m_MoveDirection);

        if (m_IsGrounded)
        {
            if (m_MoveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
            {
                Walk();
            }
            if (m_MoveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
            {
                Run();
            }
            else if (m_MoveDirection == Vector3.zero)
            {
                Idle();
            }

            m_MoveDirection *= m_MoveSpeed;
  
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }

        m_Controller.Move(m_MoveDirection * Time.deltaTime);
    
        m_Velocity.y += m_Gravity * Time.deltaTime;
        m_Controller.Move(m_Velocity * Time.deltaTime);
    }

    private void Idle()
    {
        m_Anim.SetFloat("Speed", 0f, 0.1f, Time.deltaTime);
    }

    private void Walk()
    {
        m_Anim.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
        m_MoveSpeed = 1;
    }

    private void Run()
    {
        m_Anim.SetFloat("Speed", 1f, 0.1f, Time.deltaTime);
        m_MoveSpeed = m_RunSpeed;
    }

    private void Jump()
    {
        m_Anim.SetTrigger("Jump");
        m_Velocity.y = Mathf.Sqrt(m_JumpHeight * -2 * m_Gravity);
    }

    private void Attack()
    {
        m_Anim.SetTrigger("Attack");
    }

    public void CreateFireball()
    {
        var bullet = Instantiate(m_BulletPrefab, m_ShootingPoint.position, m_ShootingPoint.rotation);
        // bullet.GetComponent<Rigidbody>().velocity =  m_ShootingPoint.forward * m_BulletSpeed;
    }

    private void Magic()
    {
        m_Anim.SetTrigger("Magic");
    }

    public void CreateLargeMagic()
    {
        var magic = Instantiate(m_MagicPrefab, transform.position, transform.rotation);
    }

    // private IEnumerator Attack()
    // {
    //     m_Anim.SetLayerWeight(m_Anim.GetLayerIndex("Attack Layer"), 1);
    //     m_Anim.SetTrigger("Attack");

    //     yield return new WaitForSeconds(2f);
    //     m_Anim.SetLayerWeight(m_Anim.GetLayerIndex("Attack Layer"), 0);
    // }
}
