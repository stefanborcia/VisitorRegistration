
using System.ComponentModel;


namespace VisitorDataAccess.Entities
{
    public class VisitorLog : SoftDelete
    {
        public long Id { get; set; }
        public Visit Visit { get; set; }
        public Action Actions { get; set; }
        public TimeSpan TimeSpent { get; set; }
    }

    public enum Action
    {
        [Description("Sign-In")]
        SignIn,

        [Description("Sign-Out")]
        SignOut
    }
}
