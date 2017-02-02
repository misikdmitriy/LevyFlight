namespace LevyFlightAutoTests
{
    public class DoubleSettingsFields : SettingFields<double>
    {
        public override double Current
        {
            get { return _current.Value; }
            protected set { _current = value; }
        }

        public override double Value
        {
            get
            {
                Current = _next >= End ? End : _next;
                if (IsFixed)
                {
                    Current = Default;
                    return Default;
                }
                if (_next >= End)
                {
                    return End;
                }
                _next += Step;
                return Current;
            }
        }

        public DoubleSettingsFields(double @default) 
            : base(@default)
        {
        }

        public DoubleSettingsFields(double start, double end, double step) 
            : base(start, end, step)
        {
        }

        public DoubleSettingsFields(double start, double end, double step, double @default, bool isFixed) 
            : base(start, end, step, @default, isFixed)
        {
            _next = start;
        }

        private double _next;
        private double? _current;
    }
}
