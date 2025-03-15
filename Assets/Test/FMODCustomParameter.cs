using FMODUnity;
using UnityEngine;

public class FMODCustomParameter : MonoBehaviour
{
    public FMODUnity.StudioEventEmitter eventEmitter;

    ParamRef param;

    private void Start()
    {
        param = eventEmitter.Params[0];
    }

    public void SetFMODParameter(int value)
    {
        Debug.Log(value);
        //eventEmitter.SetParameter(param.Name, value);
        param.Value = value;
    }
}
