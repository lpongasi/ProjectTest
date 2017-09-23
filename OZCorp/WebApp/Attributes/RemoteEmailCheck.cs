using Microsoft.AspNetCore.Mvc;

namespace WebApp.Attributes
{
    public class RemoteEmailCheck : RemoteAttribute
    {
        public RemoteEmailCheck():base("EmailCheckExists","Common", "")
        {
            ErrorMessage = "Already Exists!";
        }
    }
}
