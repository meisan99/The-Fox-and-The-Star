using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pg7TrackableEventHandler : DefaultTrackableEventHandler
{
    public static Pg7TrackableEventHandler instance;
    private bool pageIsActive;

    public GameObject trackIndicator;
    public UnityEvent OnTrackLost;
    public UnityEvent OnTrackFound;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public bool PageIsActive
    {
        get
        {
            return pageIsActive;
        }
    }

    protected override void OnTrackingLost()
    {
        trackIndicator.SetActive(true);
        pageIsActive = false;
        OnTrackLost.Invoke();
        base.OnTrackingLost();
    }

    protected override void OnTrackingFound()
    {
        trackIndicator.SetActive(false);
        pageIsActive = true;
        OnTrackFound.Invoke();
        base.OnTrackingFound();
    }
}
