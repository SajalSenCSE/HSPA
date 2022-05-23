using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Interface
{
    public interface IPropertyTypeRepository
    {
         Task<IEnumerable<PropertyType>> GetPropertyTypesAsync();
    }
}