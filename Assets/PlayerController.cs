using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PlayerController : MonoBehaviour
{

    public Camera sceneCam;
    public NavMeshAgent agent;
    [SerializeField] GameObject target;
    // Update is called once per frame

    private void Start()
    {
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
    }







}
