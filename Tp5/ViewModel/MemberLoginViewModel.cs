using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Tp5.Models;
using Tp5.Resources;

namespace Tp5.ViewModel
{
    public class MemberLoginViewModel
    {
        [Display(Name = "Username", ResourceType = typeof(Resource))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "ModelRequired", ErrorMessageResourceType = typeof(Resource))]
        [StringLength(20, MinimumLength = 3, ErrorMessageResourceName = "ModelLengthBetween", ErrorMessageResourceType = typeof(Resource))]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Resource))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "ModelRequired", ErrorMessageResourceType = typeof(Resource))]
        [StringLength(20, MinimumLength = 5, ErrorMessageResourceName = "ModelLengthBetween", ErrorMessageResourceType = typeof(Resource))]
        public string Password { get; set; }

        // Constructeur vide requis pour la désérialisation
        public MemberLoginViewModel()
        {
        }

        public MemberLoginViewModel(Member member)
        {
            Username = member.Username;
            Password = member.Password;
        }
    }
}
