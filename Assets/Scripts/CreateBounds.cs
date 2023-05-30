using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBounds : MonoBehaviour
{
    public float thick;
    public static float xMax;
    public static float xMin;
    public static float yMax;
    public static float yMin;


    GameObject[] bounds;
    float orthographicSize;
    
    void Awake()
    {
        SetBounds();
    }

    void SetBounds() {

        if (bounds != null) {
            foreach (GameObject b in bounds)
                Destroy(b);
        }

        if (Camera.main.orthographic)
        {
            bounds = new GameObject[4];

            float horizontal = Camera.main.orthographicSize * Screen.width / Screen.height;
            float vertical = horizontal * Screen.height / Screen.width;

            bounds[0] = SetBound(new Vector3(horizontal * 2, thick, thick), new Vector3(0, vertical, 0));
            bounds[1] = SetBound(new Vector3(horizontal * 2, thick, thick), new Vector3(0, -vertical, 0));
            bounds[2] = SetBound(new Vector3(thick, vertical * 2, thick), new Vector3(horizontal, 0, 0));
            bounds[3] = SetBound(new Vector3(thick, vertical * 2, thick), new Vector3(-horizontal, 0, 0));

            xMax = horizontal - thick;
            xMin = -xMax;
            yMax = vertical - thick;
            yMin = -yMax;

            orthographicSize = Camera.main.orthographicSize;
        }
    }

    
    GameObject SetBound(Vector3 localScale, Vector3 position)
    {
        GameObject bound = GameObject.CreatePrimitive(PrimitiveType.Cube);
        bound.layer = LayerMask.NameToLayer("HardCollision");
        bound.transform.localScale = localScale;
        bound.transform.position = Camera.main.transform.position + position + new Vector3(0,0, -Camera.main.transform.position.z);
        bound.transform.parent = transform;

        return bound;
    }

    private void Update()
    {
        if (orthographicSize != Camera.main.orthographicSize) {
            SetBounds();
        }
    }
}
