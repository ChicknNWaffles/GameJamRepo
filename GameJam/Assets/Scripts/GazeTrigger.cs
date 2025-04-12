using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GazeTrigger : MonoBehaviour
{
    [Tooltip("How long you have to look to trigger")]
    public float timeToTrigger;
    public UnityEvent triggerEvent;
    public bool triggerOnce = true;

    float triggerStart;
    bool triggered = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseEnter()
    {
        triggerStart = Time.time;
    }

    void OnMouseOver()
    {
        if (Time.time >= triggerStart + timeToTrigger && (!triggerOnce || !triggered))
        {
            triggered = true;
            triggerEvent.Invoke();
            print("Triggered!");
        }
    }
}
