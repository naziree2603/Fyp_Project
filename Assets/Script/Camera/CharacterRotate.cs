using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterRotate : MonoBehaviour
{
    public float rotateSpeed = 200f;

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxis("Mouse X");
            transform.Rotate(0, -mouseX * rotateSpeed * Time.deltaTime, 0);
        }
    }
}

