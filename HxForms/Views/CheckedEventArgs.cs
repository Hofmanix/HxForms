using System;

namespace HxForms.Views
{
    public class CheckedEventArgs: EventArgs
    {
        public bool Value { get; private set; }

        public CheckedEventArgs(bool value)
        {
            Value = value;
        }
    }
}