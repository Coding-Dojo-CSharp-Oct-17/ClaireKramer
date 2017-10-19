using trails.Models;
using System.Collections.Generic;
namespace trails.Factory
{
    public interface IFactory<T> where T : BaseEntity
    {
    }
}