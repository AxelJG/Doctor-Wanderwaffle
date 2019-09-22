using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Sick : State
{
    public State attended;
    public GameObject head, body;
    private NavMeshAgent agent;
    private Transform assignedBed;
    Animator _animator;

    private void OnEnable() {
        Visible();
    }
    
    // Start is called before the first frame updaate
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        CheckFreeBed();

        _animator.SetBool("isWalking", true);
    }

    // Update is called once per frame
    void Update()
    {
        if(assignedBed != null) {
            agent.SetDestination(assignedBed.position);
        }
    }

    private void CheckFreeBed() {
        int idList = 0;

        for (int i = 0; i < GameManager.Instance.freeBeds.Count; i++) {
            if(GameManager.Instance.freeBeds[i] == false) {
                idList = i;
                break;
            }
        }

        assignedBed = GameManager.Instance.bedsPoints[idList];
        GameManager.Instance.freeBeds[idList] = true;
        stateMachine.idBedRef = idList;
    }

    private void Visible() {
        this.GetComponent<BoxCollider>().enabled = true;
        this.GetComponent<Rigidbody>().isKinematic = false;
        head.SetActive(true);
        body.SetActive(true);
    }

    public override void CheckExit() {
        if (Mathf.Round(Vector3.Distance(agent.transform.position, assignedBed.position)) <= agent.stoppingDistance) {
            stateMachine.ChangeState(attended);
        }
    }
}
