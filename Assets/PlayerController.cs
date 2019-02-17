using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{

    public Camera sceneCam;
    public NavMeshAgent agent;
    [SerializeField] GameObject target;

    Rigidbody rigidbody;

    [Header("General")]
    [Tooltip("In ms^-1")] [SerializeField] float xControlSpeed = 40f;
    [Tooltip("In ms^-1")] [SerializeField] float zControlSpeed = 40f;
    float xThrow, zThrow;

    // Update is called once per frame

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        //agent.SetDestination(target.transform.position);
    }
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = sceneCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }
        ProcessInput();
    }

    private void ProcessInput()
    {
        ProcessTranslation();
        // todo fix collision issue with enemy that causes player drift
    }

    private void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffset = xThrow * xControlSpeed * Time.deltaTime;
        float xPos = transform.position.x + xOffset;

        zThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float zOffset = zThrow * zControlSpeed * Time.deltaTime;
        float zPos = transform.position.z + zOffset;

        transform.position = new Vector3(xPos, transform.position.y, zPos);
    }
}
