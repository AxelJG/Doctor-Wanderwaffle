using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cured : State {

    private Transform exitPoint;
    private NavMeshAgent agent;
    Animator _animator;

    public GameManager gameManager;

    private void OnEnable() {
        gameManager = Camera.main.GetComponent<GameManager>();
        OutOfBed();
    }

    private void Start() {
        exitPoint = gameManager.exitPoint;
        _animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        _animator.SetBool("isWalking", true);
    }

    // Update is called once per frame
    void Update()
    {
        if(this.GetComponent<NavMeshAgent>() != null) {
            this.GetComponent<NavMeshAgent>().SetDestination(exitPoint.position);
        }
    }

    private void OutOfBed() {
        this.transform.position = gameManager.bedsPoints[stateMachine.idBedRef].position;
        this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        //Enabled components
        this.GetComponent<BoxCollider>().enabled = true;
        this.GetComponent<Rigidbody>().isKinematic = false;
        this.GetComponent<NavMeshAgent>().enabled = true;
    }

    public override void CheckExit() {
        if (Mathf.Round(Vector3.Distance(agent.transform.position, exitPoint.position)) <= agent.stoppingDistance) {
            Destroy(this.gameObject);
        }
    }
}
