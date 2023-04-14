using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using SAB00300Back;
using SAB00300Common;
using SAB00300Common.DTOs;

namespace SAB00300Controller;

[ApiController]
[Route("api/[controller]/[action]"), AllowAnonymous]
public class SAB00310Controller : ControllerBase, ISAB00310
{
    [HttpPost]
    public R_ServiceGetRecordResultDTO<SAB00310DTO> R_ServiceGetRecord(
        R_ServiceGetRecordParameterDTO<SAB00310DTO> poParameter)
    {
        var loEx = new R_Exception();
        var loRtn = new R_ServiceGetRecordResultDTO<SAB00310DTO>();

        try
        {
            var loCls = new SAB00310Cls();

            loRtn.data = loCls.R_GetRecord(poParameter.Entity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }

    [HttpPost]
    public R_ServiceSaveResultDTO<SAB00310DTO> R_ServiceSave(R_ServiceSaveParameterDTO<SAB00310DTO> poParameter)
    {
        var loEx = new R_Exception();
        var loRtn = new R_ServiceSaveResultDTO<SAB00310DTO>();

        try
        {
            var loCls = new SAB00310Cls();

            loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }

    [HttpPost]
    public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<SAB00310DTO> poParameter)
    {
        var loEx = new R_Exception();
        var loRtn = new R_ServiceDeleteResultDTO();

        try
        {
            var loCls = new SAB00310Cls();

            loCls.R_Delete(poParameter.Entity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }

    [HttpPost]
    public SAB00300ListDTO<SAB00310DTO> GetAllTerritory()
    {
        var loEx = new R_Exception();
        SAB00300ListDTO<SAB00310DTO> loRtn = null;

        try
        {
            var loCls = new SAB00310Cls();

            var loResult = loCls.GetAllTerritory();
            loRtn = new SAB00300ListDTO<SAB00310DTO> { Data = loResult };
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }

    [HttpPost]
    public SAB00300ListDTO<SAB00310DTO> GetAllTerritoryByRegion(int piRegionId)
    {
        var loEx = new R_Exception();
        SAB00300ListDTO<SAB00310DTO> loRtn = null;

        try
        {
            var loCls = new SAB00310Cls();

            var loResult = loCls.GetAllTerritoryByRegion(piRegionId);
            loRtn = new SAB00300ListDTO<SAB00310DTO> { Data = loResult };
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }

    [HttpPost]
    public IAsyncEnumerable<SAB00310DTO> GetAllTerritoryByRegionStream()
    {
        var loEx = new R_Exception();
        IAsyncEnumerable<SAB00310DTO> loRtn = null;

        try
        {
            var liRegionId = R_Utility.R_GetStreamingContext<string>(ContextConstant.REGION_ID);
            var loCls = new SAB00310Cls();

            var loResult = loCls.GetAllTerritoryByRegion(Convert.ToInt16(liRegionId));

            loRtn = GetTerritoryStream(loResult);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }


    private async IAsyncEnumerable<SAB00310DTO> GetTerritoryStream(List<SAB00310DTO> poParameter)
    {
        foreach (SAB00310DTO item in poParameter)
        {
            await Task.Delay(10);
            yield return item;
        }
    }
}