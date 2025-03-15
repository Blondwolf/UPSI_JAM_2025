using UnityEngine;
using UnityEngine.Events;

public class StartStopButtonListener : MonoBehaviour
{
    //bool running = false;

    public UnityEvent OnStart;
    public UnityEvent OnPause;

    //public void StartStopClicked()
    //{
    //    if (!running)
    //    {
    //        OnStart.Invoke();
    //    }
    //    else
    //    {
    //        OnPause.Invoke();
    //    }

    //    running = !running;
    //}

    public void StartClicked()
    {
        OnStart.Invoke();
    }

    public void StopClicked()
    {
        OnPause.Invoke();
    }
}
