using System;
namespace DIP.Core.Validate
{
    public interface IValidate
    {
        void Required(object value, string property);
        void ValidateRule(bool invalidRule, string msg);
        void Interval(int? value, string property, int? minimun, int? maximum);
    }
}
