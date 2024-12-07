using System.Collections;
using UnityEngine;

public class IsometricZMover : MonoBehaviour
{
    public bool Active = true;

    private void Update()
    {
        if (Active)
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
    }
}
