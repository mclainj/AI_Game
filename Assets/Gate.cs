using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public Camera sceneCam;
    public List<GameObject> gates = new List<GameObject>(); // list of all gates in scene.
    [SerializeField] GameObject shield;

    private GameObject childShield;
    private GameObject childFrame;
    private void Start()
    {
        childShield = transform.GetChild(0).gameObject;
        childFrame = transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        ToggleShieldDoor();
    }


    private void ToggleShieldDoor()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = sceneCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                print("Hit: " + hit.transform.name);

                //if (hit.transform.name == "Gate Frame")
                if (hit.transform.gameObject == childFrame)
                {
                    //this.shield.SetActive(!shield.activeSelf);
                    childShield.SetActive(!childShield.activeSelf);
                }
            }
        }
    }

    public void ActivateClosestGate(Transform t1, Transform t2)
    {
        int minGateIndex = 0;
        float minDist = float.MaxValue;
        //List<float> distances = new List<float>();
        for (int i = 0; i < gates.Count; i++)
        {
            float t1ToGate = 1.5f * Vector3.Distance(t1.position, gates[i].transform.position);
            float gateTot2 = Vector3.Distance(t2.position, gates[i].transform.position);
            float dist = t1ToGate + gateTot2;
            //distances.Add(dist);
            if (dist < minDist)
            {
                minDist = dist;
                minGateIndex = i;
            }
            print("gate dist:" + dist.ToString());
        }
        print("Min gate: " + minGateIndex);
        print(gates[minGateIndex].transform.name);
        gates[minGateIndex].transform.GetChild(0).gameObject.SetActive(true);

    }
}
