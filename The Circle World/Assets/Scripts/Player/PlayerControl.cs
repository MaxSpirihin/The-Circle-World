using UnityEngine;
using System.Collections;


/// <summary>
/// тип управления персонажем 
/// Дискретный - свайпы
/// Непрерывный - акселлерометр
/// </summary>
public enum PlayerStandartControlType
{
    Discrete,
    Continous
}


public class PlayerControl : MonoBehaviour,IRespawnListener {

    //основные параметры
    public PlayerStandartControlType controlType;
    public float AutoSpeedForward = 0;
    public AudioClip dashSound;
    
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
    public Transform startPosition;
    
    //приватные константы
    private const float D_Distance = 0.2f;
    private const float k_GroundRayLength = 0.7f; 
    private const float jumpTime = 0.2f;
    
    private Rigidbody m_rigidbody;
    private AudioSource source;
    private Animator animator;
    private CameraToPlayer camera;
    private bool jump = false;
    private bool isDown;
    private bool move = false;
    private int D_Number = 0;
    private float D_StartPos;
    private bool blockHorizontal;
    

	void Start () {
        m_rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        camera = GameObject.FindObjectOfType<Camera>().GetComponent<CameraToPlayer>();
	}


    public void StartMove()
    {
        animator.SetTrigger("Step");
        move = true;
    }

    /// <summary>
    /// запретить горизонтальное движение
    /// </summary>
    public void BlockHorizontal(bool block)
    {
        blockHorizontal = block;
        if (controlType == PlayerStandartControlType.Discrete && block)
            D_Number = 0;
    }


	void Update () {

        //обновление аниматора
        bool isGrounded = Physics.Raycast(transform.position, -Vector3.up, k_GroundRayLength, maskGround);
        animator.SetBool("IsGrounded", isGrounded);

        if (!move)
            return;

        //двигаемся горизонтально
        if (controlType == PlayerStandartControlType.Discrete)
            HorizontalMoveDiscrete();

        // Двигаемся вперед
        m_rigidbody.transform.position = new Vector3(transform.position.x, transform.position.y,
            transform.position.z - AutoSpeedForward * Time.deltaTime * (isDown ? downSpeedMultiplier : 1));

        //прыжок
        if (!jump && InputManager.GetSwipe(Swipe.Up) && isGrounded)
        {
            source.PlayOneShot(dashSound);
            jump = true;
            isDown = false;
            CancelInvoke("StopDown");
            Invoke("StopJump", jumpTime);
            m_rigidbody.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
        }


        //прокатка
        if (InputManager.GetSwipe(Swipe.Down) && !isDown)
        {
            source.PlayOneShot(dashSound);
            if (!isGrounded)
                m_rigidbody.AddForce(-Vector3.up * jumpToDownPower, ForceMode.Impulse);

            isDown = true;
            Invoke("StopDown", downTime);
        }
        animator.SetBool("Down", isDown);

        //вращение тела
        if (isDown)
            body.Rotate(new Vector3(turnSpeed * Time.deltaTime, 0, 0));
        else if (body.eulerAngles.x > 1 || body.eulerAngles.z > 1)
            body.Rotate(new Vector3(Mathf.Min(turnSpeed * Time.deltaTime,360 -  body.eulerAngles.x), 0, 0));
    }


    private void HorizontalMoveDiscrete()
    {
        //высчитываем необходимую позицию по x:
        float targetX = startPosition.position.x + D_Number * D_Length;

        //если нужно пододвинуться до нужной дорожки, двигаемся
        if (Mathf.Abs(m_rigidbody.transform.position.x - targetX) > D_Distance)
        {
            m_rigidbody.transform.position = new Vector3(transform.position.x + Mathf.Sign(targetX - m_rigidbody.transform.position.x) * 
                Mathf.Min(D_Speed * Time.deltaTime,Mathf.Abs(targetX - m_rigidbody.transform.position.x)),
                transform.position.y, transform.position.z);
        }
        //если мы на нужной дорожке обрабатываем ввод
        else
        {
            if (InputManager.GetSwipe(Swipe.Left) && D_Number > D_Min && !blockHorizontal)
            {
                D_Number--;
                source.PlayOneShot(dashSound);
            }
            if (InputManager.GetSwipe(Swipe.Right) && D_Number < D_Max && !blockHorizontal)
            {
                source.PlayOneShot(dashSound);
                D_Number++;
            }
        }
    }


    /// <summary>
    /// смерть/поражение
    /// </summary>
    public void Kill()
    {
        //проигрываем анимацию и респавнимся
        move = false;
        camera.enabled = false;
        animator.SetTrigger("Dead");
        Respawner.Respawn(1.5f);
        MusicPlayer.Stop(1.5f);
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

    public void OnRespawn()
    {
        //скмдываем все
        transform.position = startPosition.position;
        D_Number = 0;
        BlockHorizontal(false);
        body.rotation = new Quaternion(0, 0, 0, 0);
        animator.SetTrigger("Respawn");
    }


    
    public void OnRespawnEnd()
    {
        //запуск движения
        camera.enabled = true;
        camera.SetOldPos();
        StartMove();
    }
}
