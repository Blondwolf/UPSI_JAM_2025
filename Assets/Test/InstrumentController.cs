using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InstrumentController : MonoBehaviour
{
    public int[] melody = new int[32];
    public int bpm;

    private float timePerStep;

    private int currentStep = 0;
    private float timer = 0;

    public InputActionReference pressArcAction;

    public UnityEvent<int> OnEventStart;
    public UnityEvent OnEventStop;

    private bool drawing = false;

    void Start()
    {
        timePerStep = 60f / bpm;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timePerStep)
        {
            timer -= timePerStep;
            RecordStep();
            PlayStep();
            currentStep = (currentStep + 1) % melody.Length;
        }
    }

    void OnEnable()
    {
        pressArcAction.action.started += (ctx) => StartDrawing();
        pressArcAction.action.canceled += (ctx) => StopDrawing();
        pressArcAction.action.Enable();
    }

    void OnDisable()
    {
        //pressArcAction.action.started -= StartDrawing;
        //pressArcAction.action.canceled -= StopDrawing;
        pressArcAction.action.Disable();
    }

    void StartDrawing()
    {
        drawing = true;
    }

    void StopDrawing()
    {
        drawing = false;
    }

    void RecordStep()
    {
        if (drawing)
        {
            melody[currentStep] = 1;
        }
    }

    void PlayStep()
    {
        if (melody[currentStep] != 0)
        {
            OnEventStart?.Invoke(melody[currentStep]);
        }
        else
        {
            OnEventStop?.Invoke();
        }
    }

    public void FillStepWith(int position, int value)
    {

    }
}
