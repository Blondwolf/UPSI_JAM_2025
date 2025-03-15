using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public CinemachineBrain cinemachine;
    public CinemachineVirtualCamera globalCamera;
    
    public List<CinemachineVirtualCamera> camerasInstruments = new List<CinemachineVirtualCamera>();
    int currentIndex = 0;

    public void Zoom()
    {
        globalCamera.gameObject.SetActive(false);
    }

    public void Back()
    {
        globalCamera.gameObject.SetActive(true);
    }

    public void NextInstrument()
    {
        // tODO limit

        currentIndex++;
        camerasInstruments[currentIndex].gameObject.SetActive(true);
    }

    public void PreviousInstrument()
    {
        camerasInstruments[currentIndex].gameObject.SetActive(false);
        currentIndex--;
    }
}
