using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorDataAccess.Entities
{
    public class VisitorLog
    {
        public long Id { get; set; }
        public Visit Visit {  get; set; }
        public Action Actions { get; set; }
        public DateTime TimeSpent { get; set; }
    }

    public enum Action
    {
        [Description("Sign-In")]
        SignIn,

        [Description("Sign-Out")]
        SignOut
    }
}
