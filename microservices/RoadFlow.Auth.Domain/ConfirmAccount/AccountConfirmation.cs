using MongoDB.Bson.Serialization.Attributes;

namespace RoadFlow.Auth.Domain.ConfirmAccount;

public class AccountConfirmation
{
    [BsonId]
    public string Email { get; set; }
    
    public string Code { get; set; }
    
    public DateTime ExpiryDate { get; set; }
}