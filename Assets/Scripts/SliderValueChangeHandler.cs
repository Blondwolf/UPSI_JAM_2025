using FMODUnity;
using UnityEngine;

public class SliderValueChangeHandler : MonoBehaviour
{
    public StudioGlobalParameterTrigger globalTrigger;

    public void SetGlobalParameter(float value)
    {
        globalTrigger.Value = value;
        globalTrigger.TriggerParameters();

        //if (globalTrigger != null)
        //{
        //    RuntimeManager.StudioSystem.setParameterByName(parameterName, value);
        //    globalTrigger.TriggerParameters(); // Déclenche le trigger si nécessaire
        //}
    }
}
