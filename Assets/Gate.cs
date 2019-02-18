using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public Camera sceneCam;
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
}
