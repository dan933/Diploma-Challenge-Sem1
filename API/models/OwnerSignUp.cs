
namespace API.models
{
    public class OwnerSignUp
    {
        public OwnerSignUp(){

        }

        public string? email { get; set; }
        public string? password { get; set; }
        public string? username { get; set; }
        public string? given_name { get; set; }       
        public string? family_name { get; set; }       
    }
}