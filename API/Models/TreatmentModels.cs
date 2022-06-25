// using System.ComponentModel.DataAnnotations;
// using System.Text.Json.Serialization;
// using Microsoft.AspNetCore.Mvc;

// namespace API.Models;

// public class View_Treatment
// {
//     public View_Treatment()
//     {

//     }

//     [JsonPropertyName("OwnerID")]
//     public int OwnerID { get; set; }

//     [JsonPropertyName("ID")]
//     public int ID { get; set; }

//     [JsonPropertyName("PetID")]
//     public int FK_PetID { get; set; }

//     [JsonPropertyName("PetName")]
//     public string? PetName { get; set; }

//     [JsonPropertyName("ProcedureID")]
//     public int ProcedureID { get; set; }

//     [JsonPropertyName("ProcedureName")]
//     public string? ProcedureName { get; set; }

//     [JsonPropertyName("Date")]
//     public DateTime Date { get; set; }

//     [JsonPropertyName("Notes")]
//     public string? Notes { get; set; }

//     [JsonPropertyName("Payment")]
//     public decimal Payment { get; set; }

//     [JsonPropertyName("AmountOwed")]
//     public decimal? AmountOwed { get; set; }
// }