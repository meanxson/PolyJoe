using UnityEngine;

public class JoeSpawnPoint : MonoBehaviour
{
    private void Awake()
    {
        //wtf
        Joe.This.transform.position = transform.position;
    }
}
