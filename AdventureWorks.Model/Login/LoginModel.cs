using System;

namespace AdventureWorks.Model
{
    public sealed class LoginModel : IEquatable<LoginModel>
    {
        public string Email { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordHash { get; set; }
        public DateTime LastLogonAt { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as LoginModel);
        }
        public override int GetHashCode()
        {
            return Email.GetHashCode();
        }
        public bool Equals(LoginModel other)
        {
            if (other == null) return false;
            return GetHashCode() == other.GetHashCode();
        }
    }
}
