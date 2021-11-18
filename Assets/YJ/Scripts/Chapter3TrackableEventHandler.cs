using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Chapter3TrackableEventHandler : DefaultTrackableEventHandler
{
    public static Chapter3TrackableEventHandler instance;
    private bool pageIsActive;

    public GameObject trackIndicator;
    public UnityEvent OnTrackLost;
    public UnityEvent OnTrackFound;

    public bool PageIsActive
    {
        get
        {
            return pageIsActive;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
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
        Time.timeScale = 1f;
        OnTrackFound.Invoke();
        base.OnTrackingFound();
    }
}
