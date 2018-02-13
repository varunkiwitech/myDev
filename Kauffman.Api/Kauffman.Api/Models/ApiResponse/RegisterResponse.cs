using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kauffman.Api.Models.ApiResponse
{
    public class RegisterResponse
    {
        public bool IsUserCreated { get; set; }
        public string UserId { get; set; }
        public bool IsUserSubscribed { get; set; }

        public bool HasUserTakenAssesment { get; set; }

    }
}