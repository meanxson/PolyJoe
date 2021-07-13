using UnityEngine;

public class JoeSpawnPoint : MonoBehaviour
{
    private void Awake()
    {
        Joe.This.transform.position = transform.position;
    }
}