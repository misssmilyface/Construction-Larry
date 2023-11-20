using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatWidth;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position; // we're getting the position from the background at the beginning, because we attached this RepeatBackground.cs to the background, that's why we don't need to specify that it's the background location/position.
        repeatWidth = GetComponent<BoxCollider>().size.x / 2; //we're going to repeat half of the background's size.x from the boxcollider (our current background is made with 2 background frames) that knows exactly how much the background's size.x is.
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < startPos.x - repeatWidth) //if our current position for the background is less than our starter position - 50( now is repeatWidth)
        {                                          //we get position.x because the background is only moving in our x position.
            transform.position = startPos; //our position would go back to our starting position.
        }
    }
}
