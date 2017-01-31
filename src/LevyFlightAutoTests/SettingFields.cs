namespace LevyFlightAutoTests
{
    public class SettingFields
    {
        public int Start { get; }
        public int End { get; }
        public int Step { get; }
        public int Default { get; }
        public bool IsFixed { get; }
        public int Next
        {
            get
            {
                if (IsFixed)
                {
                    return Default;
                }
                var returnValue = _current;
                _current += Step;
                return returnValue;
            }
        }

        public SettingFields(int @default)
            : this(0, 0, 0, @default, true)
        {
        }

        public SettingFields(int start, int end, int step)
            : this(start, end, step, 0, false)
        {
        }

        public SettingFields(int start, int end, int step, int @default, bool isFixed)
        {
            Start = start;
            End = end;
            Step = step;
            Default = @default;
            IsFixed = isFixed;
            _current = start;
        }

        private int _current;
    }
}
