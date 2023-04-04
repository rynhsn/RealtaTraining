using System.Data;
using System.Data.Common;
using System.Transactions;
using R_BackEnd;
using R_Common;
using TranScopeCommon;

namespace TranScopeBack;

public class TranScopeCls
{
    public TranScopeDataDTO ProcessWithoutTransactionDB(int poProcessRecordCount)
    {
        R_Exception loException = new R_Exception();
        TranScopeDataDTO loRtn = new TranScopeDataDTO();
        List<CustomerDbDTO> Customers;

        try
        {
            Customers = GetAllCustomer(poProcessRecordCount);
            RemoveAllCustomer1(Customers);
            AddAllCopyCustomer(Customers);

            loRtn.IsSuccess = true;
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        EndBlock:
        loException.ThrowExceptionIfErrors();
        return loRtn;
    }

    public TranScopeDataDTO ProcessAllWithTransactionDB(int poProcessRecordCount)
    {
        R_Exception loException = new R_Exception();
        TranScopeDataDTO loRtn = new TranScopeDataDTO();
        List<CustomerDbDTO> Customers;

        try
        {
            Customers = GetAllCustomer(poProcessRecordCount);

            using (TransactionScope TranScope = new TransactionScope(TransactionScopeOption.Required))
            {
                RemoveAllCustomer(Customers);
                AddAllCopyCustomer(Customers);
                TranScope.Complete();
            }

            loRtn.IsSuccess = true;
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        EndBlock:
        loException.ThrowExceptionIfErrors();
        return loRtn;
    }

    public TranScopeDataDTO ProcessEachTransactionDB(int poProcessRecordCount)
    {
        R_Exception loException = new R_Exception();
        TranScopeDataDTO loRtn = new TranScopeDataDTO();
        List<CustomerDbDTO> Customers;
        int lnCount;

        try
        {
            Customers = GetAllCustomer(poProcessRecordCount);
            lnCount = 0;
            foreach (CustomerDbDTO item in Customers)
            {
                try
                {
                    using (TransactionScope TranScope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        RemoveAllCustomer(Customers);
                        AddAllCopyCustomer(Customers);

                        if ((lnCount % 3) == 0)
                        {
                            loException.Add("001", $"Error: at number {lnCount.ToString()} of {poProcessRecordCount} records");
                            goto EndDetail;
                        }
                        
                        TranScope.Complete();
                    }
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                }
                EndDetail:
                lnCount++;

            }

            loRtn.IsSuccess = true;
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        EndBlock:
        loException.ThrowExceptionIfErrors();
        return loRtn;
    }

    private void RemoveAllCustomer1(List<CustomerDbDTO> poCustomers)
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
            loConn = loDb.GetConnection();
            loCommand = loDb.GetCommand();
            loDb.R_AddCommandParameter(loCommand, "StrPar1", DbType.String, 50, "");
            loDbParameter = loCommand.Parameters[0]; //0 adalah parameter pertama yaitu @strpar1

            foreach (CustomerDbDTO item in poCustomers)
            {
                lcCmd = "DELETE FROM TestCustomer WHERE CustomerID = @StrPar1";
                loCommand.CommandText = lcCmd;
                loDbParameter.Value =
                    item.CustomerID; // ini adalah parameter yang dikirim yaitu @strpar1 pada command delete
                loDb.SqlExecNonQuery(loConn, loCommand, false);

                lcCmd = "INSERT INTO TestCustomerLog(Log) VALUES (@StrPar1)";
                loCommand.CommandText = lcCmd;
                loDbParameter.Value =
                    $"Move Customer {item.CustomerID}"; // ini adalah parameter yang dikirim yaitu @strpar1 pada command insert
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
        int lnCount;

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

            loDbParameter1 = loCommand.Parameters["CustomerID"];
            loDbParameter2 = loCommand.Parameters["CustomerName"];
            loDbParameter3 = loCommand.Parameters["ContactName"];

            lcCmd =
                "INSERT INTO TestCopyCustomer(CustomerID, CustomerName, ContactName) VALUES (@CustomerID, @CustomerName, @ContactName)";
            loCommand.CommandText = lcCmd;
            lnCount = 1;

            foreach (CustomerDbDTO item in poCustomers)
            {
                if ((lnCount % 3) == 0)
                {
                    loException.Add("001", "Error: Count is 3");
                }

                loDbParameter1.Value = item.CustomerID;
                loDbParameter2.Value = item.CustomerName;
                loDbParameter3.Value = item.ContactName;

                loDb.SqlExecNonQuery(loConn, loCommand, false);
                lnCount++;
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
            lcCust = String.Format("Cust{0}", pnCount.ToString("0000"));
            lcCmd = "SELECT * FROM TestCustomer(nolock) WHERE CustomerID <= {0}";
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

    private void RemoveAllCustomer(List<CustomerDbDTO> poCustomers)
    {
        R_Exception loException = new R_Exception();
        try
        {
            foreach (CustomerDbDTO item in poCustomers)
            {
                RemoveEachCustomer(item);
                AddLogEachCustomer(item);
            }
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        EndBlock:
        loException.ThrowExceptionIfErrors();
    }

    private void RemoveEachCustomer(CustomerDbDTO poCustomer)
    {
        R_Exception loException = new R_Exception();
        R_Db loDb = null;
        DbConnection loConn = null;
        DbCommand loCommand;
        string lcCmd;
        DbParameter loDbParameter;

        try
        {
            loDb = new R_Db();
            loCommand = loDb.GetCommand();
            loConn = loDb.GetConnection();
            loDb.R_AddCommandParameter(loCommand, "StrPar1", DbType.String, 50, "");
            loDbParameter = loCommand.Parameters[0];

            lcCmd = "delete from TestCustomer where CustomerID=@StrPar1";
            loCommand.CommandText = lcCmd;
            loDbParameter.Value = poCustomer.CustomerID;
            loDb.SqlExecNonQuery(loConn, loCommand, false);
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

    private void AddLogEachCustomer(CustomerDbDTO poCustomer)
    {
        R_Exception loException = new R_Exception();
        R_Db loDb = null;
        DbConnection loConn = null;
        DbCommand loCommand;
        string lcCmd;
        DbParameter loDbParameter;

        try
        {
            using (TransactionScope TransScope = new TransactionScope(TransactionScopeOption.Suppress))
            {
                loDb = new R_Db();
                loCommand = loDb.GetCommand();
                loConn = loDb.GetConnection();
                loDb.R_AddCommandParameter(loCommand, "StrPar1", DbType.String, 50, "");
                loDbParameter = loCommand.Parameters[0];

                lcCmd = "insert into TestCustomerLog(Log) Values(@StrPar1)";
                loCommand.CommandText = lcCmd;
                loDbParameter.Value = $"Remove Customer {poCustomer.CustomerID}";
                loDb.SqlExecNonQuery(loConn, loCommand, false);
                // TransScope.Complete();
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
}