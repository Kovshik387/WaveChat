using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveChat.Context.Entities.Users;

namespace WaveChat.Context.Seeder.Seeds.Demo;

public class DemoHelper
{
    public IEnumerable<Rolestype> GetRoles = new List<Rolestype>
    {
        //new Rolestype()
        //{
        //    Id = 1,
        //    Rolename = "Работник"
        //},
        //new Rolestype()
        //{
        //    Id = 2,
        //    Rolename = "Старший программист"
        //}
    };
}
