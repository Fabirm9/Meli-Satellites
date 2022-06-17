using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Satellites.Core.Helpers
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class EnsureMinimumElementsList : ValidationAttribute
    {
        private readonly int _min;
        private readonly int _max;

        public EnsureMinimumElementsList(int min = 0, int max = int.MaxValue)
        {
            _min = min;
            _max = max;
        }

        public override bool IsValid(object value)
        {

            if (!(value is IList list))
                return false;


            return list.Count >= _min && list.Count <= _max;
        }
    }
}
