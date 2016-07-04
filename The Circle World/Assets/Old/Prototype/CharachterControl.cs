using UnityEngine;
using System.Collections;

public class CharachterControl : MonoBehaviour {

    public Transform body;
    public float turnSpeed; 

    public float m_MaxAngularVelocity = 25; // Максимальная скорость падения
    public float m_JumpPower = 2; // The force added to the ball when it jumps.
    public float m_Speed = 0.1f;
    public float autoSpeedY = 0;
    public LayerMask maskGround;

    private const float k_GroundRayLength = 0.7f; // The length of the ray to check if the ball is grounded.
    private const float jumpTime = 0.2f; // The length of the ray to check if the ball is grounded.
    
    private Rigidbody m_Rigidbody;
    private bool jump;
    private bool isDown;


    private float fingerStartTime = 0.0f;
    private Vector2 fingerStartPos = Vector2.zero;

    private bool isSwipe = false;
    private float minSwipeDist = 50.0f;
    private float maxSwipeTime = 0.5f;





    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        GetComponent<Rigidbody>().maxAngularVelocity = m_MaxAngularVelocity;
    }

    void Update()
    {

        float h = Input.acceleration.x;   // CrossPlatformInputManager.GetAxis("Horizontal");
        float v = 0;// autoSpeedY == 0 ? CrossPlatformInputManager.GetAxis("Vertical") : autoSpeedY;


        bool isUp = false;
       

        bool doJump = isUp && !jump; //CrossPlatformInputManager.GetButtonDown("Jump") && !jump;
                       // CrossPlatformInput.GetSwipe(Swipe.Up) && !jump;

    /*   if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            isDown = true;
            GetComponent<Animator>().SetTrigger("Down");
        }  */

        if (Physics.Raycast(transform.position, -Vector3.up, k_GroundRayLength,maskGround) && doJump)
        {
            jump = true;
            Invoke("StopJump", jumpTime);
            m_Rigidbody.AddForce(Vector3.up * m_JumpPower, ForceMode.Impulse);
        }


        // Двигаемся
        m_Rigidbody.transform.Translate(new Vector3(h * m_Speed * Time.deltaTime, 0, v * m_Speed * Time.deltaTime));
    
        //вращаем тело
        if (isDown)
            body.Rotate(new Vector3(turnSpeed * Time.deltaTime, 0, 0));



      
    }


    /// <summary>
    /// защита от продолжительного прыжка
    /// </summary>
    void StopJump()
    {
        jump = false;
    }
}
