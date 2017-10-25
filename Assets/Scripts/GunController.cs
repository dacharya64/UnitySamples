using UnityEngine;
using System.Collections;
using EZEffects;

public class GunController : MonoBehaviour {

    public GameObject controllerRight;
    public int number;

    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device device;

    public EffectTracer TracerEffect;
    public Transform muzzleTransform;
    private SteamVR_TrackedController controller;

    // Use this for initialization
    void Start () { 
        controller = controllerRight.GetComponent<SteamVR_TrackedController>();
        controller.TriggerClicked += TriggerPressed;
        trackedObj = controllerRight.GetComponent<SteamVR_TrackedObject>();
	}

    private void TriggerPressed(object sender, ClickedEventArgs e) {
        ShootWeapon();
    }

    public void ShootWeapon() {
        RaycastHit hit = new RaycastHit();
        Ray ray = new Ray(muzzleTransform.position, muzzleTransform.forward);

        device = SteamVR_Controller.Input((int)trackedObj.index);
        device.TriggerHapticPulse(750);
        TracerEffect.ShowTracerEffect(muzzleTransform.position, muzzleTransform.forward, 250f);

        if (Physics.Raycast(ray, out hit, 5000f)) {
            if (hit.collider.attachedRigidbody) {
                Debug.Log("We've hit " + hit.collider.gameObject.name);
                Destroy(hit.collider.gameObject);
                //hit.collider.gameObject.transform(10, 10, 10);
            }
        }
    }
	// Update is called once per frame
	void Update () {
	
	}
}
