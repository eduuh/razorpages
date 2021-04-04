using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Kaizen.Models.Models;

namespace Kaizen.DataAccess.Data.Repository.IRepository
{
    public interface ICultureReport : IRepository<CultureReport>
    {
        void Update(CultureReport report);
    }
}