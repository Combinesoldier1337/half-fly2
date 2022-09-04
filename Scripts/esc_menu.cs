using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class esc_menu : MonoBehaviour
{
    [SerializeField] private GameObject[] panelsToClose;
    bool Pause = false;
    CameraFollowTarget cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollowTarget>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Esc"))
        {
            EscKeyDown();
        }
    }

    public void EscKeyDown()
    {
        Pause = !Pause;        
        CloseAllPanels(false);
        panelsToClose[0].SetActive(Pause);
        CheckTime();
        DisableCamController();
    }

    private void CloseAllPanels(bool p)
    {
        foreach (GameObject panel in panelsToClose)
        {
            panel.SetActive(p);
        }
    }

    private void CheckTime()
    {
        Time.timeScale = Pause ? 0 : 1;
    }
    private void DisableCamController()
    {
        cam.enabled = !Pause;
    }
}
