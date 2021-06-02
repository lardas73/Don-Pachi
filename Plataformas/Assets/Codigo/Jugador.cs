using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    public int fuerzaSalto;

    public bool enElPiso = false, MirarDerecha = true, perdiste = false, siquienteNivel = false;

    private Rigidbody2D _rigidbody;

    public float velocidad, revoteEnemigo, alturaPerdiste;

    private Animator _animator;

    public AudioSource Salto, Caida;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        ControlarMovimiento(horizontal);
        Voltear(horizontal);

        if (Input.GetKeyDown("space") && enElPiso)
        {
            Salto.Play();
            _rigidbody.AddForce(new Vector2(0, fuerzaSalto));
        }
    }

    private void ControlarMovimiento(float horizontal)
    {
        _rigidbody.velocity = new Vector2(horizontal * velocidad, _rigidbody.velocity.y);
        _animator.SetFloat("velocidad", Mathf.Abs(horizontal));
    }
    
    private void Voltear(float horizontal)
    {
        
    }
}
