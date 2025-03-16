using Cinemachine;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
        currentIndex++;

        // Limit to last
        if(currentIndex >= camerasInstruments.Count)
        {
            currentIndex = camerasInstruments.Count - 1;
        }

        camerasInstruments[currentIndex].gameObject.SetActive(true);
    }

    public void PreviousInstrument()
    {
        if (currentIndex <= 0)
        {
            currentIndex = 0;
            return;
        }

        camerasInstruments[currentIndex].gameObject.SetActive(false);
        currentIndex--;
    }
}
