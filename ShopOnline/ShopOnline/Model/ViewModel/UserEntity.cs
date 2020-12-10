using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
   public class UserEntity
    {
        public long ID { get; set; }


        public string UserName { get; set; }


        public string Password { get; set; }


        public string GroupID { get; set; }


        public string Name { get; set; }


        public string Address { get; set; }


        public string Email { get; set; }


        public string Phone { get; set; }

        public int? ProvinceID { get; set; }

        public int? DistrictID { get; set; }

        public DateTime? CreatedDate { get; set; }


        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }

        public bool Status { get; set; }

        public List<UserGroup> UserGroups { get; set; }
        public void TypeOf_UserEntity(User user)
        {

            PropertyInfo[] pithis = typeof(UserEntity).GetProperties();
            PropertyInfo[] pieClinet = typeof(User).GetProperties();
            foreach (var item in pithis)
            {
                foreach (var itempiem in pieClinet)
                {
                    if (itempiem.Name == item.Name)
                    {
                        item.SetValue(this, itempiem.GetValue(user));
                        break;
                    }
                }
            }

        }
        //
        public UserEntity(User customer)
        {
            TypeOf_UserEntity(customer);

        }
    }
}
