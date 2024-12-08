using FluentMigrator.Builders.Create.Table;
using System.Diagnostics.CodeAnalysis;

namespace User.Login.Infrastructure.Migrations;

[ExcludeFromCodeCoverage]
public class VersionBase
{
    public static ICreateTableColumnOptionOrWithColumnSyntax InsertStandardColumns(ICreateTableWithColumnOrSchemaOrDescriptionSyntax table)
    {
        return table
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("RegistrationDate").AsDateTime().NotNullable();
    }
}
