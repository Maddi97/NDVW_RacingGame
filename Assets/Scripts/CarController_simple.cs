using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController_simple : MonoBehaviour
{
    private SpawnPointManager _spawnPointManager;
    public Rigidbody theRB;

    public float Acc=3f, maxSpeed=100f, turnStrenght=50, gravityForce = 10f, dragOnGround = 3f;


    
    public float speedInput, turnInput;
    private bool grounded;

    public LayerMask whatIsGround;
    public float groundRayLength = .5f;
    public Transform groundRayPoint;

    public Transform leftFrontWheel, rightFrontWheel;
    public float maxWheelTurn = 25f;

    //copied from the dude
    public void Awake()
    {
        _spawnPointManager = FindObjectOfType<SpawnPointManager>();
    }

    // Start is called before the first frame update
    public void Start()
    {
        theRB.transform.parent = null;
    }
    public void Respawn()
    {
        Vector3 pos = _spawnPointManager.SelectRandomSpawnpoint().Item1;
        Quaternion rotation = _spawnPointManager.SelectRandomSpawnpoint().Item2;
        theRB.MovePosition(pos);
        theRB.MoveRotation(rotation);
        transform.rotation = rotation;
        transform.position = pos - new Vector3(0, 0.4f, 0);
    }

    public void steer(float input){
        turnInput = input;
    }

    public void accelerate(float input){
        if(input > 0){
            speedInput = input*Acc*500f;
        }
        
    }

    // Update is called once per frame
    void Update()
    {

        //steer(Input.GetAxis("Horizontal"));
        //accelerate(Input.GetAxis("Vertical"));

        if(grounded){
            //transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput*turnStrenght*Time.deltaTime * Input.GetAxis("Vertical") , 0f));
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * 10f, 0f));
        }

        leftFrontWheel.localRotation = Quaternion.Euler(leftFrontWheel.localRotation.eulerAngles.x, (turnInput*maxWheelTurn)-180, leftFrontWheel.localRotation.eulerAngles.z);
        rightFrontWheel.localRotation = Quaternion.Euler(rightFrontWheel.localRotation.eulerAngles.x, turnInput*maxWheelTurn, rightFrontWheel.localRotation.eulerAngles.z);

        transform.position = theRB.transform.position;
        //speedInput = 0f;
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

