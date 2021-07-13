using System;
using UnityEngine;

namespace _Scripts
{
    public class Parallax : MonoBehaviour
    {
        private Vector2 _length, _startPos;
        public Camera cam;
        public float parallaxEffectX;
        public float parallaxEffectY;
        private void Start()
        {
            _startPos = new Vector2(
                transform.position.x,
                transform.position.y);

            _length = new Vector2(
                GetComponent<SpriteRenderer>().bounds.size.x,
                GetComponent<SpriteRenderer>().bounds.size.y);
        }

        private void FixedUpdate()
        {
            Vector2 temp = new Vector2(
                cam.transform.position.x * (1 - parallaxEffectX),
                cam.transform.position.y * (1 - parallaxEffectY));

            Vector2 dist = new Vector2(
                cam.transform.position.x * parallaxEffectX,
                cam.transform.position.y * parallaxEffectY
                );

            transform.position = new Vector3(_startPos.x + dist.x, _startPos.y + dist.y, transform.position.z);

            if (temp.x > _startPos.x + _length.x) _startPos += new Vector2(_length.x, 0);
            else if (temp.x < _startPos.x - _length.x) _startPos -= new Vector2(_length.x, 0);
        }
    }
}
