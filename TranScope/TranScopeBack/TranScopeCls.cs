using System.Data;
using System.Data.Common;
using System.Transactions;
using R_BackEnd;
using R_Common;
using TranScopeCommon;

namespace TranScopeBack;

public class TranScopeCls
{
    public TranScopeDataDTO ProcessWthoutTransaction(int poProcessRecordCount)
    {
        R_Exception loException = new R_Exception();
        TranScopeDataDTO loRtn = new TranScopeDataDTO();
        List<CustomerDbDTO> Customers = null;

        try
        {
            Customers = GetAllCustomer(poProcessRecordCount);
            RemoveAllCustomer(Customers);
            AddAllCopyCustomer(Customers);
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        EndBlock:
        loException.ThrowExceptionIfErrors();
        return loRtn;
    }

    private void RemoveAllCustomer(List<CustomerDbDTO> poCustomers)
    {
        R_Exception loException = new R_Exception();
        R_Db loDb = null;
        DbConnection loConn = null;
        DbCommand loCommand;
        DbParameter loDbParameter;
        string lcCmd;

        try
        {
            loDb = new R_Db();
            loCommand = loDb.GetCommand();
            loConn = loDb.GetConnection();
            loDb.R_AddCommandParameter(loCommand, "StrPar1", DbType.String, 50, "");
            loDbParameter = loCommand.Parameters[0];

            foreach (CustomerDbDTO item in poCustomers)
            {
                lcCmd = "DELETE FROM TestCustomer WHERE CustomerID = @StrPar1";
                loCommand.CommandText = lcCmd;
                loDbParameter.Value = item.CustomerID;
                loDb.SqlExecNonQuery(loConn, loCommand, false);

                lcCmd = "INSERT INTO TetsCustomerLog(Log) VALUES (@StrPar1)";
                loCommand.CommandText = lcCmd;
                loDbParameter.Value = $"Remove Customer {item.CustomerID}";
                loDb.SqlExecNonQuery(loConn, loCommand, false);
            }
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }
        finally
        {
            if (loConn != null)
            {
                if (loConn.State != ConnectionState.Closed)
                {
                    loConn.Close();
                }

                loConn.Dispose();
            }
        }

        EndBlock:
        loException.ThrowExceptionIfErrors();
    }

    private void AddAllCopyCustomer(List<CustomerDbDTO> poCustomers)
    {
        R_Exception loException = new R_Exception();
        DbConnection loConn = null;
        DbCommand loCommand;
        R_Db loDb = null;
        
        DbParameter loDbParameter1;
        DbParameter loDbParameter2;
        DbParameter loDbParameter3;
        
        string lcCmd;

        try
        {
            loDb = new R_Db();
            loCommand = loDb.GetCommand();
            loConn = loDb.GetConnection();
            
            loDb.R_AddCommandParameter(loCommand, "CustomerID", DbType.String, 50, "");
            loDb.R_AddCommandParameter(loCommand, "CustomerName", DbType.String, 50, "");
            loDb.R_AddCommandParameter(loCommand, "ContactName", DbType.String, 50, "");
            loDbParameter1 = loCommand.Parameters[0];
            loDbParameter2 = loCommand.Parameters[1];
            loDbParameter3 = loCommand.Parameters[2];

            foreach (CustomerDbDTO item in poCustomers)
            {
                lcCmd =
                    "INSERT INTO TestCustomer(CustomerID, CustomerName, ContactName) VALUES (@StrPar1, @StrPar2, @StrPar3)";
                loCommand.CommandText = lcCmd;
                 
                loDbParameter1.Value = item.CustomerID;
                loDbParameter2.Value = item.CustomerName;
                loDbParameter3.Value = item.ContactName;

                loDb.SqlExecNonQuery(loConn, loCommand, false);

                lcCmd = "INSERT INTO TetsCustomerLog(Log) VALUES (@StrPar1)";
                loCommand.CommandText = lcCmd;
                loDbParameter1.Value = $"Add Customer {item.CustomerID}";
                loDb.SqlExecNonQuery(loConn, loCommand, false);
            }
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }
        finally
        {
            if (loConn != null)
            {
                if (loConn.State != ConnectionState.Closed)
                {
                    loConn.Close();
                }

                loConn.Dispose();
            }
        }

        EndBlock:
        loException.ThrowExceptionIfErrors();
    }

    private List<CustomerDbDTO> GetAllCustomer(int pnCount)
    {
        R_Exception loException = new R_Exception();
        List<CustomerDbDTO> loRtn = null;
        R_Db loDb;
        string lcCust;
        string lcCmd;

        try
        {
            lcCust = string.Format("Cust{0}", pnCount.ToString("0000"));
            lcCmd = "SELECT * FROM TestCustomer(nolock) WHERE CustomerID <= Cust{0}";
            loDb = new R_Db();
            loRtn = loDb.SqlExecObjectQuery<CustomerDbDTO>(lcCmd, lcCust);
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        EndBlock:
        loException.ThrowExceptionIfErrors();

        return loRtn;
    }
}