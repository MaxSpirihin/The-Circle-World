using UnityEngine;
using System.Collections;


public enum PlayerStandartControlType
{
    Discrete,
    Continous
}


public class PlayerControl : MonoBehaviour {

    //основные параметры
    public PlayerStandartControlType controlType;
    public float AutoSpeedForward = 0;
    
    //параметры дискретного движения
    public float D_Speed = 1f;
    public float D_Length = 1f;
    public int D_Max = 1;
    public int D_Min = -1;

    //параметры прыжка
    public float JumpPower = 20;
    public LayerMask maskGround;

    //параметры прокрутки
    public float downTime = 2;
    public float jumpToDownPower = 10;
    public float downSpeedMultiplier = 1.5f;
    public Transform body;
    public float turnSpeed = 2f;

    //параметры смерти
    
    

    //приватные константы
    private const float D_Distance = 0.2f;
    private const float k_GroundRayLength = 0.7f; 
    private const float jumpTime = 0.2f;
    
    private Rigidbody rigidbody;
    private Animator animator;
    private bool jump;
    private bool isDown;
    private bool move = false;
    private int D_Number = 0;
    private float D_StartPos;
    

	void Start () {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        D_StartPos = rigidbody.transform.position.x;

        animator.SetTrigger("Step");
        move = true;
	}
	
	void Update () {

        if (!move)
            return;

        //двигаемся горизонтально
        if (controlType == PlayerStandartControlType.Discrete)
            HorizontalMoveDiscrete();

        // Двигаемся вперед
        rigidbody.transform.Translate(new Vector3(0, 0, AutoSpeedForward*Time.deltaTime * (isDown ? downSpeedMultiplier : 1)));

        //прыжок
        bool isGrounded = Physics.Raycast(transform.position, -Vector3.up, k_GroundRayLength, maskGround) ;
        animator.SetBool("IsGrounded", isGrounded);
        if (!jump && InputManager.GetSwipe(Swipe.Up) && isGrounded)
        {
            jump = true;
            isDown = false;
            CancelInvoke("StopDown");
            Invoke("StopJump", jumpTime);
            rigidbody.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
        }


        //прокатка
        if (InputManager.GetSwipe(Swipe.Down) && !isDown)
        {
            if (!isGrounded)
                rigidbody.AddForce(-Vector3.up * jumpToDownPower, ForceMode.Impulse);

            isDown = true;
            Invoke("StopDown", downTime);
        }
        animator.SetBool("Down", isDown);

        if (isDown)
            body.Rotate(new Vector3(turnSpeed * Time.deltaTime, 0, 0));
        else if (body.eulerAngles.x > 1 || body.eulerAngles.z > 1)
            body.Rotate(new Vector3(Mathf.Min(turnSpeed * Time.deltaTime,360 -  body.eulerAngles.x), 0, 0));
    }


    private void HorizontalMoveDiscrete()
    {
        //высчитываем необходимую позицию по x:
        float targetX = D_StartPos + D_Number * D_Length;

        //если нужно пододвинуться до нужной дорожки, двигаемся
        if (Mathf.Abs(rigidbody.transform.position.x - targetX) > D_Distance)
        {
            rigidbody.transform.Translate(new Vector3(Mathf.Sign(rigidbody.transform.position.x - targetX) * D_Speed* Time.deltaTime, 0, 0));
        }
        //если мы на нужной дорожке обрабатываем ввод
        else
        {
            if (InputManager.GetSwipe(Swipe.Left) && D_Number > D_Min)
                D_Number--;
            if (InputManager.GetSwipe(Swipe.Right) && D_Number < D_Max)
                D_Number++;
        }
    }


    public void Kill()
    {
        move = false;
        animator.SetTrigger("Dead");
        Respawner.Respawn(1.5f);
    }



    /// <summary>
    /// защита от продолжительного прыжка
    /// </summary>
    void StopJump()
    {
        jump = false;
    }


    /// <summary>
    /// прерывание прокрутки
    /// </summary>
    void StopDown()
    {
        isDown = false;
    }
}
