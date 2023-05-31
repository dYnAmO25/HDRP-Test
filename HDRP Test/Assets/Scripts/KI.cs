using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class KI : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject player;
    private Animator animator;
    [SerializeField] Renderer ren;
    [SerializeField] AudioSource audioLeft;
    [SerializeField] AudioSource audioRight;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //agent.destination = player.transform.position;

        if (ren.isVisible)
        {
            animator.speed = 0;
            //agent.isStopped = true;
            agent.destination = transform.position;
        }
        else
        {
            animator.speed = 2;
            //agent.isStopped = false;
            agent.destination = player.transform.position;
            Attack();
        }
    }

    private void Attack()
    {
        RaycastHit hit;

        Physics.Raycast(new Vector3(transform.position.x, 1, transform.position.z), transform.forward, out hit, 1f);

        if (hit.collider != null)
        {
            if (hit.collider.tag == "Player")
            {
                Die();
            }
        }
    }

    private void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LeftStep()
    {
        audioLeft.Play();
    }

    private void RightStep()
    {
        audioRight.Play();
    }
}
