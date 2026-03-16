using UnityEngine;

public class destroyThings : MonoBehaviour
{
    [SerializeField] private float lifetime = 1f;
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

   
}
