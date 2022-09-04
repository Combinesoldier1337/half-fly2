using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class main_menu : MonoBehaviour
{
    [SerializeField] private Transform difficulty, route;
    [SerializeField] private Slider sliderGraphics;
    int d = 0,r = 0;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    public void loadGameScene()
    {
        SceneManager.LoadScene(1);        
    }
    public void loadMainMenuScene()
    {
        SceneManager.LoadScene(0);
    }

    public void swichDifficulty(bool b)
    {
        d = SwithTransform(b, difficulty, d);
    }

    public void swichRoute(bool b)
    {
        r = SwithTransform(b, route, r);
    }

    private int SwithTransform(bool b, Transform t, int n)
    {
        t.GetChild(n).gameObject.SetActive(false);
        n = b ? n + 1 : n - 1;
        n = n > t.childCount - 1 ? 0 : n;
        n = n < 0 ? t.childCount - 1 : n;
        t.GetChild(n).gameObject.SetActive(true);
        return n;
    }

    public void SetupPositions()
    {
        int p;
        switch (r)
        {
            case 0:
                SpawnPos(2, out p);
                TargetPos(1, out p);
                break;
            case 1:
                SpawnPos(1, out p);
                TargetPos(3, out p);
                break;
            case 2:
                SpawnPos(3, out p);
                TargetPos(2, out p);
                break;
        }
    }

    public void SpawnPos(int n, out int p)
    {
        PlayerPrefs.SetInt("SpawnPos", n);
        p = PlayerPrefs.GetInt("SpawnPos");
    }

    public void TargetPos(int n, out int p)
    {
        PlayerPrefs.SetInt("TargetPos", n);
        p = PlayerPrefs.GetInt("TargetPos");
    }

    public void SetQualitySettings()
    {
        QualitySettings.SetQualityLevel((int)sliderGraphics.value);
    }

}
