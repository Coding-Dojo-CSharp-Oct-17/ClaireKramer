using dojoleague.Models;
using System.Collections.Generic;
namespace dojoleague.Factory {
    public interface IFactory<T> where T : BaseEntity
    {
    }
}
