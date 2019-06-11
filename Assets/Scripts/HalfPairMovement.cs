using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HalfPairMovement : MonoBehaviour
{

    [Header("Movement Variables")]
    public float baseSpeed = 5f;
    public float moveSpeed;
    public static Vector2 motion;
    public static Vector2 direction = new Vector2(0, -1);
    public static bool CanMove = true;
    private Animator anim;
    private SpriteRenderer rend;
    public static Rigidbody2D rb;

    void Start()
    {
        anim = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
