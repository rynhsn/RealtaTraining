using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using SAB00300Common.DTOs;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SAB00300Model.ViewModels
{
    public class SAB00310ViewModel : R_ViewModel<SAB00310DTO>
    {
        private SAB00310Model _SAB00310Model = new SAB00310Model();
        public ObservableCollection<SAB00310DTO> TerritoryList { get; set; } = new ObservableCollection<SAB00310DTO>();
        public SAB00310DTO Territory = new SAB00310DTO();
        public int RegionId { get; set; }

        public async Task GetTerritoryByRegion(int piRegionId)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _SAB00310Model.GetAllTerritoryByRegionAsync(piRegionId);
                TerritoryList = new ObservableCollection<SAB00310DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        public async Task GetTerritoryByRegionStream()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _SAB00310Model.GetAllTerritoryByRegionStreamListAsync();
                TerritoryList = new ObservableCollection<SAB00310DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetTerritoryById(string piTerritoryId)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new SAB00310DTO { TerritoryID = piTerritoryId };
                var loResult = await _SAB00310Model.R_ServiceGetRecordAsync(loParam);

                Territory = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveTerritory(SAB00310DTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                poNewEntity.RegionID = RegionId;
                var loResult = await _SAB00310Model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                Territory = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task DeleteTerritory(string territoryId)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new SAB00310DTO { TerritoryID = territoryId };
                await _SAB00310Model.R_ServiceDeleteAsync(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}