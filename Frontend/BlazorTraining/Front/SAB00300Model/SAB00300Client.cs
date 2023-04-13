using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using SAB00100Common;
using SAB00100Common.DTOs;

namespace SAB00300Model
{
    public class SAB00300Client : R_BusinessObjectServiceClientBase<SAB00100DTO>, ISAB00100
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/SAB01300";

        public SAB00300Client(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, plSendWithContext, plSendWithToken)
        {
        }

        #region GetAllCategory
        
        #endregion

        public SAB00100ListEmployeeDTO GetAllEmployee()
        {
            throw new NotImplementedException();
        }

        public SAB00100ListEmployeeOriginalDTO GetAllEmployeeOriginal()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<SAB00100DTO> GetAllEmployeeStream()
        {
            throw new NotImplementedException();
        }
    }
}