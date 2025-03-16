using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InstrumentChooser : MonoBehaviour
{
    public List<InstrumentController> instruments = new List<InstrumentController>();

    InstrumentController currentInstrument;
    int currentIndex;

    private void Start()
    {
        instruments = GetComponentsInChildren<InstrumentController>().ToList();
        currentInstrument = instruments[0];
        currentIndex = 0;
    }

    public void StartRunning()
    {
        foreach (InstrumentController controller in instruments)
        {
            controller.StartRunning();
            controller.GetComponentInChildren<WheelRotateController>().StartRotating();
            controller.GetComponentInChildren<ArcDrawer>().StartRunning();
        }
    }

    public void StopRunning()
    {
        foreach(InstrumentController controller in instruments)
        {
            controller.StopRunning();
            controller.GetComponentInChildren<WheelRotateController>().StopRotating();
            controller.GetComponentInChildren<ArcDrawer>().StopRunning();
        }
    }

    public void NextInstrument()
    {
        currentIndex++;

        // limit to max
        if(currentIndex >= instruments.Count)       
            currentIndex = instruments.Count - 1;
        
        currentInstrument.GetComponentInChildren<ArcDrawer>().selected = false;
        currentInstrument.selected = false;
        currentInstrument = instruments[currentIndex];
        currentInstrument.GetComponentInChildren<ArcDrawer>().selected = true;
        currentInstrument.selected = true;
    }

    public void PreviousInstrument()
    {
        currentIndex--;

        // limit to min
        if (currentIndex < 0)
            currentIndex = 0;

        currentInstrument.GetComponentInChildren<ArcDrawer>().selected = false;
        currentInstrument.selected = false;
        currentInstrument = instruments[currentIndex];
        currentInstrument.GetComponentInChildren<ArcDrawer>().selected = true;
        currentInstrument.selected = true;
    }
}
