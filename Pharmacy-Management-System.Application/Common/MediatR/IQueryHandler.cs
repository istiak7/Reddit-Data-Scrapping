using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy_Management_System.Application.Common.MediatR
{
    public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery,TResponse>
        where TQuery : IQuery<TResponse>
    {
    }
}
