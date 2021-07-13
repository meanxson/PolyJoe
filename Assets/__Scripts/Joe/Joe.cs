using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joe : MonoBehaviour
{
    public static Joe This;
    private MultiTargetCam _cam;
    private void Awake()
    {
        _cam = Camera.main.GetComponent<MultiTargetCam>();
        _cam.targets.Add(transform);
        This = this;
    }
}
