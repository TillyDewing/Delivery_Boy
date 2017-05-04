using UnityEngine;
using System.Collections;

public class BikeController : MonoBehaviour
{
    public WheelCollider fWheelCollider;
    public WheelCollider rWheelCollider;
    public Transform fWheel;
    public Transform fSusp;
    public Transform rWheel;
    public Transform handleBars;
    public Rigidbody bike;


    public float maxSteerAngle = 20;
    public float speed = 200;
    public float brakeForce = 500;
    public float leanThreshold = 4;
    public float maxLeanAngle = 20;
    private void Start()
    {
        fWheelCollider.ConfigureVehicleSubsteps(5,12,15);
    }

    private void Update()
    {
        

        Debug.Log(bike.velocity.magnitude);

        float move = Input.GetAxis("Horizontal");
        float throtle = Input.GetAxis("Vertical");
        float leanAngle = Mathf.Lerp(0, maxLeanAngle, Mathf.Abs(move)) * Mathf.Clamp(bike.velocity.magnitude / leanThreshold, 0,1) * Mathf.Ceil(move) * -1;
        
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, leanAngle);
    

        fWheelCollider.steerAngle = maxSteerAngle * move;
        rWheelCollider.motorTorque = speed * throtle;

        if (Input.GetKey(KeyCode.Space))
        {
            fWheelCollider.brakeTorque = brakeForce;
            rWheelCollider.brakeTorque = brakeForce;
        }
        else
        {
            fWheelCollider.brakeTorque = 0f;
            rWheelCollider.brakeTorque = 0f;
        }


        fWheel.localEulerAngles = new Vector3(fWheel.localEulerAngles.x, fWheelCollider.steerAngle - fWheel.localEulerAngles.z, fWheel.localEulerAngles.z);
        fSusp.localEulerAngles = new Vector3(fSusp.localEulerAngles.x, fWheelCollider.steerAngle - fSusp.localEulerAngles.z, fSusp.localEulerAngles.z);
        handleBars.localEulerAngles = new Vector3(handleBars.localEulerAngles.x, fWheelCollider.steerAngle - handleBars.localEulerAngles.z, handleBars.localEulerAngles.z);

        fWheel.Rotate(fWheelCollider.rpm / 60 * 360 * Time.deltaTime, 0, 0);
        rWheel.Rotate(rWheelCollider.rpm / 60 * 360 * Time.deltaTime, 0, 0);

    }
}
