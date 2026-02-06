using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy_Management_System.Application.Constants
{
    public static class AppConstants
    {
        public enum ResultStatus
        {
            Success,
            Error,
            Canceled
        }
        public enum ApprovalStatusId
        {
            NotApproved = 1,
            PartialApproved = 2,
            Approved = 3,
            Rejected = 4
        }
    }
}
