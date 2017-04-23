using System;

namespace LevyFlight.AutoTests
{
    public abstract class SettingFields<T>
    {
        public T Start { get; }
        public T End { get; }
        public T Step { get; }
        public T Default { get; }
        public bool IsFixed { get; }

        public abstract T Current { get; protected set; }

        public abstract T Value { get; }

        protected SettingFields(T @default)
            : this(default(T), default(T), default(T), @default, true)
        {
        }

        protected SettingFields(T start, T end, T step)
            : this(start, end, step, default(T), false)
        {
        }

        protected SettingFields(T start, T end, T step, T @default, bool isFixed)
        {
            Start = start;
            End = end;
            Step = step;
            Default = @default;
            IsFixed = isFixed;
        }
    }
}
