using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
   public GameObject player;
   Vector3 offset;
   
   // Use this for initialization
   void Start()
   {
      this.offset = this.transform.position - this.player.transform.position;
   }
   
   // Update is called once per frame
   void LateUpdate()
   {
      this.transform.position = this.player.transform.position + this.offset;
   }
}
