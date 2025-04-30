using UnityEngine;

namespace Jugador
{
    public class Movement : MonoBehaviour
    {
        
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
         
         //Aplicar gravedad
         if (controller.isGrounded && velocity.y < 0)
         {
             velocity.y = -2f;
         }
         
         controller.Move(velocity * Time.deltaTime);
        }
    }
}