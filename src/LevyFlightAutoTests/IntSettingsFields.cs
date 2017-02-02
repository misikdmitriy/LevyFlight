namespace LevyFlightAutoTests
{
    public class IntSettingsFields : SettingFields<int>
    {
        public override int Current
        {
            get { return _current.Value; }
            protected set { _current = value; }
        }

        public override int Value
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

        public IntSettingsFields(int @default) 
            : base(@default)
        {
        }

        public IntSettingsFields(int start, int end, int step) 
            : base(start, end, step)
        {
        }

        public IntSettingsFields(int start, int end, int step, int @default, bool isFixed) 
            : base(start, end, step, @default, isFixed)
        {
            _next = start;
        }

        private int _next;
        private int? _current;
    }
}
