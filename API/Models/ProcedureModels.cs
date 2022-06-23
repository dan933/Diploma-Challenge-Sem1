using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace API.Models;

public class Procedure
{
    public Procedure()
    {
        
    }
    
    [Key]
    [JsonPropertyName("ID")]
    public int ID { get; set; }
    
    [JsonPropertyName("ProcedureID")]
    public int ProcedureID { get; set; }

    [JsonPropertyName("Description")]
    public string? Description { get; set; }

    [JsonPropertyName("Price")]
    public decimal Price { get; set; }
}