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
    public class SAB00310Model : R_BusinessObjectServiceClientBase<SAB00310DTO>, ISAB00310
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/SAB00310";

        public SAB00310Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true)
            : base(pcHttpClientName, pcRequestServiceEndPoint, plSendWithContext, plSendWithToken)
        {
        }

        #region GetAllTerritory
        public SAB00300ListDTO<SAB00310DTO> GetAllTerritory()
        {
            throw new System.NotImplementedException();
        }

        public async Task<SAB00300ListDTO<SAB00310DTO>> GetAllTerritoryAsync()
        {
            var loEx = new R_Exception();
            SAB00300ListDTO<SAB00310DTO> loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<SAB00300ListDTO<SAB00310DTO>>(
                    _RequestServiceEndPoint,
                    nameof(ISAB00310.GetAllTerritory),
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
        #endregion

        #region GetAllTerritoryByRegion
        public SAB00300ListDTO<SAB00310DTO> GetAllTerritoryByRegion(int piRegionId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<SAB00300ListDTO<SAB00310DTO>> GetAllTerritoryByRegionAsync(int piRegionId)
        {
            var loEx = new R_Exception();
            SAB00300ListDTO<SAB00310DTO> loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<SAB00300ListDTO<SAB00310DTO>, int>(
                    _RequestServiceEndPoint,
                    nameof(ISAB00310.GetAllTerritoryByRegion),
                    piRegionId,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
        #endregion

        #region GetAllTerritoryByRegionStream
        public IAsyncEnumerable<SAB00310DTO> GetAllTerritoryByRegionStream()
        {
            throw new NotImplementedException();
        }
        
        public async Task<List<SAB00310DTO>> GetAllTerritoryByRegionStreamListAsync()
        {
            var loEx = new R_Exception();
            List<SAB00310DTO> loResult = null;
        
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<SAB00310DTO>(
                    _RequestServiceEndPoint,
                    nameof(ISAB00310.GetAllTerritoryByRegionStream),
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

        #region GetAllTerritoryStream
        // public IAsyncEnumerable<SAB00310DTO> GetAllTerritoryStream()
        // {
        //     throw new NotImplementedException();
        // }
        //
        // public async Task<List<SAB00310DTO>> GetAllTerritoryStreamListAsync()
        // {
        //     var loEx = new R_Exception();
        //     List<SAB00310DTO> loResult = null;
        //
        //     try
        //     {
        //         R_HTTPClientWrapper.httpClientName = _HttpClientName;
        //         loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<SAB00310DTO>(
        //             _RequestServiceEndPoint,
        //             nameof(ISAB00310.GetAllTerritoryStream),
        //             _SendWithContext,
        //             _SendWithToken,
        //             null);
        //     }
        //     catch (Exception ex)
        //     {
        //         loEx.Add(ex);
        //     }
        //
        //     loEx.ThrowExceptionIfErrors();
        //
        //     return loResult;
        // }
        #endregion
    }
}