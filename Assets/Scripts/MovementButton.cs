using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class movementbutton : MonoBehaviour
{

    //    public GameObject player;
    //    private Rigidbody rb;
    //    private Vector3 movement;
    //    private Vector3 inputa;
    //    public int speed;
    //    public bool isHeldDown;
    //    private bool rbAssigned = false;

    //	// Use this for initialization
    //	void Start () {
    //        isHeldDown = false;

    //        rb = player.GetComponent<Rigidbody>();

    //	}

    //    void updateRb()
    //    {

    //        try
    //        {
    //            rb = player.GetComponent<Rigidbody>();
    //        }
    //        catch(UnassignedReferenceException e)
    //        {

    //        }
    //        rbAssigned = true;

    //    }

    //    void FixedUpdate()
    //    {
    //        if (!rbAssigned)
    //        {
    //            updateRb();
    //        }
    //        if (isHeldDown)
    //        {
    //            Debug.Log(rb);
    //            //inputa = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    //            rb.AddRelativeForce(Vector3.forward,ForceMode.Impulse);//,ForceMode.Force); //speed * Time.deltaTime;
    //            //player.transform.position += movement;
    //            ////player.transform.Translate(movement*speed);
    //            //rb.velocity = movement;
    //        }
    //        else
    //        {
    //            player.transform.Rotate(0, 2, 0);
    //        }
    //    }

    //    public void OnPressDown()
    //    {
    //        isHeldDown = true;
    //    }

    //    public void OnRelease()
    //    {
    //        isHeldDown = false;
    //    }

    //	// Update is called once per frame

}
