using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Operating : State {

    private Transform operatingPoint;
    public GameObject head, body;
    public State sick;
    private NavMeshAgent agent;
    public GameManager gameManager;

    private void OnEnable() {
        gameManager = Camera.main.GetComponent<GameManager>();
        OutOfBed();
    }

    private void Start() {
        operatingPoint = gameManager.operatingPoint;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update() {
        if (this.GetComponent<NavMeshAgent>() != null) {
            this.GetComponent<NavMeshAgent>().SetDestination(operatingPoint.position);
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

    private void Invisible() {
        this.GetComponent<Rigidbody>().isKinematic = true;
        this.GetComponent<BoxCollider>().enabled = false;
        head.SetActive(false);
        body.SetActive(false);
    }

    public override void CheckExit() {
        if (Mathf.Round(Vector3.Distance(agent.transform.position, operatingPoint.position)) <= agent.stoppingDistance) {
            Invisible();
        }

        //if(paciente_termiandaAccion)
        //stateMachine.ChangeState(sick);
    }
}
