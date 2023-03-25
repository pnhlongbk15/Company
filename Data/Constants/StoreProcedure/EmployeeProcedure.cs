namespace Data.Constants.StoreProcedure
{
    public static class EmployeeProcedure
    {
        public readonly static string Insert = @"CREATE PROCEDURE Employees_Insert 
                                                        @Id nvarchar(max), @FirstName nvarchar(max), @LastName nvarchar(max),
                                                        @DateOfBirth Date, @PhoneNumber nvarchar(max), @Email nvarchar(max), @DepartmentName nvarchar(max)
                                        AS
                                        BEGIN
                                                DECLARE @DepartmentId nvarchar(max)
                                                SELECT @DepartmentId = Id FROM Departments WHERE Name = @DepartmentName
                                                
                                                INSERT Employees (Id,FirstName,LastName,DateOfBirth,PhoneNumber,Email,DepartmentId) 
                                                       VALUES (@Id,@FirstName,@LastName,@DateOfBirth,@PhoneNumber,@Email,@DepartmentId)
                                        END
                                        GO";

        public readonly static string Update = @"CREATE PROCEDURE Employees_Update 
                                                        @Id nvarchar(max), @FirstName nvarchar(max), @LastName nvarchar(max),
                                                        @DateOfBirth Date, @PhoneNumber nvarchar(max), @Email nvarchar(max), @DepartmentName nvarchar(max)
                                        AS
                                        BEGIN
                                                DECLARE @DepartmentId nvarchar(max)
                                                SELECT @DepartmentId = Id FROM Departments WHERE Name = @DepartmentName
                                                
                                                UPDATE Employees SET FirstName=@FirstName,LastName=@LastName,DateOfBirth=@DateOfBirth,PhoneNumber=@PhoneNumber,Email=@Email,DepartmentId=@DepartmentId 
                                                                 WHERE Id=@Id
                                        END
                                        GO";
        public readonly static string Delete = @"CREATE PROCEDURE Employees_Delete 
                                                        @Email nvarchar(max), @DepartmentName nvarchar(max)
                                        AS
                                        BEGIN
                                                DECLARE @DepartmentId nvarchar(max)
                                                SELECT @DepartmentId = Id FROM Departments WHERE Name = @DepartmentName

                                                DELETE FROM Employees  
                                                       WHERE Email = @Email AND DepartmentId = @DepartmentId
                                        END
                                        GO";
    }
}
