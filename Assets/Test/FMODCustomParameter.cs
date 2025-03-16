using FMODUnity;
using UnityEngine;

public class FMODCustomParameter : MonoBehaviour
{
    public FMODUnity.StudioEventEmitter eventEmitter;

    ParamRef param;

    private void Start()
    {
        if(eventEmitter.Params.Length > 0)
            param = eventEmitter.Params[0];
    }

    public void SetFMODParameter(int value)
    {
        if (param == null)
            return;

        Debug.Log(value);
        //eventEmitter.SetParameter(param.Name, value);
        param.Value = value;
    }
}
