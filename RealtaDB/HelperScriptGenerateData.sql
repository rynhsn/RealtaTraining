
select * from TestCustomer
select * from TestCustomerLog
select * from TestCopyCustomer



delete TestCustomer
Delete TestCopyCustomer
delete TestCustomerLog

DECLARE @Counter INT 
SET @Counter=1
WHILE ( @Counter <= 10)
BEGIN
    insert into TestCustomer(CustomerID,CustomerName,ContactName) values('Cust'+cast(@Counter as varchar(4)),'Name'+cast(@Counter as varchar(4)),'Contact'+cast(@Counter as varchar(4)))

    SET @Counter  = @Counter  + 1
END
