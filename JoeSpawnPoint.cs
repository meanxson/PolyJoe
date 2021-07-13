using UnityEngine;

public class JoeSpawnPoint : MonoBehaviour
{
    public Joe joe;
    private void Awake()
    {
        joe.transform.position = transform.position;
    }
}
