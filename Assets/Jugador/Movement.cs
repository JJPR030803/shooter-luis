using UnityEngine;

namespace Jugador
{
    public class Movement : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float speed = 10f;
        [SerializeField] private float gravity = -9.18f;
        
        private CharacterController controller;
        private Vector3 velocity;
        
        private void Start()
        {
            controller = GetComponent<CharacterController>();
        }
        
        private void Update()
        {
         float horizontalInput = Input.GetAxis("Horizontal");
         float verticalInput = Input.GetAxis("Vertical");
         
         //Vector de movimiento
         Vector3 movement = transform.right * horizontalInput + transform.forward * verticalInput;
         
         //Aplicar movimiento 
         controller.Move(movement * speed * Time.deltaTime);
         
         //Aplicar gravedad
         if (controller.isGrounded && velocity.y < 0)
         {
             velocity.y = -2f;
         }
         
         velocity.y += gravity * Time.deltaTime;
         controller.Move(velocity * Time.deltaTime);
        }
    }
}