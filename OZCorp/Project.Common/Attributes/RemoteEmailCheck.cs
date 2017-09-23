using Microsoft.AspNetCore.Mvc;

namespace Project.Common.Attributes
{
    public class RemoteEmailCheck : RemoteAttribute
    {
        public RemoteEmailCheck():base("EmailCheckExists","Common", "")
        {
            ErrorMessage = "Already Exists!";
        }
    }
}
