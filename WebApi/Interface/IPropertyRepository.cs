using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Interface
{
    public interface IPropertyRepository
    {
         Task<IEnumerable<Property>> GetPropertiesAsync(int sellRent);
         void AddProperty(Property property);
         void DeleteProperty(Property property);
    }
}