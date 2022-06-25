using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace API.Models;

// public class Owner
// {
//     public Owner()
//     {
        
//     }
    
//     [Key]
//     [JsonPropertyName("OwnerID")]
//     public int OwnerID { get; set; }
    
//     [JsonPropertyName("Surname")]
//     public string? Surname { get; set; }

//     [JsonPropertyName("FirstName")]
//     public string? FirstName { get; set; }

//     [JsonPropertyName("Phone")]
//     public string? Phone { get; set; }
// }

public class CreateOwnerReq
{
    public CreateOwnerReq()
    {
        
    }
    
    [JsonPropertyName("Surname")]
    public string? Surname { get; set; }

    [JsonPropertyName("FirstName")]
    public string? FirstName { get; set; }

    [JsonPropertyName("Phone")]
    public string? Phone { get; set; }
}