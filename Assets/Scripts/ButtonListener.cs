using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;
using UnityEngine.Events;

public class ButtonListener : MonoBehaviour
{


    public OVRHand lefthand;
    public GameObject leftanchor;
    public OVRHand righthand;
    public GameObject rightanchor;

    public Material cur;
    public Material[] materials;

    public StateHolder state;

    bool eventtrigger;
    bool boxtrigger;

    Transform curleft;
    Transform curright;
    int hand;
    float xIndex;

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
        curleft = leftanchor.GetComponent<Transform>();
        curright = rightanchor.GetComponent<Transform>();
        state = GameObject.FindGameObjectWithTag("State").GetComponent<StateHolder>();
        hand = 0;
        xIndex = 0f;
    }

    void InitialEvent(InteractableStateArgs state)
    {
        //print(state.Interactable);
        if (state.NewInteractableState == InteractableState.ProximityState)
        {
            //boxtrigger = true;
        } else if (state.NewInteractableState == InteractableState.ContactState)
        {
            boxtrigger = true;

        } else if (state.NewInteractableState == InteractableState.ActionState)
        {
        } else
        {
            boxtrigger = false;
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

                if ((isIndexFingerPinching && !isRIndexFingerPinching) && eventtrigger)
                {
                    cur = materials[1];
                    eventtrigger = false;
                }
                else if ((isMiddleFingerPinching && !isRMiddleFingerPinching) && eventtrigger)
                {
                    cur = materials[2];
                    eventtrigger = false;
                }
                else if ((isRingFingerPinching && !isRRingFingerPinching) && eventtrigger)
                {
                    cur = materials[3];
                    eventtrigger = false;
                }
                else if ((isPinkyFingerPinching && !isRPinkyFingerPinching) && eventtrigger)
                {
                    cur = materials[4];
                    eventtrigger = false;
                }
                else if ((!isIndexFingerPinching && isRIndexFingerPinching) && eventtrigger)
                {
                    cur = materials[5];
                    eventtrigger = false;
                }
                else if ((!isMiddleFingerPinching && isRMiddleFingerPinching) && eventtrigger)
                {
                    cur = materials[6];
                    eventtrigger = false;
                }
                else if ((!isRingFingerPinching && isRRingFingerPinching) && eventtrigger)
                {
                    cur = materials[7];
                    eventtrigger = false;
                }
                else if ((!isPinkyFingerPinching && isRPinkyFingerPinching) && eventtrigger)
                {
                    cur = materials[8];
                    eventtrigger = false;
                }
                else if (!eventtrigger && !isIndexFingerPinching && !isMiddleFingerPinching && !isRingFingerPinching && !isPinkyFingerPinching &&
                    !isRIndexFingerPinching && !isRMiddleFingerPinching && !isRRingFingerPinching && !isRPinkyFingerPinching)
                {
                    eventtrigger = true;
                }

                GetComponent<MeshRenderer>().material = cur;
            }
            else if (state.state == 1 && isIndexFingerPinching && !isRIndexFingerPinching)
            {
                this.gameObject.GetComponent<Transform>().localEulerAngles =leftanchor.GetComponent<Transform>().eulerAngles;
            }
            else if (state.state == 1 && isRIndexFingerPinching && !isIndexFingerPinching)
            {
                this.gameObject.GetComponent<Transform>().localEulerAngles = rightanchor.GetComponent<Transform>().eulerAngles;
            }
            else if (state.state == 1 && ((isMiddleFingerPinching && !isRMiddleFingerPinching) || (!isMiddleFingerPinching && isRMiddleFingerPinching)))
            {  
                    this.gameObject.GetComponent<Transform>().localScale = this.gameObject.GetComponent<Transform>().localScale - new Vector3(0.005f, 0.005f, 0.005f);
            }
            else if (state.state == 1 && ((isRingFingerPinching && !isRRingFingerPinching) || (!isRingFingerPinching && isRRingFingerPinching)))
            {
                    this.gameObject.GetComponent<Transform>().localScale = this.gameObject.GetComponent<Transform>().localScale + new Vector3(0.005f, 0.005f, 0.005f);
            }
            else if (state.state == 2)
            {
                if (eventtrigger)
                {
                    if (isIndexFingerPinching && !isRIndexFingerPinching)
                    {
                        Vector3 localchange = new Vector3(-leftanchor.GetComponent<Transform>().forward.z, leftanchor.GetComponent<Transform>().forward.y, leftanchor.GetComponent<Transform>().forward.x);
                        Vector3 temp = leftanchor.GetComponent<Transform>().position + localchange;
                        GameObject newObject = new GameObject();
                        Transform newTransform = newObject.transform;
                        newTransform.localPosition = temp;
                        Instantiate(this.gameObject, newTransform);
                        eventtrigger = false;
                    } else if ( isRIndexFingerPinching && !isIndexFingerPinching)
                    {
                        Vector3 localchange = new Vector3(-rightanchor.GetComponent<Transform>().forward.z, rightanchor.GetComponent<Transform>().forward.y, rightanchor.GetComponent<Transform>().forward.x);
                        Vector3 temp = rightanchor.GetComponent<Transform>().position + localchange;
                        GameObject newObject = new GameObject();
                        Transform newTransform = newObject.transform;
                        newTransform.localPosition = temp;
                        Instantiate(this.gameObject, newTransform);
                        eventtrigger = false;
                    }
                }
                if (!isIndexFingerPinching && !isRIndexFingerPinching)
                {
                    eventtrigger = true;
                }
            }
            else if (state.state == 3)
            {
                if (isRIndexFingerPinching && !isIndexFingerPinching)
                {
                    this.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            eventtrigger = true;
            curleft = leftanchor.GetComponent<Transform>();
            curright = rightanchor.GetComponent<Transform>();
        }

        
    }

    void updateValue(float a, float b)
    {
        b = a;
    }

}
