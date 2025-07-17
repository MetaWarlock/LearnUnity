using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    [SerializeField] private string TagToInteract;
    [SerializeField] private bool OnOrOff, OnOrOff2, OneActuation;
    [SerializeField] private GameObject[] TargetObject;
    [SerializeField] private GameObject[] TargetObject2;

    private bool canAct;

    void Start()
    {
        canAct = true;

        if (TargetObject.Length > 0)
            SetActiveObjects(TargetObject, !OnOrOff);

        if (TargetObject2.Length > 0)
            SetActiveObjects(TargetObject2, !OnOrOff2);
    }

    void SetActiveObjects(GameObject[] massive, bool onoff)
    {
        for(int i = 0; i < massive.Length; i++)
        {
            massive[i].SetActive(onoff);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        Debug.Log("Trigger detect: " + col.gameObject.tag);
        if (col.gameObject.CompareTag(TagToInteract) && canAct)
        {
            if (TargetObject.Length > 0)
                SetActiveObjects(TargetObject, OnOrOff);

            if (TargetObject2.Length > 0)
                SetActiveObjects(TargetObject2, OnOrOff2);

            if (OneActuation)
                canAct = false;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log("controller hit: " + hit.gameObject.tag);
        if (hit.gameObject.CompareTag(TagToInteract) && canAct)
        {
            if (TargetObject.Length > 0)
                SetActiveObjects(TargetObject, OnOrOff);

            if (TargetObject2.Length > 0)
                SetActiveObjects(TargetObject2, OnOrOff2);

            if (OneActuation)
                canAct = false;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("trigger collision exit: " + other.gameObject.tag);
        if (other.gameObject.CompareTag(TagToInteract) && canAct)
        {
            if (TargetObject.Length > 0)
                SetActiveObjects(TargetObject, !OnOrOff);

            if (TargetObject2.Length > 0)
                SetActiveObjects(TargetObject2, !OnOrOff2);
        }
    }
}
