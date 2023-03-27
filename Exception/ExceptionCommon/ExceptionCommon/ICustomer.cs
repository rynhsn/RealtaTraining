using System.Collections.Generic;

namespace ExceptionCommon
{
    public interface ICustomer
    {
        IAsyncEnumerable<CustomerStreamDTO> GetCustomersList(GetCustomersParameterDTO poParameter);
        CustomerResultDTO GetCustomerByTd(GetCustomerByIdParameterDTO poParameter);
    }
}