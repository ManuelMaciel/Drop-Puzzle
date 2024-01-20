﻿using System;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic
{
    public class Movement : MonoBehaviour
    {
        public event Action OnShapeDropped;

        private Rigidbody2D _shapeRigidbody;
        private IInput _input;
        private bool _isDropped;

        [Inject]
        public void Construct(IInput input)
        {
            _input = input;
        }

        private void Update()
        {
            if(_isDropped) return;

            if (_input.IsPress())
            {
                _shapeRigidbody.position = new Vector2(_input.GetXPosition(), 
                    _shapeRigidbody.position.y);
            }

            if (_input.IsDropped())
            {
                OnShapeDropped?.Invoke();

                _shapeRigidbody.bodyType = RigidbodyType2D.Dynamic;
                _isDropped = true;
            }
        }

        public void AddShape(Rigidbody2D newShape)
        {
            _shapeRigidbody = newShape;
            _isDropped = false;
        }
    }
}