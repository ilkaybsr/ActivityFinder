using System;

namespace Business.DTO
{
    public class UserLoginResult
    {
        public string Token { get; set; }

        public DateTime ExpireDate { get; set; }
    }
}
