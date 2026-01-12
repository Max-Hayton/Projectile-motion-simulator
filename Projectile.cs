using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Projectile_motion_simulator
{
    internal class Projectile
    {
        private float _displacement;
        private float _initialVelocity;
        private float _finalVelocity;
        private Vector2 _acceleration;
        private float _flightTime;
        private float _angle;

        private Texture2D _projectileTexture;
        private Vector2 _position;
        private Color _projectileColour;
        private Vector2 _componentsOfVelocity;
        private Vector2 _currentVelocity;

        public Projectile()
        { }

        public Projectile(Texture2D projectileTexture, Vector2 position, Color projectileColour, float displacement, float initialVelocity,float finalVelocity, Vector2 acceleration, float flightTime, float angle)
        {
            _projectileTexture = projectileTexture;
            _position = position;
            _projectileColour = projectileColour;

            _displacement = displacement;
            _initialVelocity = initialVelocity;
            _finalVelocity = finalVelocity;
            _acceleration = acceleration;
            _flightTime = flightTime;
            _angle = angle;
        }
        public void Launch()
        {
            _currentVelocity = GetComponents(); // initial velocity vector
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_projectileTexture, _position, _projectileColour);
            _displacement = _flightTime * ((_initialVelocity + _finalVelocity) / 2);
        }

        public Vector2 GetComponents()
        {
            double angleRadians = _angle * Math.PI / 180.0; // convert to radians for Math functions
            float horizontal = (float)(Math.Cos(angleRadians) * _initialVelocity);
            float vertical = -1*((float)(Math.Sin(angleRadians) * _initialVelocity));
            _componentsOfVelocity = new Vector2(horizontal,vertical); //creates the launch vector
            return _componentsOfVelocity;
        }

        public void Movement(float deltaTime,float gravity,int screenWidth,int screenHeight)
        {
            // Apply gravity
            _currentVelocity.Y += gravity * deltaTime;

            // Update position
            _position += _currentVelocity * deltaTime;

            //stop at ground
            float ground = screenHeight - 100;
            float degreeOfRestitution = 0.8f;
            if (_position.Y > ground)
            {
                _position.Y = ground;
                _currentVelocity.Y *= -degreeOfRestitution;
            }

            //stop at wall
            float wall = screenWidth - 50;
            if (_position.X > wall)
            {
                _position.X = wall;
                _currentVelocity.X *= -degreeOfRestitution;
            }

            if (_position.X < 0)
            {
                _position.X = 0;
                _currentVelocity.X *= -degreeOfRestitution;
            }
            //stop at ceiling
            float ceiling = 0;
            if(_position.Y < 0)
            {
                _position.Y = ceiling;
                _currentVelocity.Y *= -degreeOfRestitution;
            }
        }



    }

}
