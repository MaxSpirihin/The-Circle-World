using UnityEngine;
using System.Collections;

/// <summary>
/// отвечает за ввод с мобильных устройств - код преимущественно взят извне
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

    private Swipe currentSwipe;

    private const int mMessageWidth  = 200;
    private const int mMessageHeight = 64;
 
    private readonly Vector2 mXAxis = new Vector2(1, 0);
    private readonly Vector2 mYAxis = new Vector2(0, 1);

    // The angle range for detecting swipe
    private const float mAngleRange = 30;

    // To recognize as swipe user should at lease swipe for this many pixels
    private const float mMinSwipeDist = 15.0f; //50

    // To recognize as a swipe the velocity of the swipe
    // should be at least mMinVelocity
    // Reduce or increase to control the swipe speed
    private const float mMinVelocity = 400.0f; //2000

    private Vector2 mStartPosition;
    private float mSwipeStartTime;



    void Start()
    {
        currentSwipe = Swipe.None;
    }

    void Update()
    {

        // Mouse button down, possible chance for a swipe
        if (Input.GetMouseButtonDown(0))
        {
            // Record start time and position
            mStartPosition = new Vector2(Input.mousePosition.x,
                                         Input.mousePosition.y);
            mSwipeStartTime = Time.time;
        }

        // Mouse button up, possible chance for a swipe
        if (Input.GetMouseButtonUp(0))
        {
            float deltaTime = Time.time - mSwipeStartTime;

            Vector2 endPosition = new Vector2(Input.mousePosition.x,
                                               Input.mousePosition.y);
            Vector2 swipeVector = endPosition - mStartPosition;

            float velocity = swipeVector.magnitude / deltaTime;

            if (velocity > mMinVelocity &&
                swipeVector.magnitude > mMinSwipeDist)
            {
                // if the swipe has enough velocity and enough distance

                swipeVector.Normalize();

                float angleOfSwipe = Vector2.Dot(swipeVector, mXAxis);
                angleOfSwipe = Mathf.Acos(angleOfSwipe) * Mathf.Rad2Deg;

                // Detect left and right swipe
                if (angleOfSwipe < mAngleRange)
                {
                    currentSwipe = Swipe.Right;
                    return;
                }
                else if ((180.0f - angleOfSwipe) < mAngleRange)
                {
                    currentSwipe = Swipe.Left;
                    return;
                }
                else
                {
                    // Detect top and bottom swipe
                    angleOfSwipe = Vector2.Dot(swipeVector, mYAxis);
                    angleOfSwipe = Mathf.Acos(angleOfSwipe) * Mathf.Rad2Deg;
                    if (angleOfSwipe < mAngleRange)
                    {
                        currentSwipe = Swipe.Up;
                        return;
                    }
                    else if ((180.0f - angleOfSwipe) < mAngleRange)
                    {
                        currentSwipe = Swipe.Down;
                        return;
                    }
                    else
                    {
                        currentSwipe = Swipe.None;
                    }
                }
            }
        }

        currentSwipe = Swipe.None;
    }

    public static bool GetSwipe(Swipe swipe)
    {
        return (Instance.currentSwipe == swipe);
    }
}
