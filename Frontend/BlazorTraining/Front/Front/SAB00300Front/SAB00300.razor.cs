using BlazorClientHelper;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Base;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using R_ContextFrontEnd;
using SAB00300Common;
using SAB00300Common.DTOs;
using SAB00300Model.ViewModels;

namespace SAB00300Front;

public partial class SAB00300 : R_Page
{
    private SAB00300ViewModel RegionViewModel = new();
    private R_Grid<SAB00300DTO> _gridRef;
    private R_ConductorGrid _conGridRegionRef;

    private SAB00310ViewModel TerritoryViewModel = new();
    private R_Conductor _conductorTerritoryRef;
    private R_Grid<SAB00310DTO> _gridTerritoryRef;

    [Inject] private R_ContextHeader _contextHeader { get; set; }
    [Inject] private IClientHelper _clientHelper { get; set; }

    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();
    
        try
        {
            var lcCompanyId = _clientHelper.CompanyId;
            var lcUserId = _clientHelper.UserId;
            await _gridRef.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
    
        R_DisplayException(loEx);
    }
    
    #region Conductor Region
    private async Task GridRegion_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await RegionViewModel.GetRegionList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }

    private async Task Grid_Display(R_DisplayEventArgs eventArgs)
    {
        if (eventArgs.ConductorMode == R_eConductorMode.Normal)
        {
            var loParam = (SAB00300DTO)eventArgs.Data;
            TerritoryViewModel.RegionId = loParam.RegionID;
            await _gridTerritoryRef.R_RefreshGrid(loParam);
        }
    }

    private async Task Grid_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = (SAB00300DTO)eventArgs.Data;
            await RegionViewModel.GetRegion(loParam.RegionID);

            eventArgs.Result = RegionViewModel.Region;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task Grid_ServiceSave(R_ServiceSaveEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await RegionViewModel.SaveRegion((SAB00300DTO)eventArgs.Data, (eCRUDMode)eventArgs.ConductorMode);
            eventArgs.Result = RegionViewModel.Region;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task Grid_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loData = (SAB00300DTO)eventArgs.Data;
            await RegionViewModel.DeleteRegion(loData.RegionID);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    #endregion

    #region Conductor Territory
    private async Task GridTerritory_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var liParam = ((SAB00300DTO)eventArgs.Parameter).RegionID;
            _contextHeader.R_Context.R_SetStreamingContext(ContextConstant.REGION_ID, liParam);
            await TerritoryViewModel.GetTerritoryByRegionStream();
            // await TerritoryViewModel.GetTerritoryByRegion(liParam);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }

    private async Task ConductorTerritory_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = (SAB00310DTO)eventArgs.Data;
            await TerritoryViewModel.GetTerritoryById(loParam.TerritoryID);

            eventArgs.Result = TerritoryViewModel.Territory;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task Territory_ServiceSaveAsync(R_ServiceSaveEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            await TerritoryViewModel.SaveTerritory((SAB00310DTO)eventArgs.Data, (eCRUDMode)eventArgs.ConductorMode);
            eventArgs.Result = TerritoryViewModel.Territory;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    #endregion
}