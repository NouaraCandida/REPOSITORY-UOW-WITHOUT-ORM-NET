using System;
using DIP.Core._Util;
using DIP.Core.Exceptions;
using DIP.Core.Validate;
using DIP.Sensors.Domain.Models;
using Microsoft.Extensions.Localization;

namespace DIP.Sensors.API.DTO
{
    public class LocalityDTO
    {
        public string NameRegion { get; private set; }
        public string NameCountry { get; private set; }
        public RegionEnum NameRegionEnum { get; private set; }
        public CountryEnum NameCountryEnum { get; private set; }
        private readonly IStringLocalizer _stringLocalizer;


        public LocalityDTO(IValidate validate, IStringLocalizer stringLocalizer, RegionEnum nameRegionEnum, CountryEnum nameCountryEnum)
        {
            validate.Required(nameRegionEnum, PropertyHelper.ClassAndPropertyName(() => nameRegionEnum));
            validate.Required(nameCountryEnum, PropertyHelper.ClassAndPropertyName(() => nameCountryEnum));

            _stringLocalizer = stringLocalizer;

            NameCountry = ValidadeEnumCountry(nameCountryEnum);
            NameRegion = ValidadeEnumRegion(nameRegionEnum);

            NameRegionEnum = nameRegionEnum;
            NameCountryEnum = nameCountryEnum;
        }

        private string ValidadeEnumCountry(CountryEnum countryEnum)
        {
            return countryEnum switch
            {
                CountryEnum.Undefined => throw new ValidateException(_stringLocalizer["DTO.Locality.NameCountry.Undefined"]),
                CountryEnum.Brasil => "Brasil",
                _ => throw new NotImplementedException(),
            };

        }

        private string ValidadeEnumRegion(RegionEnum regionEnum)
        {
            return regionEnum switch
            {
                RegionEnum.Undefined => throw new ValidateException(_stringLocalizer["DTO.Locality.NameRegion.Undefined"]),
                RegionEnum.CentroOeste => "Centro Oeste",
                RegionEnum.Nordeste => "Nordeste",
                RegionEnum.Norte => "Norte",
                RegionEnum.Sul => "Sul",
                RegionEnum.Suldeste => "Suldeste",
                _ => throw new NotImplementedException(),
            };

        }
    }
}
