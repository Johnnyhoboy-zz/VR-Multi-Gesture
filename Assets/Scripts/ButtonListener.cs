using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;
using UnityEngine.Events;

public class ButtonListener : MonoBehaviour
{

    //public UnityEvent proximityevent;
    //public UnityEvent contactevent;
    //public UnityEvent actionevent;
    //public UnityEvent defaultevent;

    public OVRHand lefthand;
    public GameObject leftanchor;
    public OVRHand righthand;
    public GameObject rightanchor;

    public Material cur;
    public Material[] materials;

    public StateHolder state;

    bool eventtrigger;
    bool boxtrigger;

    Transform handpos;
    Transform mypos;
    int hand = 0;
    bool movetrigger; 

    public int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<ButtonController>().InteractableStateChanged.AddListener(InitialEvent);
        cur = materials[0];
        eventtrigger = true;
        lefthand = GameObject.FindGameObjectWithTag("Left Hand").GetComponent<OVRHand>();
        righthand = GameObject.FindGameObjectWithTag("Right Hand").GetComponent<OVRHand>();
        leftanchor = GameObject.FindGameObjectWithTag("Left Anchor");
        rightanchor = GameObject.FindGameObjectWithTag("Right Anchor");
        state = GameObject.FindGameObjectWithTag("State").GetComponent<StateHolder>();
        movetrigger = false;
    }

    void InitialEvent(InteractableStateArgs state)
    {
        //print(state.Interactable);
        if (state.NewInteractableState == InteractableState.ProximityState)
        {
            //GetComponent<MeshRenderer>().material = cur;
            //boxtrigger = true;
        } else if (state.NewInteractableState == InteractableState.ContactState)
        {
            //GetComponent<MeshRenderer>().material = cur;
            boxtrigger = true;
            /*if (righthand.GetFingerIsPinching(OVRHand.HandFinger.Index) && righthand.GetFingerPinchStrength(OVRHand.HandFinger.Index) > 0.8)
            {
                GetComponent<Transform>().position = righthand.GetComponent<Transform>().position;
            }
            if (lefthand.GetFingerIsPinching(OVRHand.HandFinger.Index) && lefthand.GetFingerPinchStrength(OVRHand.HandFinger.Index) > 0.8)
            {
                GetComponent<Transform>().position = lefthand.GetComponent<Transform>().position;
            }*/

        } else if (state.NewInteractableState == InteractableState.ActionState)
        {
            //actionevent.Invoke();
        } else
        {
            //defaultevent.Invoke();
            boxtrigger = false;
            //cur = materials[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool isIndexFingerPinching = lefthand.GetFingerIsPinching(OVRHand.HandFinger.Index);
        bool isMiddleFingerPinching = lefthand.GetFingerIsPinching(OVRHand.HandFinger.Middle);
        bool isRingFingerPinching = lefthand.GetFingerIsPinching(OVRHand.HandFinger.Ring);
        bool isPinkyFingerPinching = lefthand.GetFingerIsPinching(OVRHand.HandFinger.Pinky);

        bool isRIndexFingerPinching = righthand.GetFingerIsPinching(OVRHand.HandFinger.Index);
        bool isRMiddleFingerPinching = righthand.GetFingerIsPinching(OVRHand.HandFinger.Middle);
        bool isRRingFingerPinching = righthand.GetFingerIsPinching(OVRHand.HandFinger.Ring);
        bool isRPinkyFingerPinching = righthand.GetFingerIsPinching(OVRHand.HandFinger.Pinky);

        if (boxtrigger)
        {
            if (state.state == 0)
            {

                if ((isIndexFingerPinching || isRIndexFingerPinching) && eventtrigger)
                {
                    cur = materials[1];
                    eventtrigger = false;
                }
                else if ((isMiddleFingerPinching || isRMiddleFingerPinching) && eventtrigger)
                {
                    cur = materials[2];
                    eventtrigger = false;
                }
                else if ((isRingFingerPinching || isRRingFingerPinching) && eventtrigger)
                {
                    cur = materials[3];
                    eventtrigger = false;
                }
                else if ((isPinkyFingerPinching || isRPinkyFingerPinching) && eventtrigger)
                {
                    cur = materials[4];
                    eventtrigger = false;
                }
                if (!eventtrigger && !isIndexFingerPinching && !isMiddleFingerPinching && !isRingFingerPinching && !isPinkyFingerPinching &&
                    !isRIndexFingerPinching && !isRMiddleFingerPinching && !isRRingFingerPinching && !isRPinkyFingerPinching)
                {
                    eventtrigger = true;
                }

                GetComponent<MeshRenderer>().material = cur;
            }

            else if (state.state == 1)
            {


                if (isIndexFingerPinching && hand == 0 && eventtrigger)
                {
                    handpos = leftanchor.GetComponent<Transform>();
                    mypos = this.gameObject.GetComponent<Transform>();
                    hand = 1;
                    movetrigger = false;
                }
                if (isRIndexFingerPinching && hand == 0 && eventtrigger)
                {
                    handpos = rightanchor.GetComponent<Transform>();
                    mypos = this.gameObject.GetComponent<Transform>();
                    hand = 2;
                    movetrigger = false;
                }

                if (!eventtrigger)
                {
                    Transform curHand = leftanchor.GetComponent<Transform>();
                    if (hand == 2)
                    {
                        curHand = rightanchor.GetComponent<Transform>();
                    }
                    Transform curObj = this.gameObject.GetComponent<Transform>();


                    curObj.Rotate(curHand.eulerAngles);

                    handpos = curHand;
                }

                if (!eventtrigger && hand == 1 && !isIndexFingerPinching)
                {
                    hand = 0;
                    eventtrigger = true;
                }
                else if (!eventtrigger && hand == 2 && !isRIndexFingerPinching)
                {
                    hand = 0;
                    eventtrigger = true;
                }
            }
            else if (state.state == 2)
            {
                /*if (isIndexFingerPinching || isRIndexFingerPinching)
                {
                    Instantiate(this.gameObject, GetComponent<Transform>());
                }*/
            }
            else if (state.state == 3)
            {
                if (isIndexFingerPinching || isRIndexFingerPinching)
                {
                    this.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            eventtrigger = true;
            hand = 0;
        } 
    }
}
