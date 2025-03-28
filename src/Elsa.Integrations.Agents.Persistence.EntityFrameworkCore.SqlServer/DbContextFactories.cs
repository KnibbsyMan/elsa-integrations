﻿using Elsa.EntityFrameworkCore.Abstractions;
using Elsa.EntityFrameworkCore.Extensions;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace Elsa.Integrations.Agents.Persistence.EntityFrameworkCore.SqlServer;

[UsedImplicitly]
public class SqlServerAgentsDbContextFactory : DesignTimeDbContextFactoryBase<AgentsDbContext>
{
    protected override void ConfigureBuilder(DbContextOptionsBuilder<AgentsDbContext> builder, string connectionString)
    {
        builder.UseElsaSqlServer(GetType().Assembly, connectionString);
    }
}