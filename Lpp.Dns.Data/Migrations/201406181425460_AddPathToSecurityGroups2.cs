namespace Lpp.Dns.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPathToSecurityGroups2 : DbMigration
    {
        public override void Up()
        {
            Sql(@"ALTER TRIGGER [dbo].[SecurityGroups_InsertUpdateItem] 
    ON  [dbo].SecurityGroups
    AFTER INSERT, UPDATE
AS 
BEGIN
    IF (EXISTS(SELECT NULL FROM INSERTED))    
	    UPDATE SecurityGroups SET Path = ISNULL((SELECT TOP 1 Name FROM Organizations WHERE Organizations.ID = SecurityGroups.OwnerID), (SELECT TOP 1 Name FROM Projects WHERE Projects.ID = SecurityGroups.OwnerID)) WHERE SecurityGroups.ID IN (SELECT I.ID FROM INSERTED I JOIN DELETED D ON I.ID = D.ID WHERE I.Name <> D.Name OR I.OwnerID <> D.OwnerID)
END");

            Sql(@"UPDATE SecurityGroups SET Path = ISNULL((SELECT TOP 1 Name FROM Organizations WHERE Organizations.ID = SecurityGroups.OwnerID), (SELECT TOP 1 Name FROM Projects WHERE Projects.ID = SecurityGroups.OwnerID)) + '\' + Name");
        }
        
        public override void Down()
        {
        }
    }
}
