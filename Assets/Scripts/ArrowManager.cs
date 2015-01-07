using UnityEngine;
using System.Collections.Generic;

public class ArrowManager : MonoBehaviour
{
   List<Transform> checkpoints = new List<Transform>();
   GameObject arrow;
   float baseZscale; // Base local scale.

   void Start()
   {
      this.arrow = GameObject.Find("Arrow");
      this.baseZscale = arrow.transform.localScale.x;

      GameObject checkpoint = GameObject.Find("CheckPoints");
      if (checkpoint != null)
      {
         foreach (Transform g in checkpoint.transform)
            checkpoints.Add(g);
      }
   }
   
   void Update()
   {
      Vector3 v = checkpoints[0].transform.position;
      v.y = transform.position.y;
      float distance = Vector3.Distance(v, arrow.transform.position);
      arrow.transform.localScale = new Vector3(arrow.transform.localScale.x, arrow.transform.localScale.y, Mathf.Min(baseZscale * (1 + distance / 100.0), baseZscale * 3));
      arrow.transform.LookAt(v);
   }

   public void RemoveCheckPoint(Collider other)
   {
      if (checkpoints.Count > 0 && other.transform.Equals (checkpoints [0]))
      {   
         Debug.Log (string.Format("checkpoints.size: {0}", this.checkpoints.Count));
         checkpoints.RemoveAt (0);
         Debug.Log (string.Format("checkpoints.size: {0}", this.checkpoints.Count));
      }
   }

   public bool PassTroughAllCheckPoints()
   {
      return checkpoints.Count == 0;
   }
}
