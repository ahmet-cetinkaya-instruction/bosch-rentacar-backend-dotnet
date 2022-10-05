using Core.Entities;
using Entities.Concretes;

namespace Entities.Concretes
{
    public class Brand : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}