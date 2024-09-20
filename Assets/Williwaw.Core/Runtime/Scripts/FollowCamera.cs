using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;

    public void Update()
    {
        transform.position = _target.position + _offset;
    }
}
