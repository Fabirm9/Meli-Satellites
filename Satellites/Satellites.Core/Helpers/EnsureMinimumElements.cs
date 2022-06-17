using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Satellites.Core.Helpers
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class EnsureMinimumElements : ValidationAttribute
    {
        private readonly int _min;
        private readonly int _max;

        public EnsureMinimumElements(int min = 0, int max = int.MaxValue)
        {
            _min = min;
            _max = max;
        }

        public override bool IsValid(object value)
        {

            if (!(value is IList list))
                return false;

            if (list.Count == 1 && list[0].ToString() == "")
                return false;

            return list.Count >= _min && list.Count <= _max;
        }
    }
}
