using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool _moving = false, _didOnce = false;
    readonly string _moveInputAxis = "Vertical";
    readonly string _turnInputAxis = "Horizontal";

    public float rotationRate = 360f;
    public float moveSpeed = 1f;

    ObjectPool _dustParticlePool;
    public Transform dustEmitter;

    void Awake()
    {
        _dustParticlePool = GameObject.FindGameObjectWithTag("DustParticlePool").GetComponent<ObjectPool>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.playerActionInprogress)
        {
            return;
        }

        float moveAxis = Input.GetAxis(_moveInputAxis);
        float turnAxis = Input.GetAxis(_turnInputAxis);

        ApplyInput(moveAxis, turnAxis);
        _moving = moveAxis != 0;

        if (_moving && !_didOnce)
        {
            StartCoroutine(SpawnParticleTrail());
            _didOnce = true;
        }

        if (!_moving)
        {
            _didOnce = false;
        }
    }

    private void ApplyInput(float moveInput, float turnInput)
    {
        Move(moveInput);
        Turn(turnInput);
    }

    private void Move(float input)
    {
        transform.Translate(Vector3.forward * input * moveSpeed * Time.deltaTime);
    }

    private void Turn(float input)
    {
        transform.Rotate(0, input * rotationRate * Time.deltaTime, 0);
    }

    IEnumerator SpawnParticleTrail()
    {
        yield return new WaitForSeconds(.1f);

        GameObject particle = _dustParticlePool.GetPooledObjectByTag("DustParticleEffect");

        if (particle != null)
        {
            particle.transform.position = dustEmitter.position;
            particle.SetActive(true);
        }

        if (_moving)
        {
            StartCoroutine(SpawnParticleTrail());
        }

        yield return null;

    }
}
