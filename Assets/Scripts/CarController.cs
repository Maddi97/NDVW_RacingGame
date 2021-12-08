using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CarController : MonoBehaviour
{

    public Rigidbody theRB;

    public float forwardAcc=3f, reverseAcc=2f, maxSpeed=100f, turnStrenght=50, gravityForce = 10f, dragOnGround = 3f;

    public KeyCode switch_to_easy1 = KeyCode.P;
    public KeyCode switch_to_easy2 = KeyCode.O;
    public KeyCode switch_to_complex = KeyCode.I;

    public Vector3 complexStart_pos = new Vector3(3.7f, 0.641f, 20.5f);
    public Quaternion complexStart_rota = new Quaternion(0f, 0f, 0f, 0);
    public Vector3 simpleStart1_pos  = new Vector3(-150.2f, 0.641f, 30.7f);
    public Quaternion simpleStart1_rota  = new Quaternion(0f, 0f, 0f, 0);
    public Vector3 simpleStart2_pos  = new Vector3(-360.1f, 0.641f, 3.3f);
    public Quaternion simpleStart2_rota  = new Quaternion(0f, 0f, 0f, 0);

    
    private float speedInput, turnInput;
    private bool grounded;

    public LayerMask whatIsGround;
    public float groundRayLength = .5f;
    public Transform groundRayPoint;

    public Transform leftFrontWheel, rightFrontWheel;
    public float maxWheelTurn = 25f;

    public Countdown countDown;
    public float time = 0f;

    // Start is called before the first frame update
    void Start()
    {
        theRB.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (countDown.notFinished())
        {
            return;
        }
        time += Time.deltaTime;

        if(Input.GetKey(switch_to_easy1)){
            theRB.transform.position = simpleStart1_pos;
            transform.rotation = simpleStart1_rota;
            //Debug.Log(transform.position);
            return;
        }
        else if (Input.GetKey(switch_to_easy2)){
            theRB.transform.position = simpleStart2_pos;
            transform.rotation = simpleStart2_rota;
            return;
        }
        else if (Input.GetKey(switch_to_complex)){
            theRB.transform.position = complexStart_pos;
            transform.rotation = complexStart_rota;
            return;
        }




        speedInput = 0f;
        if(Input.GetAxis("Vertical") > 0){
            speedInput = Input.GetAxis("Vertical") *forwardAcc *1000f;
        }
        else if(Input.GetAxis("Vertical") < 0){
            speedInput = Input.GetAxis("Vertical") * reverseAcc * 1000f;
        }

        turnInput = Input.GetAxis("Horizontal");
        if(grounded){
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput*turnStrenght*Time.deltaTime * Input.GetAxis("Vertical") , 0f));
        }

        leftFrontWheel.localRotation = Quaternion.Euler(leftFrontWheel.localRotation.eulerAngles.x, (turnInput*maxWheelTurn)-180, leftFrontWheel.localRotation.eulerAngles.z);
        rightFrontWheel.localRotation = Quaternion.Euler(rightFrontWheel.localRotation.eulerAngles.x, turnInput*maxWheelTurn, rightFrontWheel.localRotation.eulerAngles.z);

        transform.position = theRB.transform.position;
    }

    private void FixedUpdate(){
        grounded = false;
        RaycastHit hit;

        if(Physics.Raycast(groundRayPoint.position, -transform.up, out hit, groundRayLength, whatIsGround)){
            grounded = true;
            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }

        if(grounded){
            theRB.drag = dragOnGround;

            if(Mathf.Abs(speedInput) > 0 ){
                theRB.AddForce(transform.forward * speedInput);
            }
        }
        else{
            theRB.drag = 0.1f;
            theRB.AddForce(Vector3.up * -gravityForce* 100f);
        }
    }
}

