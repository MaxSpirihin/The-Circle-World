using UnityEngine;
using System.Collections;

/// <summary>
/// отвечает за ввод
/// </summary>
public class MobileInputManager : MonoBehaviour {

    //Singleton
    private static MobileInputManager _instance;
    private static MobileInputManager Instance
    {
        get
        {
            //Если объекта еще нет, находим его на сцене
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<MobileInputManager>();
            return _instance;
        }
    }


    private float fingerStartTime = 0.0f;
    private Vector2 fingerStartPos = Vector2.zero;

    private bool isSwipe = false;
    private float minSwipeDist = 50.0f;
    private float maxSwipeTime = 0.5f;
    private Swipe currentSwipe;
	
	void Start () {
	
	}
	
	void Update () {

            foreach (Touch touch in Input.touches)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        /* this is a new touch */
                        isSwipe = true;
                        fingerStartTime = Time.time;
                        fingerStartPos = touch.position;
                        break;

                    case TouchPhase.Canceled:
                        /* The touch is being canceled */
                        isSwipe = false;
                        break;

                    case TouchPhase.Ended:

                        float gestureTime = Time.time - fingerStartTime;
                        float gestureDist = (touch.position - fingerStartPos).magnitude;

                        if (isSwipe && gestureTime < maxSwipeTime && gestureDist > minSwipeDist)
                        {
                            Vector2 direction = touch.position - fingerStartPos;
                            Vector2 swipeType = Vector2.zero;

                            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                            {
                                // the swipe is horizontal:
                                swipeType = Vector2.right * Mathf.Sign(direction.x);
                            }
                            else
                            {
                                // the swipe is vertical:
                                swipeType = Vector2.up * Mathf.Sign(direction.y);
                            }

                            if (swipeType.x != 0.0f)
                            {
                                if (swipeType.x > 0.0f)
                                {
                                    currentSwipe = Swipe.Right;
                                    return;
                                }
                                else
                                {
                                    currentSwipe = Swipe.Left;
                                    return;
                                }
                            }

                            if (swipeType.y != 0.0f)
                            {
                                if (swipeType.y > 0.0f)
                                {
                                    currentSwipe = Swipe.Up;
                                    return;
                                }
                                else
                                {
                                    currentSwipe = Swipe.Down;
                                    return;
                                }
                            }

                        }
                        break;
                }
            }
            currentSwipe = Swipe.None;
	}


    public static bool GetSwipe(Swipe swipe)
    {
        return (Instance.currentSwipe == swipe);
    }
}
