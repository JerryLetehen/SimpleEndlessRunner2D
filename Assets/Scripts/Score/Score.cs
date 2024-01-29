namespace NinjaJump.Score
{
    public class Score
    {
        private readonly float _speed;
        public double Value { get; private set; }

        public Score(float speed)
        {
            _speed = speed;
        }

        public void Reset() => Value = 0;

        public void Add(double value) => Value += value;

        public void Update(float deltaTime) => Value += _speed * deltaTime;
    }
}