using System;
using NinjaJump.Character;
using UniRx;
using UnityEngine;

namespace NinjaJump.Environment
{
    /// <summary>
    /// Base behavior of all environment entities
    /// </summary>
    public abstract class EnvironmentObject : MonoBehaviour
    {
        public static IObservable<EnvironmentObject> TargetReached => TargetReachedSubject;

        private static readonly Subject<EnvironmentObject> TargetReachedSubject = new();

        private Transform _target;
        protected IApplication _context;
        private float _speed;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<ICharacter>() == _context.Character)
            {
                TargetReachedSubject.OnNext(this);
                OnCharacterTouchedHandler();
            }
        }

        protected abstract void OnCharacterTouchedHandler();

        public void SetTarget(Transform target) => _target = target;

        public void SetContext(IApplication context) => _context = context; 

        public void SetSpeed(float speed) => _speed = speed;

        protected void Update()
        {
            if (_target == null) return;

            var direction = (_target.position - transform.position);
            var directionNormalized = direction.normalized;
            var step = directionNormalized * (Time.deltaTime * _speed);
            transform.position += step;

            if (direction.sqrMagnitude < 1) TargetReachedSubject.OnNext(this);
        }

        public void Disable()
        {
            SetTarget(null);
            gameObject.SetActive(false);
        }

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        protected bool Equals(EnvironmentObject other)
        {
            return base.Equals(other) && Equals(_target, other._target) && _speed.Equals(other._speed);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((EnvironmentObject)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), TargetReachedSubject);
        }
    }
}