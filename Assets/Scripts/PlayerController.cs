using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoState<PlayerController>
{

    [Header("Tank")]
    [SerializeField]
    Transform barrel;

    [Header("Movement")]
    [SerializeField]
    float speed = 8.5F;

    [Header("Animation")]
    [SerializeField]
    Animator animator;

    [Header("Combat")]
    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    Transform[] firePoints;

    [SerializeField]
    [Range(0.1F, 1.0F)]
    float fireRate = 0.3F;

    [SerializeField]
    Animator[] wheelAnimators;



    // raya baja porque es un atributo
    Rigidbody2D _rb;

    //constantes dentro de c#

    Camera CAMERA;
    Vector2 _direction;
    Vector2 _mousePosition;


    float _fireTimer;

    protected override void Awake()
    {

        base.Awake();
        CAMERA = Camera.main;
        _rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        //el vector guarda una posicion x / y 
        _direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        _mousePosition = CAMERA.ScreenToWorldPoint(Input.mousePosition);
        HandleBarreRotation();

        //boton 0 = derecho 
        //boton 1 = izquierda

        _fireTimer -= Time.deltaTime;
        if (Input.GetMouseButton(0) && _fireTimer <= 0.0F)
        {
            Shoot();
            _fireTimer = fireRate;
        }

    }

    void Shoot()
    {
        foreach (Transform firePoint in firePoints)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }

    private void FixedUpdate()
    {
        if (_direction.sqrMagnitude == 0)
        {
            return;
        }

        if (animator != null)
        {
            animator.SetFloat("Horizontal", _direction.x);
            animator.SetFloat("Vertical", _direction.y);
            animator.SetFloat("Speed", _direction.sqrMagnitude);

        }

        if (wheelAnimators.Length > 0)
        {
            foreach (Animator wheelAnimator in wheelAnimators)
            {
                wheelAnimator.SetFloat("Speed", _direction.sqrMagnitude);
            }
        }



        _rb.MovePosition(_rb.position + _direction * speed * Time.fixedDeltaTime);
        HandleRotation();
    }

    void HandleRotation()
    {

        float angle = _direction.x > 0 && _direction.y == 0
            ? 90.0F
              : _direction.x > 0 && _direction.y > 0
                ? 45.0F
                  : _direction.x > 0 && _direction.y < 0
                    ? 135.0F
                      : _direction.x == 0 && _direction.y < 0
                        ? 180.0F
                          : _direction.x < 0 && _direction.y < 0
                            ? 225.0F
                              : _direction.x < 0 && _direction.y == 0
                                ? 270.0F
                                  : _direction.x < 0 && _direction.y > 0
                                    ? 315.0F
                                      : 0.0F;
        transform.rotation = Quaternion.Euler(new Vector3(0.0F, 0.0F, -angle));


    }

    void HandleBarreRotation()
    {
        float angle = Mathf.Atan2(_mousePosition.y - barrel.position.y,
            _mousePosition.x - barrel.position.x) * Mathf.Rad2Deg - 90.0F;
        barrel.rotation = Quaternion.Euler(new Vector3(0.0F, 0.0F, angle));
    }


    //Morir por choque de otros tanques
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
            LevelManager.Instance.NextScene();
        }
    }



}
