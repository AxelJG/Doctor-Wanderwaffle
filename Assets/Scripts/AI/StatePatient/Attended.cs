using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attended : State
{
    public State cured, operating, xray, treatment;

    private void OnEnable() {
        AttendedBedPosition();
    }

    private void AttendedBedPosition() {
        //Disable components
        this.GetComponent<NavMeshAgent>().enabled = false;
        this.GetComponent<Rigidbody>().isKinematic = true;
        this.GetComponent<BoxCollider>().enabled = false;

        //Adapt position & rotation
        this.transform.rotation = Quaternion.Euler(-104f, 90f, 185f);
        this.transform.position = GameManager.Instance.bedsAttendedPoints[stateMachine.idBedRef].position;
    }

    public override void CheckExit() {
        //TODO con el script de paciente

        //if(paciente_curado)
        //stateMachine.ChangeState(cured);

        //if(paciente_rayosX)
        //stateMachine.ChangeState(xray);

        //if(paciente_operacion)
        //stateMachine.ChangeState(operating);

        //if(paciente_tratamiento)
        //stateMachine.ChangeState(treatment);
    }
}
