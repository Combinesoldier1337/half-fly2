using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSetup : MonoBehaviour
{
    [SerializeField] private GameObject player, spawnPoints, Targets, winPanel, targetMarker, controls_help;
    private GameObject spawnPoint, target;
    Rigidbody rb;
    public bool freeroam = false;
    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = spawnPoints.transform.GetChild(PlayerPrefs.GetInt("SpawnPos") - 1).gameObject;
        player.transform.position = spawnPoint.transform.position;
        player.transform.rotation = spawnPoint.transform.rotation;
        rb = player.GetComponent<Rigidbody>();

        target = Targets.transform.GetChild(PlayerPrefs.GetInt("TargetPos") - 1).gameObject;
        target.SetActive(true);

        controls_help.SetActive(true);
    }

    public void StartFreeroaming()
    {
        freeroam = true;
    }

    public bool isFreeroaming { get { return freeroam; } set { value = freeroam; } }

    private void Update()
    {
        if (!freeroam && Vector3.Distance(player.transform.position, target.transform.position) < 2 && rb.velocity.magnitude < 3)
        {
            target.SetActive(false);
            winPanel.SetActive(true);
            targetMarker.SetActive(false);
        }
    }
}
