using DIP.Core._Util;
using DIP.Core.Validate;
using Microsoft.Extensions.Localization;

namespace DIP.Core.Repository
{
    public class OptionsSearch:IOptionsSearch
    {
        private readonly IStringLocalizer _stringLocalizer;
        private readonly IValidate _validate;

        public OptionsSearch(IStringLocalizer stringLocalizer, IValidate validate)
        {
            _stringLocalizer = stringLocalizer;
            _validate = validate;
        }

        public int? RegisterPerPage { get; set; }
        public int? Page { get; set; }

        public void Validate()
        {
            _validate.Interval(RegisterPerPage, PropertyHelper.ClassAndPropertyName(() => this.RegisterPerPage), 1, null);
            _validate.Interval(Page, PropertyHelper.ClassAndPropertyName(() => this.Page), 1, null);

        }
    }
}
