namespace DDD.Infra.CrossCutting.Identity.Models.AccountViewModels
{
    public class TokenViewModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
