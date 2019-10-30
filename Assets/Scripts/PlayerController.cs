using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public Vector3 spawnPoint;
	
    //1 = direita; 2 = esquerda
  
    public int direcao = 1;
    public float speed = 0;
    public float countDown = 1.0f;
    public KeyCode key = KeyCode.W;
    public float maxJumpForce = 400;
    public float jumpGrowth = 5;

    private Rigidbody2D m_Rigidbody2D;
    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    const float k_WallRadius = .2f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded;            // Whether or not the player is grounded.
    private Vector3 m_Velocity = Vector3.zero;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
    [SerializeField] private float m_JumpForce = 400f;							// Amount of force added when the player jumps.
    [SerializeField] private Transform m_GroundCheck1;							// A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_GroundCheck2;							// A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_GroundCheck3;							// A position marking where to check if the player is grounded.
    [SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
    [SerializeField] private Transform m_WallCheckR1;							// A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_WallCheckR2;							// A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_WallCheckR3;							// A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_WallCheckL1;							// A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_WallCheckL2;							// A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_WallCheckL3;							// A position marking where to check if the player is grounded.
    [SerializeField] private LayerMask m_WhatIsWall;							// A mask determining what is ground to the character
    private float defaultSpeed;
    private bool charging = false;
	private float defaultJumpForce;
	private float defaultCountDown;

    private void Awake()
    {
	    spawnPoint = this.gameObject.transform.position;
	    
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        defaultSpeed = speed;
        defaultJumpForce = m_JumpForce;
		defaultCountDown = countDown;
	}

    // Update is called once per frame
    void Update()
    {
	    Debug.Log(spawnPoint);
        if (m_Grounded)
        {
            // Move the character by finding the target velocity
            if (direcao == 1)
            {
                Vector3 targetVelocity = new Vector2(speed, m_Rigidbody2D.velocity.y);
                // And then smoothing it out and applying it to the character
                m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
            }
            else if(direcao == 2)
            {
                Vector3 targetVelocity = new Vector2(-speed, m_Rigidbody2D.velocity.y);
                // And then smoothing it out and applying it to the character
                m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
            }
        }
        if (Input.GetKeyDown(key) && m_Grounded)
        {
			//countDown -= Time.deltaTime;
			//if (countDown < 0)
			//{
			//    charging = true;
			//}
			m_Grounded = false;
			m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_JumpForce);
			//m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
		}
   //     if (Input.GetKeyUp(key) && m_Grounded)
   //     {
   //         charging = false;
   //         m_Grounded = false;
			//m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
   //         m_JumpForce = defaultJumpForce;
   //         countDown = defaultCountDown;
   //     }
        else if (Input.GetKeyDown(key) && !m_Grounded)
        {
            Collider2D[] collidersWallR1 = Physics2D.OverlapCircleAll(m_WallCheckR1.position, k_WallRadius, m_WhatIsWall);
            for (int i = 0; i < collidersWallR1.Length; i++)
            {
                if (collidersWallR1[i].gameObject != gameObject)
                {
                    if (direcao == 1)
                    {
                        direcao = 2;
                        Vector3 targetVelocity = new Vector2(-speed, 0);
                        // And then smoothing it out and applying it to the character
                        m_Rigidbody2D.velocity = targetVelocity;
						m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_JumpForce);
					}
                    break;
                }
            }
			Collider2D[] collidersWallR2 = Physics2D.OverlapCircleAll(m_WallCheckR2.position, k_WallRadius, m_WhatIsWall);
			for (int i = 0; i < collidersWallR2.Length; i++)
			{
				if (collidersWallR2[i].gameObject != gameObject)
				{
					if (direcao == 1)
					{
						direcao = 2;
						Vector3 targetVelocity = new Vector2(-speed, 0);
						// And then smoothing it out and applying it to the character
						m_Rigidbody2D.velocity = targetVelocity;
						m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_JumpForce);
					}
					break;
				}
			}
			Collider2D[] collidersWallR3 = Physics2D.OverlapCircleAll(m_WallCheckR3.position, k_WallRadius, m_WhatIsWall);
			for (int i = 0; i < collidersWallR3.Length; i++)
			{
				if (collidersWallR3[i].gameObject != gameObject)
				{
					if (direcao == 1)
					{
						direcao = 2;
						Vector3 targetVelocity = new Vector2(-speed, 0);
						// And then smoothing it out and applying it to the character
						m_Rigidbody2D.velocity = targetVelocity;
						m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_JumpForce);
					}
					break;
				}
			}
			Collider2D[] collidersWallL1 = Physics2D.OverlapCircleAll(m_WallCheckL1.position, k_WallRadius, m_WhatIsWall);
            for (int i = 0; i < collidersWallL1.Length; i++)
            {
                if (collidersWallL1[i].gameObject != gameObject)
                {
                    if (direcao == 2)
                    {
                        direcao = 1;
                        Vector3 targetVelocity = new Vector2(speed, 0);
                        // And then smoothing it out and applying it to the character
                        m_Rigidbody2D.velocity = targetVelocity;
						m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_JumpForce);
					}
                    break;
                }
            }
			Collider2D[] collidersWallL2 = Physics2D.OverlapCircleAll(m_WallCheckL2.position, k_WallRadius, m_WhatIsWall);
			for (int i = 0; i < collidersWallL2.Length; i++)
			{
				if (collidersWallL2[i].gameObject != gameObject)
				{
					if (direcao == 2)
					{
						direcao = 1;
						Vector3 targetVelocity = new Vector2(speed, 0);
						// And then smoothing it out and applying it to the character
						m_Rigidbody2D.velocity = targetVelocity;
						m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_JumpForce);
					}
					break;
				}
			}
			Collider2D[] collidersWallL3 = Physics2D.OverlapCircleAll(m_WallCheckL3.position, k_WallRadius, m_WhatIsWall);
			for (int i = 0; i < collidersWallL3.Length; i++)
			{
				if (collidersWallL3[i].gameObject != gameObject)
				{
					if (direcao == 2)
					{
						direcao = 1;
						Vector3 targetVelocity = new Vector2(speed, 0);
						// And then smoothing it out and applying it to the character
						m_Rigidbody2D.velocity = targetVelocity;
						m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_JumpForce);
					}
					break;
				}
			}
		}

        //if (charging)
        //{
        //    if(m_JumpForce <= maxJumpForce)
        //    {
        //        m_JumpForce += jumpGrowth;
        //    }
        //}

        //if (Input.GetKeyDown(key) && m_Grounded)
        //{
        //    m_Grounded = false;
        //    m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        //}
        
    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders1 = Physics2D.OverlapCircleAll(m_GroundCheck1.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders1.Length; i++)
        {
            if (colliders1[i].gameObject != gameObject)
            {
                m_Grounded = true;
            }
        }
		//Collider2D[] colliders2 = Physics2D.OverlapCircleAll(m_GroundCheck2.position, k_GroundedRadius - 0.1f, m_WhatIsGround);
		//for (int i = 0; i < colliders2.Length; i++)
		//{
		//	if (colliders2[i].gameObject != gameObject)
		//	{
		//		m_Grounded = true;
		//	}
		//}
		//Collider2D[] colliders3 = Physics2D.OverlapCircleAll(m_GroundCheck3.position, k_GroundedRadius - 0.1f, m_WhatIsGround);
		//for (int i = 0; i < colliders3.Length; i++)
		//{
		//	if (colliders3[i].gameObject != gameObject)
		//	{
		//		m_Grounded = true;
		//	}
		//}
		if (m_Grounded)
        {
            Collider2D[] collidersWallR = Physics2D.OverlapCircleAll(m_WallCheckR1.position, k_GroundedRadius, m_WhatIsWall);
            for (int i = 0; i < collidersWallR.Length; i++)
            {
                if (collidersWallR[i].gameObject != gameObject)
                {
                    if (direcao == 1)
                    {
                        direcao = 2;
                    }
                    break;
                }
            }
			Collider2D[] collidersWallL = Physics2D.OverlapCircleAll(m_WallCheckL1.position, k_GroundedRadius, m_WhatIsWall);
            for (int i = 0; i < collidersWallL.Length; i++)
            {
                if (collidersWallL[i].gameObject != gameObject)
                {
                    if (direcao == 2)
                    {
                        direcao = 1;
                    }
                    break;
                }
            }
        }
    }

    private void setSpeed()
    {
        speed = defaultSpeed;
    }
}
