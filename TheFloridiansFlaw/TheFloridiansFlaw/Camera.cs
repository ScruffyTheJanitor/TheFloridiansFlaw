using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheFloridiansFlaw
{
    public class Camera
    {
        protected float _zoom;
        protected Matrix _transform;
        protected Matrix _inverseTransform;
        public Vector2 _pos;
        protected float _rotation;
        protected Viewport _viewport;
        protected MouseState _mState;
        protected KeyboardState _keyState;
        protected Int32 _scroll;

        public float Zoom
        {
            get { return _zoom; }
            set { _zoom = value; }
        }

        public Matrix Transform
        {
            get { return _transform; }
            set { _transform = value; }
        }

        public Matrix InverseTransform
        {
            get { return _inverseTransform; }
        }
        public Vector2 Pos
        {
            get { return _pos; }
            set { _pos = value; }
        }
        public float Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }

        public Camera(Viewport viewport)
        {
            _zoom = 1.0f;
            _scroll = 1;
            _rotation = 0.0f;
            _pos = new Vector2(1280, 0);
            _viewport = viewport;
        }

        public void Update()
        {
            Input();
            _zoom = MathHelper.Clamp(_zoom, 0.0f, 10.0f);
            _rotation = ClampAngle(_rotation);
            _transform = Matrix.CreateRotationZ(_rotation) *
                            Matrix.CreateScale(new Vector3(_zoom, _zoom, 1)) *
                            Matrix.CreateTranslation(_pos.X, _pos.Y, 0);
            _inverseTransform = Matrix.Invert(_transform);
        }

        protected virtual void Input()
        {
            _mState = Mouse.GetState();
            _keyState = Keyboard.GetState();
            //Check zoom
            /*if (_mState.ScrollWheelValue > _scroll)
            {
                _zoom += 0.1f;
                _scroll = _mState.ScrollWheelValue;
            }
            else if (_mState.ScrollWheelValue < _scroll)
            {
                _zoom -= 0.1f;
                _scroll = _mState.ScrollWheelValue;
            }
            //Check rotation
            if (_keyState.IsKeyDown(Keys.Left))
            {
                _rotation -= 0.1f;
            }
            if (_keyState.IsKeyDown(Keys.Right))
            {
                _rotation += 0.1f;
            }
            //Check Move
            if (_keyState.IsKeyDown(Keys.A))
            {
                //_pos.X += 0.5f;
                _pos.X += 2.0f;
            }
            if (_keyState.IsKeyDown(Keys.D))
            {
                //_pos.X -= 0.5f;
                _pos.X -= 2.0f;
            }
            if (_keyState.IsKeyDown(Keys.W))
            {
                //_pos.Y += 0.5f;
                _pos.Y += 2.0f;
            }
            if (_keyState.IsKeyDown(Keys.S))
            {
                //_pos.Y -= 0.5f;
                _pos.Y -= 2.0f;
            }*/
        }

        protected float ClampAngle(float radians)
        {
            while (radians < -MathHelper.Pi)
            {
                radians += MathHelper.TwoPi;
            }
            while (radians > MathHelper.Pi)
            {
                radians -= MathHelper.TwoPi;
            }
            return radians;
        }
    }
}