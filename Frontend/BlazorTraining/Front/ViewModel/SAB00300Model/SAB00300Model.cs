using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using SAB00300Common;
using SAB00300Common.DTOs;

namespace SAB00300Model
{
    public class SAB00300Model : R_BusinessObjectServiceClientBase<SAB00300DTO>, ISAB00300
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/SAB00300";

        public SAB00300Model(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, plSendWithContext, plSendWithToken)
        {
        }

        #region GetAllRegion

        public SAB00300ListDTO<SAB00300DTO> GetAllRegion()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<SAB00300DTO> GetAllRegionStream()
        {
            throw new NotImplementedException();
        }

        public async Task<SAB00300ListDTO<SAB00300DTO>> GetAllRegionAsync()
        {
            var loEx = new R_Exception();
            SAB00300ListDTO<SAB00300DTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<SAB00300ListDTO<SAB00300DTO>>(
                    _RequestServiceEndPoint,
                    nameof(ISAB00300.GetAllRegion),
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        #endregion

        #region GetAllRegionStream

        public async Task<List<SAB00300DTO>> GetAllRegionStreamAsync()
        {
            var loEx = new R_Exception();
            List<SAB00300DTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<SAB00300DTO>(
                    _RequestServiceEndPoint,
                    nameof(ISAB00300.GetAllRegionStream),
                    _SendWithContext,
                    _SendWithToken,
                    null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        #endregion
    }
}