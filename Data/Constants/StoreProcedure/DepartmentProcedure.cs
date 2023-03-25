namespace Data.Constants.StoreProcedure
{
    public static class DepartmentProcedure
    {
        public readonly static string Insert = @"CREATE PROCEDURE Departments_Insert 
                                                        @Id nvarchar(max), @Name nvarchar(max)
                                        AS
                                        BEGIN
                                                INSERT Departments (Id,Name) 
                                                       VALUES (@Id,@Name)
                                        END
                                        GO";

        public readonly static string Update = @"CREATE PROCEDURE Departments_Update 
                                                        @Id nvarchar(max), @Name nvarchar(max)
                                        AS
                                        BEGIN
                                                UPDATE Departments SET Name=@Name
                                                       WHERE Id=@Id 
                                        END
                                        GO";
    }
}
