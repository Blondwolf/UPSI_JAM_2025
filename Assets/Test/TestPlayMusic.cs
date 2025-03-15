using System;
using UnityEngine;
using UnityEngine.Events;

public class TestPlayMusic : MonoBehaviour
{
    public int bpm = 120; // Battements par minute
    public int[] melody = new int[32]; // 32 temps
    private int currentStep = 0; // Position actuelle dans la séquence
    private float timePerStep; // Temps entre chaque temps
    private float timer = 0; // Chronomètre interne

    // Événements pour signaler le début et la fin d'un son
    public UnityEvent<int> OnEventStart;
    public UnityEvent OnEventStop;

    void Start()
    {
        timePerStep = 60f / bpm; // Calcul du temps entre chaque step
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timePerStep)
        {
            timer -= timePerStep;
            ProcessStep(); // Vérification du temps actuel
            currentStep = (currentStep + 1) % melody.Length; // Passage au temps suivant
            Debug.Log("time:" + currentStep);
        }
    }

    void ProcessStep()
    {
        if (melody[currentStep] != 0)
        {
            OnEventStart?.Invoke(melody[currentStep]); // Déclencher l'événement de début
        }
        else
        {
            OnEventStop?.Invoke(); // Déclencher l'événement d'arrêt
        }
    }
}