using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Xray : State
{
    private Transform xRayPoint;
    public GameObject head, body;
    public State sick;
    private NavMeshAgent agent;

    private void OnEnable() {
        OutOfBed();
    }

    private void Start() {
        xRayPoint = GameManager.Instance.xRayPoint;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update() {
        if (this.GetComponent<NavMeshAgent>() != null) {
            this.GetComponent<NavMeshAgent>().SetDestination(xRayPoint.position);
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

    private void Invisible() {
        this.GetComponent<Rigidbody>().isKinematic = true;
        this.GetComponent<BoxCollider>().enabled = false;
        head.SetActive(false);
        body.SetActive(false);
    }

    public override void CheckExit() {
        if (Mathf.Round(Vector3.Distance(agent.transform.position, xRayPoint.position)) <= agent.stoppingDistance) {
            Invisible();
        }

        //if(paciente_termiandaAccion)
        //stateMachine.ChangeState(sick);
    }
}
