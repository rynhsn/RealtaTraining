using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using R_Common;
using R_CommonFrontBackAPI;
using SAB00300Back;
using SAB00300Common;
using SAB00300Common.DTOs;

namespace SAB00300Controller;

[ApiController]
[Route("api/[controller]/[action]"), AllowAnonymous]
public class SAB00300Controller : ControllerBase, ISAB00300
{
    [HttpPost]
    public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<SAB00300DTO> poParameter)
    {
        var loEx = new R_Exception();
        var loRtn = new R_ServiceDeleteResultDTO();

        try
        {
            var loCls = new SAB00300Cls();

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
    public R_ServiceGetRecordResultDTO<SAB00300DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<SAB00300DTO> poParameter)
    {
        var loEx = new R_Exception();
        var loRtn = new R_ServiceGetRecordResultDTO<SAB00300DTO>();

        try
        {
            var loCls = new SAB00300Cls();

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
    public R_ServiceSaveResultDTO<SAB00300DTO> R_ServiceSave(R_ServiceSaveParameterDTO<SAB00300DTO> poParameter)
    {
        var loEx = new R_Exception();
        var loRtn = new R_ServiceSaveResultDTO<SAB00300DTO>();

        try
        {
            var loCls = new SAB00300Cls();

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
    public SAB00300ListDTO<SAB00300DTO> GetAllRegions()
    {
        var loEx = new R_Exception();
        SAB00300ListDTO<SAB00300DTO> loRtn = null;

        try
        {
            var loCls = new SAB00300Cls();

            var loResult = loCls.GetCategories();
            loRtn = new SAB00300ListDTO<SAB00300DTO> { Data = loResult };
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }
}