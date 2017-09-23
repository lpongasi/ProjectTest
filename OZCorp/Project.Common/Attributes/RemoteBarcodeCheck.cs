using Microsoft.AspNetCore.Mvc;

namespace Project.Common.Attributes
{
    public class RemoteBarcodeCheck : RemoteAttribute
    {
        public RemoteBarcodeCheck():base("BarcodeCheckExists","Common", "")
        {
            ErrorMessage = "Already Exists!";
        }
    }
}
