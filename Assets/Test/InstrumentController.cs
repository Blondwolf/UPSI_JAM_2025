using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InstrumentController : MonoBehaviour
{
    public int[] melody = new int[32];
    public int bpm;

    public int selectedNote = 1;
    //public Color selectedColor = Color.green;

    private float timePerStep;

    private int currentStep = 0;
    private float timeSinceStarted = 0;

    public InputActionReference pressArcAction;

    public Transform splitParent;

    [Header("Events")]
    public UnityEvent<int> OnEventStart;
    public UnityEvent OnEventStop;

    private bool drawing = false;
    bool running = false;
    public bool selected = false;

    void Start()
    {
        timePerStep = 60f / bpm;

        WheelRotateController wheel = GetComponentInChildren<WheelRotateController>();
        wheel.bpm = bpm;
        wheel.beatsNumber = melody.Length;
    }

    //void Update()
    //{
    //    if (!running)
    //        return;

    //    timeSinceStarted += Time.deltaTime;

    //    if (timeSinceStarted >= timePerStep)
    //    {
    //        timeSinceStarted -= timePerStep;
    //        RecordStep();
    //        PlayStep();
    //        currentStep = (currentStep + 1) % melody.Length;
    //        Debug.Log(timeSinceStarted);
    //    }
    //}

    IEnumerator PlayMelody()
    {
        float nextStepTime = Time.time;

        while (running)
        {
            if (Time.time >= nextStepTime)
            {
                RecordStep();
                PlayStep();
                currentStep = (currentStep + 1) % melody.Length;

                Debug.Log("Step: " + currentStep);

                nextStepTime += timePerStep;
            }

            yield return null;
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
        if (running && selected)
            drawing = true;
    }

    void StopDrawing()
    {
        drawing = false;
    }

    void RecordStep()
    {
        if (drawing && selected)
        {
            melody[currentStep] = selectedNote;
        }
    }

    void PlayStep()
    {
        if (melody[currentStep] != 0)
        {
            // Notes start at 0 but software start with 1 so => -1
            OnEventStart?.Invoke(melody[currentStep] - 1); 
        }
        else
        {
            OnEventStop?.Invoke();
        }
    }

    public void SelectNote(int note, Color color)
    {
        selectedNote = note;
    }

    //public void FillStepWith(int position, int value)
    //{

    //}

    public void StartRunning()
    {
        running = true;
        StartCoroutine(PlayMelody());
    }

    public void StopRunning()
    {
        running = false;
        currentStep = 0;
        timeSinceStarted = 0;
    }

    public void ClearSplits()
    {
        melody = new int[32];
        foreach (Transform child in splitParent)
        {
            Destroy(child.gameObject);
        }
    }
}
