using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace API.Models;

public class Pet
{
    public Pet()
    {
        
    }
    
    [Key]
    [JsonPropertyName("ID")]
    public int ID { get; set; }
    
    [JsonPropertyName("OwnerID")]
    public int OwnerID { get; set; }

    [JsonPropertyName("PetName")]
    public string? PetName { get; set; }

    [JsonPropertyName("Type")]
    public string? Type { get; set; }
}