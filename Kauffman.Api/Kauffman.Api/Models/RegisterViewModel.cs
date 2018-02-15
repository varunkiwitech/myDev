using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Kauffman.Api.Models
{
    /// <summary>
    /// /// Sign Up 
    /// </summary>
    public class RegisterViewModel
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string UserEmail { get; set; }
        [Required]
        public string Password { get; set; }
    }

    /// <summary>
    /// /// Sign In 
    /// </summary>
    public class SignInViewModel
    {
        /// <summary>
        /// /// Use password as grant type
        /// </summary>
        [Required]
        public string grant_type { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
    }


}