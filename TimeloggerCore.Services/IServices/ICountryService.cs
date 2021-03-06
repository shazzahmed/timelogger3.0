using TimeloggerCore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using TimeloggerCore.Common.Models;

namespace TimeloggerCore.Services.IService
{
    public interface ICountryService : IBaseService<CountryModel, Country, int>
    {
    }
}
