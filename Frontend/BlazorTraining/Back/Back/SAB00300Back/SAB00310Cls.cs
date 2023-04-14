using BackHelper;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using SAB00300Common.DTOs;

namespace SAB00300Back;

public class SAB00310Cls:R_BusinessObject<SAB00310DTO>
{
    protected override SAB00310DTO R_Display(SAB00310DTO poEntity)
    {
        var loEx = new R_Exception();
        SAB00310DTO loResult = null;

        try
        {
            var loDb = new R_Db();
            var loConn = loDb.GetConnection("NorthwindConnectionString");

            var lcQuery = "SELECT * FROM Territories (NOLOCK) WHERE TerritoryID = @TerritoryID";

            var loCmd = loDb.GetCommand();
            loCmd.CommandText = lcQuery;
            loCmd.AddParameter("@TerritoryID", poEntity.TerritoryID);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loResult = R_Utility.R_ConvertTo<SAB00310DTO>(loDataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();

        return loResult;
    }

    protected override void R_Saving(SAB00310DTO poNewEntity, eCRUDMode poCRUDMode)
    {
        var loEx = new R_Exception();

        try
        {
            string lcQuery = "";
            var loDb = new R_Db();
            var loConn = loDb.GetConnection("NorthwindConnectionString");

            var loCmd = loDb.GetCommand();
            if (poCRUDMode == eCRUDMode.AddMode)
            {
                lcQuery = "INSERT INTO Territories (TerritoryID, TerritoryDescription, RegionID) ";
                lcQuery += "VALUES (@TerritoryID, @TerritoryDescription, @RegionID) ";

                loCmd.CommandText = lcQuery;
                loCmd.AddParameter("@TerritoryID", poNewEntity.TerritoryID);
                loCmd.AddParameter("@TerritoryDescription", poNewEntity.TerritoryDescription);
                loCmd.AddParameter("@RegionID", poNewEntity.RegionID);

                loDb.SqlExecNonQuery(loConn, loCmd, true);

                return;
            }

            lcQuery = "UPDATE Territories SET TerritoryDescription = @TerritoryDescription, " +
                      "RegionID = @RegionID ";
            lcQuery += "WHERE TerritoryID = @TerritoryID ";

            loCmd.CommandText = lcQuery;
            loCmd.AddParameter("@TerritoryID", poNewEntity.TerritoryID);
            loCmd.AddParameter("@TerritoryDescription", poNewEntity.TerritoryDescription);
            loCmd.AddParameter("@RegionID", poNewEntity.RegionID);
            loDb.SqlExecNonQuery(loConn, loCmd, true);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    protected override void R_Deleting(SAB00310DTO poEntity)
    {
        var loEx = new R_Exception();

        try
        {
            var loDb = new R_Db();
            var loConn = loDb.GetConnection("NorthwindConnectionString");

            var lcQuery = "DELETE FROM Territories WHERE TerritoryID = @TerritoryID";

            var loCmd = loDb.GetCommand();
            loCmd.CommandText = lcQuery;
            loCmd.AddParameter("@ProductID", poEntity.TerritoryID);

            loDb.SqlExecNonQuery(loConn, loCmd, true);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    public List<SAB00310DTO> GetAllTerritory()
    {
        var loEx = new R_Exception();
        List<SAB00310DTO> loResult = null;

        try
        {
            var loDb = new R_Db();
            var loConn = loDb.GetConnection("NorthwindConnectionString");

            var lcQuery = "SELECT * FROM Territories (NOLOCK) ORDER BY TerritoryID";
            loResult = loDb.SqlExecObjectQuery<SAB00310DTO>(lcQuery, loConn, true);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();

        return loResult;
    }
    
    public List<SAB00310DTO> GetAllTerritoryByRegion(int piRegionId)
    {
        var loEx = new R_Exception();
        List<SAB00310DTO> loResult = null;

        try
        {
            var loDb = new R_Db();
            var loConn = loDb.GetConnection("NorthwindConnectionString");

            var lcQuery = "SELECT * FROM Territories (NOLOCK) ";
            lcQuery += $"WHERE RegionID = {piRegionId} ";
            loResult = loDb.SqlExecObjectQuery<SAB00310DTO>(lcQuery, loConn, true);
            loResult.ForEach(e => e.TerritoryDescription.Trim());
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();

        return loResult;
    }
}