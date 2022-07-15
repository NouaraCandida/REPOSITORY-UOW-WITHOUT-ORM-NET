using DIP.Core.Exceptions;
using Microsoft.Extensions.Localization;

namespace DIP.Core.Validate
{
    public class Validate:IValidate
    {
        private readonly IStringLocalizer _stringLocalizer;

        public Validate(IStringLocalizer stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
        }

        public void Interval(int? value, string property, int? minimun, int? maximum)
        {
            if (value != null)
            {
                if (minimun.HasValue && value < minimun.Value)
                    throw new ValidateException($"{_stringLocalizer[property]} {_stringLocalizer["PropertyValueLessThan"]} {minimun.Value}");
                if (maximum.HasValue && value > maximum.Value)
                    throw new ValidateException($"{_stringLocalizer[property]} {_stringLocalizer["PropertyValueGreaterThan"]} {maximum.Value}");
            }
        }


            public void Required(object value, string property)
        {
            if (value == null)
                throw new ValidateException($"{_stringLocalizer[property]} {_stringLocalizer["PropertyCannotBeNull"]}");
        }

        public void ValidateRule(bool invalidRule, string msg)
        {
            if (invalidRule)
                throw new ValidateException(_stringLocalizer[msg]);
        }
    }
}
