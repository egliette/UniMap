using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float m_AttackRange = 1f;
    [SerializeField] private float m_AttackTime = 2f;
    [SerializeField] private int m_AttackDamage = 10;

    [SerializeField] private float m_Speed = 10f;

    private Animator m_Anim;
    private Transform m_Player;
    private LayerMask m_PlayerMask;
    private NavMeshAgent m_NavMeshAgent;
    private bool m_PlayerInAttackRange;
    private PlayerHealth m_playerHealth;


    private void Awake()
    {
        m_PlayerMask = LayerMask.GetMask("Player");
        m_Player = GameObject.Find("Player").transform;
        m_playerHealth = m_Player.GetComponent<PlayerHealth>();
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        m_Anim = GetComponent<Animator>();
    }

    private void Update() 
    {
        Chasing();
    }

    private void Chasing()
    {   
        m_PlayerInAttackRange = Physics.CheckSphere(transform.position, m_AttackRange, m_PlayerMask);
        
        if (m_PlayerInAttackRange && !m_Anim.GetBool("Attacking"))
            Attacking();
        else if (!m_Anim.GetBool("Attacking"))
            Move(m_Speed);     
            m_NavMeshAgent.destination = m_Player.position;
    }

    void Move(float speed)
    {
        m_NavMeshAgent.isStopped = false;
        m_NavMeshAgent.speed = speed;
    }

    void Stop()
    {
        m_NavMeshAgent.isStopped = true;
        m_NavMeshAgent.speed = 0;
        m_NavMeshAgent.velocity = Vector3.zero;
    }

    private void Attacking()
    {  
        Stop();
        m_Anim.SetBool("Attacking", true);
        StartCoroutine(ResetAttack());
    }

    public void DealDamage()
    {
        if (Physics.CheckSphere(transform.position, m_AttackRange, m_PlayerMask))
        {
            m_playerHealth.TakeDamage(m_AttackDamage);
        }
    }

    private IEnumerator ResetAttack()
    {   
        yield return new WaitForSeconds(m_AttackTime);
        FaceTarget(); 
        m_Anim.SetBool("Attacking", false);
    }

    private void FaceTarget()
    {
        Vector3 direction = (m_Player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = lookRotation;
    }


}
