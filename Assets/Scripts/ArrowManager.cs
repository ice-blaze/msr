using UnityEngine;
using System.Collections.Generic;

public class ArrowManager : MonoBehaviour
{
	public class PosRot
	{
		public Vector3 position;
		public Quaternion rotation;

		public PosRot(Vector3 pos, Quaternion rot)
		{
			position = pos;
			rotation = rot;
		}

		public void setTransform(Transform t)
		{
			position = t.position;
			rotation = t.rotation;
		}
	}

   List<Transform> checkpoints = new List<Transform>();
	PosRot lastCheckpoint;
   GameObject arrow;
   float baseZscale; // Base local scale.
	VehicleManager playerController;

   void Start()
   {
		playerController = GetComponentInParent<VehicleManager>();

      this.arrow = GameObject.Find("Arrow");
      this.baseZscale = arrow.transform.localScale.x;

		lastCheckpoint =  new PosRot(arrow.transform.position, arrow.transform.rotation);

      GameObject checkpoint = GameObject.Find("CheckPoints");
      if (checkpoint != null)
      {
         foreach (Transform g in checkpoint.transform)
         checkpoints.Add(g);
      }
   }

   void Update()
   {
      if (checkpoints.Count > 0)
      {
         Vector3 v = checkpoints [0].transform.position;
         v.y = transform.position.y;
         float distance = Vector3.Distance (v, arrow.transform.position);
         arrow.transform.localScale = new Vector3 (arrow.transform.localScale.x, arrow.transform.localScale.y, Mathf.Min (baseZscale * (1 + distance / 100.0f), baseZscale * 3));
		 arrow.transform.LookAt (v);
      }
   }

   public void RemoveCheckPoint(Collider other)
   {
      if (checkpoints.Count > 0 && other.transform.Equals (checkpoints [0]))
      {
        lastCheckpoint.setTransform(playerController.transform);
         checkpoints.RemoveAt (0);
      }
   }

   public bool PassTroughAllCheckPoints()
   {
      return checkpoints.Count == 0;
   }

	public PosRot getLastCheckpoint()
	{
		return lastCheckpoint;
	}
}
