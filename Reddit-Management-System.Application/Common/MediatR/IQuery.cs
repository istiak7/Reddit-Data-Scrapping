using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddit_Management_System.Application.Common.MediatR
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}
