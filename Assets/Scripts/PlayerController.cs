using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
   /*void Update()
   {
   }*/
   
   public float speed;
   int count;
   
   void Start()
   {
   }

   void FixedUpdate()
   {
      float moveHorizontal = Input.GetAxis("Horizontal");
      float moveVertical = Input.GetAxis("Vertical");
      
      var movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
      this.rigidbody.AddForce(movement * this.speed * Time.deltaTime);
   }
}

