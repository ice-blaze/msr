// Center 3DText on the screen, independently the resolution.

using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ScreenToWorldAlignment : MonoBehaviour 
{
    // TODO More button than just 3
	public enum Index {StarMenu, LevelSelection};
	public Index index; 

	void Start () 
	{
		Vector3 tempScreenPosition = new Vector3 (0, 0, -Camera.main.transform.position.z);
		
		Vector3 worldPosition = Camera.main.ScreenToWorldPoint(tempScreenPosition);
//		worldPosition.x -= renderer.collider.bounds.size.x;
//		worldPosition.y += renderer.collider.bounds.size.y;
//		worldPosition.x *= 0.5f;
//		worldPosition.y *= 0.5f;

        switch(index)
        {
			case Index.StarMenu:
				worldPosition.z += 2.5f*collider.bounds.size.y;
				break;
			case Index.LevelSelection:
				worldPosition.z += 3.0f*collider.bounds.size.y;
				break;
            default: 
                break;
        }


		transform.position = worldPosition;
	}
}