using System;
using UnityEngine;
using UnityEngine.Events;

public class SwipeManager
{
	public class OnSwipeHandler {
		public Action onSwipeUp;
    	public Action onSwipeDown;
    	public Action onSwipeLeft;
    	public Action onSwipeRight;

        public OnSwipeHandler(
            Action onSwipeUp,
    	    Action onSwipeDown,
    	    Action onSwipeLeft,
    	    Action onSwipeRight
        ) {
            this.onSwipeUp = onSwipeUp;
            this.onSwipeDown = onSwipeDown;
            this.onSwipeLeft = onSwipeLeft;
            this.onSwipeRight = onSwipeRight;
        }
	}

    private Vector2 fingerDown;
    private Vector2 fingerUp;
    public bool detectSwipeOnlyAfterRelease = false;

    public float SWIPE_THRESHOLD = 20f;

    private OnSwipeHandler handler;

	public SwipeManager(OnSwipeHandler handler) {
        this.handler = handler;
    }

    // Update is called once per frame
    public void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                fingerUp = touch.position;
                fingerDown = touch.position;
            }

            //Detects Swipe while finger is still moving
            if (touch.phase == TouchPhase.Moved)
            {
                if (!detectSwipeOnlyAfterRelease)
                {
                    fingerDown = touch.position;
                    checkSwipe();
                }
            }

            //Detects swipe after finger is released
            if (touch.phase == TouchPhase.Ended)
            {
                fingerDown = touch.position;
                checkSwipe();
            }
        }
    }

    void checkSwipe()
    {
        //Check if Vertical swipe
        if (verticalMove() > SWIPE_THRESHOLD && verticalMove() > horizontalValMove())
        {
            ////Debug.Log("Vertical");
            if (fingerDown.y - fingerUp.y > 0)//up swipe
            {
                OnSwipeUp();
            }
            else if (fingerDown.y - fingerUp.y < 0)//Down swipe
            {
                OnSwipeDown();
            }
            fingerUp = fingerDown;
        }

        //Check if Horizontal swipe
        else if (horizontalValMove() > SWIPE_THRESHOLD && horizontalValMove() > verticalMove())
        {
            ////Debug.Log("Horizontal");
            if (fingerDown.x - fingerUp.x > 0)//Right swipe
            {
                OnSwipeRight();
            }
            else if (fingerDown.x - fingerUp.x < 0)//Left swipe
            {
                OnSwipeLeft();
            }
            fingerUp = fingerDown;
        }

        //No Movement at-all
        else
        {
            ////Debug.Log("No Swipe!");
        }
    }

    float verticalMove()
    {
        return Mathf.Abs(fingerDown.y - fingerUp.y);
    }

    float horizontalValMove()
    {
        return Mathf.Abs(fingerDown.x - fingerUp.x);
    }

    //////////////////////////////////CALLBACK FUNCTIONS/////////////////////////////
    void OnSwipeUp()
    {
        //Debug.Log("Swipe UP");
        this.handler.onSwipeUp();
    }

    void OnSwipeDown()
    {
        //Debug.Log("Swipe Down");
        this.handler.onSwipeDown();
    }

    void OnSwipeLeft()
    {
        //Debug.Log("Swipe Left");
        this.handler.onSwipeLeft();
    }

    void OnSwipeRight()
    {
        //Debug.Log("Swipe Rifht");
        this.handler.onSwipeRight();
    }
}
