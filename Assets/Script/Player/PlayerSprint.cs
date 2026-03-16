using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSprint : MonoBehaviour
{
    [SerializeField] private Slider SprintSlider;
    [SerializeField] private float MaxSprint = 100f;
    public float CurrentSprint;

    public bool canSprint { get { return CurrentSprint > 0 && CanSprintAgain; } }

    [SerializeField] private float DrainRate = 20f;
    [SerializeField] private float regValue = 10f;

    private bool CanSprintAgain = true;
    void Start()
    {
        SprintSlider.maxValue = MaxSprint;
        CurrentSprint = MaxSprint;
        SprintSlider.value = CurrentSprint;
    }

    public void UseSprint(float deltaTime)
    {
        CurrentSprint -= DrainRate * deltaTime;
        CurrentSprint = Mathf.Clamp(CurrentSprint, 0, MaxSprint);
        SprintSlider.value = CurrentSprint;
        
        if(CurrentSprint <= 0 && CanSprintAgain)
        {
            CanSprintAgain = false;
            StartCoroutine(restoreCanSprintAgain());
        }
            
        
    }

    public void regenerateSprint(float deltaTime)
    {
        if(CurrentSprint < MaxSprint)
        {
            CurrentSprint += regValue * deltaTime;
            SprintSlider.value = CurrentSprint;
        }
    }

    IEnumerator restoreCanSprintAgain()
    {
        yield return new WaitForSeconds(1f);
        CanSprintAgain = true;
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
