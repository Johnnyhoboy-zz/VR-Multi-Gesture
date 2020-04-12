using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StateHolder : MonoBehaviour
{
    public OVRHand lefthand;
    public OVRHand righthand;

    public Material cur;
    public Material[] materials;

    public int state;

    public enum State { Default, Moveing, Copy, Erase };

    // Start is called before the first frame update
    void Start()
    {
        state = 0;
        cur = materials[0];
        GetComponent<MeshRenderer>().material = cur;
    }

    // Update is called once per frame
    void Update()
    {
        bool lIndexPinch = lefthand.GetFingerIsPinching(OVRHand.HandFinger.Index);
        bool lMiddlePinch = lefthand.GetFingerIsPinching(OVRHand.HandFinger.Middle);
        bool lRingPinch = lefthand.GetFingerIsPinching(OVRHand.HandFinger.Ring);
        bool lPinkyPinch = lefthand.GetFingerIsPinching(OVRHand.HandFinger.Pinky);

        bool rIndexPinch = righthand.GetFingerIsPinching(OVRHand.HandFinger.Index);
        bool rMiddlePinch = righthand.GetFingerIsPinching(OVRHand.HandFinger.Middle);
        bool rRingPinch = righthand.GetFingerIsPinching(OVRHand.HandFinger.Ring);
        bool rPinkyPinch = righthand.GetFingerIsPinching(OVRHand.HandFinger.Pinky);

        /*if (lPinkyPinch && rPinkyPinch)
        {
            state++;
            if (state > 3)
            {
                state = 0;
            }
            cur = materials[state];
            GetComponent<MeshRenderer>().material = cur;
        }*/

        if (lIndexPinch && rIndexPinch)
        {
            state = 0;
            cur = materials[0];
            GetComponent<MeshRenderer>().material = cur;
        } else if (lMiddlePinch && rMiddlePinch)
        {
            state = 1;
            cur = materials[1];
            GetComponent<MeshRenderer>().material = cur;
        } else if (lIndexPinch && rMiddlePinch)
        {
            state = 2;
            cur = materials[2];
            GetComponent<MeshRenderer>().material = cur;
        } else if (lMiddlePinch && rIndexPinch)
        {
            state = 3;
            cur = materials[3];
            GetComponent<MeshRenderer>().material = cur;
        }

    }
}
