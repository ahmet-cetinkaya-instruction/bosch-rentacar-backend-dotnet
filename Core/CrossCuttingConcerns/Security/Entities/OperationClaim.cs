using Core.Entities;

namespace Core.CrossCuttingConcerns.Security.Entities;

// Authorization: Yetkilendirme
public class OperationClaim : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
}