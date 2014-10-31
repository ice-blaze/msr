// Center 3DText on the screen, independently the resolution.

using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ScreenToWorldAlignment : MonoBehaviour 
{
    // TODO More button than just 3
	public enum Index {First, Second, Third};
	public Index index; 

	void Update () 
	{
		Vector3 tempScreenPosition = new Vector3 (0, 0, -Camera.main.transform.position.z);
		
		Vector3 worldPosition = Camera.main.ScreenToWorldPoint(tempScreenPosition);
		worldPosition.x -= renderer.bounds.size.x;
		worldPosition.y += renderer.bounds.size.y;
		worldPosition.x *= 0.5f;
		worldPosition.y *= 0.5f;

        switch(index)
        {
            case Index.First: worldPosition.y += renderer.bounds.size.y; 
                break;
            case Index.Third: worldPosition.y -= renderer.bounds.size.y;
                break;
            default: 
                break;
        }

		worldPosition.z += 5*renderer.bounds.size.y;
		transform.position = worldPosition;
	}
}