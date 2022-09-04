using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    [SerializeField] public GameObject currentVehicle, target;
    [SerializeField] private RectTransform playerCursor, targetMarker;
    [SerializeField] private RectTransform mapImg, mapHolder;
    [SerializeField] private float minimapZoom = 1;
    [SerializeField] private Slider slider_scale, slider_zoom;

    private void Start()
    {
        foreach (Transform go in target.transform)
        {
            if (go.gameObject.activeSelf)
            {
                target = go.gameObject;
            }
        }
    }

    void Update()
    {
        if (!currentVehicle.GetComponent<Rigidbody>().IsSleeping())
        {
            playerCursor.rotation = Quaternion.Euler(Vector3.back * currentVehicle.transform.rotation.eulerAngles.y);
            mapImg.localPosition = (Vector3.left * currentVehicle.transform.position.x) - (Vector3.up * currentVehicle.transform.position.z);
            Vector2 pos = new Vector3(target.transform.position.x, target.transform.position.z)+mapImg.localPosition;
            targetMarker.localPosition = Vector2.ClampMagnitude(pos,45* minimapZoom);
            //targetMarker.localPosition = pos;
        }        
    }

    public void RescaleMinimap()
    {
        transform.localScale = (1 + slider_scale.value) * Vector3.one;
        transform.position = new Vector2(70 + slider_scale.value*60, Screen.height - (slider_scale.value*60) - 70);
    }

    public void ZoomMinimap()
    {
        minimapZoom = slider_zoom.value;
        playerCursor.localScale = (slider_zoom.value) * Vector3.one;
        targetMarker.localScale = (slider_zoom.value) * Vector3.one;
        mapHolder.localScale = (1/slider_zoom.value) * Vector3.one;
    }
}
