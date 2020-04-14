using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateHolder : MonoBehaviour
{
    public OVRHand lefthand;
    public GameObject leftanchor;
    public OVRHand righthand;

    public GameObject prefab;

    public Material cur;
    public Material[] materials;

    public int state;

    bool create;

    public Text text;

    public enum State {Default, Moveing, Copy, Erase };

    // Start is called before the first frame update
    void Start()
    {
        state = 0;
        cur = materials[0];
        GetComponent<MeshRenderer>().material = cur;
        create = true;
        text.text = "Color Mode";
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
            state = 1;
            cur = materials[1];
            GetComponent<MeshRenderer>().material = cur;
            text.text = "Rotate/Scale Mode";
        } else if (lMiddlePinch && rMiddlePinch)
        {
            state = 2;
            cur = materials[2];
            GetComponent<MeshRenderer>().material = cur;
            text.text = "Copy/Paste Mode";
        } else if (lRingPinch && rRingPinch)
        {
            state = 3;
            cur = materials[3];
            GetComponent<MeshRenderer>().material = cur;
            text.text = "Create/Erase Mode";
        } else if (lPinkyPinch && rPinkyPinch)
        {
            state = 0;
            cur = materials[0];
            GetComponent<MeshRenderer>().material = cur;
            text.text = "Color Mode";
        }

        if(state == 3 && lIndexPinch && !rIndexPinch && create)
        {
            Vector3 localchange = new Vector3(-leftanchor.GetComponent<Transform>().forward.z, leftanchor.GetComponent<Transform>().forward.y, leftanchor.GetComponent<Transform>().forward.x);
            Vector3 temp = leftanchor.GetComponent<Transform>().position + localchange;
            GameObject newObject = new GameObject();
            Transform newTransform = newObject.transform;
            newTransform.localPosition = temp;
            //newTransform.localRotation = leftanchor.GetComponent<Transform>().rotation;
            Instantiate(prefab, newTransform);
            create = false;
        } 
        if (state == 3 && !lIndexPinch)
        {
            create = true;
        }

    }
}
