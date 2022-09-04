using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog_controller : MonoBehaviour
{
    Rigidbody rb;
    float init_fog_distance;
    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        init_fog_distance = RenderSettings.fogEndDistance;
        player = rb.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!rb.IsSleeping())
        {
            float fog = init_fog_distance - ((Mathf.Abs(player.position.x) + Mathf.Abs(player.position.z))/2);
            RenderSettings.fogEndDistance = fog < 1? 5 : fog;
            if (Mathf.Abs(player.position.x) > 950)
            {
                player.position = new Vector3 (-player.position.x*.95f, player.position.y, player.position.z);
            }
            if (Mathf.Abs(player.position.z) > 950)
            {
                player.position = new Vector3(player.position.x, player.position.y, -player.position.z * .95f);
            }
        }
    }
}
