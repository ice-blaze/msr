using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ScreenToWorldAlignment : MonoBehaviour 
{
	public enum Index {First, Second, Third};
	public Index index; 

	void Update () 
	{
		Vector3 tempScreenPosition = new Vector3 (0, 0, -Camera.main.transform.position.z);
		
		Vector3 worldPosition = Camera.main.ScreenToWorldPoint(tempScreenPosition);
		worldPosition.x -= renderer.bounds.size.x;// * tempScreenPosition.x / Screen.width;
		worldPosition.y += renderer.bounds.size.y;// * (1 - tempScreenPosition.y / Screen.height);
		worldPosition.x *= 0.5f;
		worldPosition.y *= 0.5f;

		if (index == Index.First)
        {
            worldPosition.y += renderer.bounds.size.y;
        } else if (index == Index.Third)
        {
            worldPosition.y -= renderer.bounds.size.y;
        }

		worldPosition.z += 5*renderer.bounds.size.y;
		transform.position = worldPosition;
	}
}