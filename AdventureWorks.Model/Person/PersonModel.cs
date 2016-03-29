using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace AdventureWorks.Model
{
    [DataContract]
    public class PersonModel : IEquatable<PersonModel>
    {
        private int _id;
        [DataMember]
        public int Id
        {
            get { return _id; }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException("id can't be negative");
                _id = value;
            }
        }

        public PersonType Type { get; set; }
        [DataMember]
        public string TypeString
        {
            get { return Type.ToString(); }
            set
            {
                if (String.IsNullOrWhiteSpace(value)) throw new ArgumentNullException("type can not be empty");
                Type = (PersonType)Enum.Parse(typeof(PersonType), value);
            }
        }

        private string _title;
        [DataMember]
        public string Title
        {
            get { return _title; }
            set
            {
                if (String.IsNullOrWhiteSpace(value)) value = null;
                _title = value;
            }
        }

        private string _firstName;
        [DataMember]
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (String.IsNullOrWhiteSpace(value)) throw new ArgumentNullException("first name can't be empty");
                _firstName = value;
            }
        }

        private string _lastName;
        [DataMember]
        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (String.IsNullOrWhiteSpace(value)) throw new ArgumentNullException("last name can't be empty");
                _lastName = value;
            }
        }

        public DateTime ModifiedDate { get; set; }

        [DataMember]
        public string ModifiedDateString
        {
            get { return ModifiedDate.ToString("O", CultureInfo.InvariantCulture); }
            set
            {
                if (String.IsNullOrWhiteSpace(value)) throw new ArgumentNullException("date can not be empty");
                ModifiedDate = Convert.ToDateTime(value, CultureInfo.InvariantCulture);
            }
        }

        public PersonModel() { }
        public PersonModel(int id, string title, string firstName, string lastName) : this(id, PersonType.EM, title, firstName, lastName) { }
        public PersonModel(int id, PersonType type, string title, string firstName, string lastName)
        {
            Id = id;
            Type = type;
            Title = title;
            FirstName = firstName;
            LastName = lastName;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as PersonModel);
        }
        public override int GetHashCode()
        {
            return Id;
        }

        public bool Equals(PersonModel other)
        {
            if (other == null) return false;
            return GetHashCode() == other.GetHashCode();
        }
    }
}
