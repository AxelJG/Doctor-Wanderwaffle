using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cured : State {

    private Transform exitPoint;
    private NavMeshAgent agent;

    private void OnEnable() {
        OutOfBed();
    }

    private void Start() {
        exitPoint = GameManager.Instance.exitPoint;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(this.GetComponent<NavMeshAgent>() != null) {
            this.GetComponent<NavMeshAgent>().SetDestination(exitPoint.position);
        }
    }

    private void OutOfBed() {
        this.transform.position = GameManager.Instance.bedsPoints[stateMachine.idBedRef].position;
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
