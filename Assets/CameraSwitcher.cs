using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSwitcher : MonoBehaviour
{
    private CinemachineVirtualCamera vCAm;
    public int priorityIncrement;
    [SerializeField]
    private Canvas zoomedOutCanvas;
    [SerializeField]
    private Canvas zoomedInCanvas;

    void Start()
    {
        vCAm = GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            AimMode();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            NormalMode();
        }
    }

    private void AimMode()
    {
        vCAm.Priority += priorityIncrement;
        zoomedInCanvas.enabled = true;
        zoomedOutCanvas.enabled = false;
    }

    private void NormalMode()
    {
        vCAm.Priority -= priorityIncrement; 
        zoomedInCanvas.enabled = false;
        zoomedOutCanvas.enabled = true;
    }
}
