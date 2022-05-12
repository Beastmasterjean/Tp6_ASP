using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Tp5.Models.Base;
using Tp5.Resources;

namespace Tp5.Models
{
    public class Member : ModelBase
    {

        public const string ROLE_ADMIN = "Admin";
        public const string ROLE_STANDARD = "Standard";

        [Display(Name = "Name", ResourceType = typeof(Resource))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "ModelRequired", ErrorMessageResourceType = typeof(Resource))]
        [StringLength(30, ErrorMessageResourceName = "ModelLengthLessThan", ErrorMessageResourceType = typeof(Resource))]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email", ResourceType = typeof(Resource))]
        [EmailAddress(ErrorMessageResourceName = "InvalidEmailFormat", ErrorMessageResourceType = typeof(Resource))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "ModelRequired", ErrorMessageResourceType = typeof(Resource))]
        [StringLength(50, ErrorMessageResourceName = "ModelLengthLessThan", ErrorMessageResourceType = typeof(Resource))]
        public string Email { get; set; }

        [Display(Name = "Username", ResourceType = typeof(Resource))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "ModelRequired", ErrorMessageResourceType = typeof(Resource))]
        [StringLength(20, MinimumLength = 3, ErrorMessageResourceName = "ModelLengthBetween", ErrorMessageResourceType = typeof(Resource))]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Resource))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "ModelRequired", ErrorMessageResourceType = typeof(Resource))]
        [StringLength(20, MinimumLength = 5, ErrorMessageResourceName = "ModelLengthBetween", ErrorMessageResourceType = typeof(Resource))]
        public string Password { get; set; }

        public bool IsActive { get; set; }
        public Guid ActivationCode { get; set; }
        public string Role { get; set; }

        // Constructeur vide requis pour la désérialisation
        public Member()
        {
        }

        public Member(int id, string name, string email, string username, string password, string role)
            : base(id)
        {
            Name = name;
            Email = email;
            Username = username;
            Password = password;
            IsActive = true;
            ActivationCode = Guid.Empty;
            Role = role;
        }
    }
}
