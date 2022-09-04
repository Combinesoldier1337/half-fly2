using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class menu_map : MonoBehaviour, IDragHandler, IScrollHandler
{
    [SerializeField] private Transform target, player;   
    [SerializeField] private RectTransform playerMarker, marker, mapPanel;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnEnable()
    {
        mapPanel.transform.localPosition = Vector3.zero;
        mapPanel.transform.localScale = Vector3.one;
        marker.transform.localPosition = new Vector3(target.position.x, target.position.z, 0) / 2;
        playerMarker.transform.localPosition = new Vector3(player.position.x, player.position.z, 0) / 2;
    }

    public void OnDrag(PointerEventData eventData)
    {
        mapPanel.transform.localPosition += (Vector3)eventData.delta;
    }

    public void OnScroll(PointerEventData eventData)
    {
        mapPanel.transform.localScale += (float)(eventData.scrollDelta.y/10)*Vector3.one;
    }
}
