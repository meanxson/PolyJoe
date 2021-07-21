using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MultiTargetCam : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothTime;
    [SerializeField] private List<Transform> _targets;
    
    private Vector3 _velocity;
    private ObservableCollection<Transform> _observableCollection { get; set; }

    private void Awake()
    {
        _observableCollection = new ObservableCollection<Transform>(_targets);
        
        //Subscribe to event when _targets is changed
        _observableCollection.CollectionChanged += (sender, args) =>
        {
            StartCoroutine(Move());
        };
    }

    private IEnumerator Move()
    {
        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = centerPoint + offset;
        Vector3 dis = transform.position - newPosition;
        
        while (dis.sqrMagnitude > 0.1f*0.1f)
        {
            transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref _velociy, smoothTime);
            yield return new WaitForEndOfFrame();
        }
    }

    private Vector3 GetCenterPoint()
    {
        if (_observableCollection.Count == 1)
            return _observableCollection[0].position;
        
        var bounds = new Bounds(_observableCollection[0].position, Vector3.zero);
        foreach (var t in _observableCollection) 
            bounds.Encapsulate(t.position);
        
        return bounds.center;
    }
}
