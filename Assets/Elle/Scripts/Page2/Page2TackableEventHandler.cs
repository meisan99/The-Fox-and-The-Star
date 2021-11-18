using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Page2TackableEventHandler : DefaultTrackableEventHandler
{
    public static Page2TackableEventHandler instance;
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
        OnTrackFound.Invoke();
        base.OnTrackingFound();
    }
}
