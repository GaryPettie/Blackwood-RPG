using UnityEngine;

[RequireComponent (typeof(Camera))]
public class CameraRaycaster : MonoBehaviour
{
    [SerializeField] private Layer[] layerPriorities = {
        Layer.Enemy,
        Layer.Walkable
    };

    [SerializeField] private float distanceToBackground = 100f;
    Camera viewCamera;

    RaycastHit rayHit;
    public RaycastHit raycastHit
    {
        get { return rayHit; }
    }

    Layer layerHit;
    public Layer currentLayerHit
    {
        get { return layerHit; }
    }


    public delegate void OnLayerChange(Layer newLayer);	//Declare new delegate type
    public event OnLayerChange onLayerChange;			//Instantiate an observer set

    void Start()
    {
        viewCamera = Camera.main;						
    }

    void Update()
    {
        // Look for and return priority layer hit
        foreach (Layer layer in layerPriorities)
        {
            var hit = RaycastForLayer(layer);
            if (hit.HasValue)
            {
				rayHit = hit.Value;
				if (layerHit != layer) { 
					layerHit = layer;
					onLayerChange(layer);				//Call the delegates
				}
                return;
            }
        }

        // Otherwise return background hit
        rayHit.distance = distanceToBackground;
		layerHit = Layer.RaycastEndStop;
		onLayerChange(layerHit);
    }

    RaycastHit? RaycastForLayer(Layer layer)
    {
        int layerMask = 1 << (int)layer; // See Unity docs for mask formation
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit; // used as an out parameter
        bool hasHit = Physics.Raycast(ray, out hit, distanceToBackground, layerMask);
        if (hasHit)
        {
            return hit;
        }
        return null;
    }
}
