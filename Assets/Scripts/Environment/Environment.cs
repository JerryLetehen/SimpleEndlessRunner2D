using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Pool;
using Random = System.Random;

namespace NinjaJump.Environment
{
    /// <summary>
    /// Controlling environment objects
    /// </summary>
    public class Environment : MonoBehaviour, IEnvironment
    {
        private const int RndSeed = 1;

        [SerializeField] private EnvironmentConfig _config;
        [SerializeField] private Transform _startPoint;
        [SerializeField] private Transform _endPoint;

        public IObservable<Unit> Spawned => _spawnedSubject;

        private readonly Subject<Unit> _spawnedSubject = new();
        private readonly CompositeDisposable _subscriptions = new();
        private readonly Dictionary<EnvironmentObject, IObjectPool<EnvironmentObject>> _activeObjects = new(10);

        private IApplication _context;
        private Random _rnd;
        private IObjectPool<EnvironmentObject>[] _pools;
        private bool _isRunning;
        private float _spawnDelayCounter;

        private IObjectPool<EnvironmentObject>[] Pools
        {
            get
            {
                if (_pools != null) return _pools;

                _pools = new IObjectPool<EnvironmentObject>[_config.Prefabs.Length];
                for (var i = 0; i < _config.Prefabs.Length; i++)
                {
                    var index = i;
                    _pools[i] = new ObjectPool<EnvironmentObject>(
                        createFunc: () => Instantiate(_config.Prefabs[index], _startPoint),
                        ActionOnGet,
                        ActionOnRelease);
                }

                return _pools;
            }
        }

        public void Run(IApplication context)
        {
            _context = context;
            _rnd = new Random(RndSeed);

            EnvironmentObject.TargetReached.Subscribe(ReturnToPool).AddTo(_subscriptions);

            SpawnNew();

            _isRunning = true;
        }

        public void Stop()
        {
            _isRunning = false;

            foreach (var (obj, pool) in _activeObjects)
            {
                pool.Release(obj);
            }

            _activeObjects.Clear();
            _subscriptions.Clear();

            _spawnDelayCounter = 0;
        }

        private void Update()
        {
            if (_isRunning == false) return;

            _spawnDelayCounter += Time.deltaTime;

            if (_spawnDelayCounter >= _config.SpawnDelay)
            {
                SpawnNew();
                _spawnDelayCounter = 0;
            }
        }

        private void SpawnNew()
        {
            var pool = GetPool();
            var obj = pool.Get();
            _activeObjects[obj] = pool;
            obj.SetContext(_context);
            obj.SetSpeed(_config.Speed);
            obj.SetTarget(_endPoint);
            _spawnedSubject.OnNext(Unit.Default);
        }

        private void ReturnToPool(EnvironmentObject obj)
        {
            _activeObjects[obj].Release(obj);
            _activeObjects.Remove(obj);
        }

        private IObjectPool<EnvironmentObject> GetPool() => Pools[_rnd.Next(Pools.Length)];

        private void ActionOnRelease(EnvironmentObject obj)
        {
            obj.Disable();
            obj.transform.position = _startPoint.position;
        }

        private void ActionOnGet(EnvironmentObject obj)
        {
            obj.Enable();
            obj.transform.position = _startPoint.position;
        }
    }
}