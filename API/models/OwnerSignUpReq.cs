
namespace API.models
{
    public class OwnerSignUpReq
    {
        public OwnerSignUpReq(){

        }

        public string? email { get; set; }
        public string? password { get; set; }
        public string? given_name { get; set; }       
        public string? family_name { get; set; }       
    }
}