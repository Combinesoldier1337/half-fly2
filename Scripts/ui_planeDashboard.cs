using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ui_planeDashboard : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Text height, speed, sight, jetPowerText;
    [SerializeField] RectTransform roll;
    [SerializeField] Image pitch, jetPower;
    Transform plane;
    GameObject cam;
    AircraftController ac;
    SkinnedMeshRenderer jetFlames;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        plane = rb.transform;        
        jetFlames = plane.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>();
        ac = rb.GetComponent<AircraftController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!rb.IsSleeping())
        {
            roll.transform.rotation = Quaternion.Euler(Vector3.forward * (plane.eulerAngles.z));
            float angle = plane.eulerAngles.x > 45 ? plane.eulerAngles.x - 360 : plane.eulerAngles.x;
            pitch.fillAmount = (((angle)/45)+1)/2;
            jetPower.fillAmount = ac.getCurrentJetPower < 1?0:(ac.getCurrentJetPower / ac.getTotalJetPower);
            jetFlames.SetBlendShapeWeight(0, (Random.Range(0.9f,1.1f) - jetPower.fillAmount)*100);
            //text info
            jetPowerText.text = jetPower.fillAmount == 0?"0%": (jetPower.fillAmount*100).ToString("#.") + "%";
            height.text = (plane.transform.position.y*10).ToString("#.#") + "м";
            speed.text = (rb.velocity.magnitude*20).ToString("#.") + "км/ч";
            sight.text = (RenderSettings.fogEndDistance*10).ToString("#.") + "м";
        }
    }
}
