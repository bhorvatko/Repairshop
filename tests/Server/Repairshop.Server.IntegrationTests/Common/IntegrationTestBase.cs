﻿using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repairshop.Server.Features.WarrantManagement.Data;
using Repairshop.Server.Features.WarrantManagement.Procedures;
using Repairshop.Server.Features.WarrantManagement.Technicians;
using Repairshop.Server.Features.WarrantManagement.Warrants;
using Xunit.Abstractions;

namespace Repairshop.Server.IntegrationTests.Common;

[Collection(TestConstants.Collections.IntegrationTests)]
public class IntegrationTestBase
{
    protected HttpClient _client;
    protected WarrantManagementDbContext _dbContext;
    protected HubConnection _hubConnection;

    public IntegrationTestBase(
        ITestOutputHelper testOutputHelper,
        DatabaseFixture databaseFixture)
    {
        ApiFactory factory = new ApiFactory(
            testOutputHelper,
            databaseFixture.ConnectionString);

        _client = factory.CreateClient();
        _client.DefaultRequestHeaders.Add("X-API-KEY", TestConstants.ApiKey);

        var scope = factory.Services.CreateScope();
        
        _dbContext = scope.ServiceProvider.GetRequiredService<WarrantManagementDbContext>();

        ClearDatabase();

        _hubConnection = new HubConnectionBuilder()
            .WithUrl(
                "https://localhost/Notifications",
                options =>
                {
                    options.HttpMessageHandlerFactory = _ => factory.Server.CreateHandler();
                    options.Headers.Add("X-API-KEY", TestConstants.ApiKey);
                })
            .Build();
    }

    private void ClearDatabase()
    {
        _dbContext.Set<WarrantStepTransition>().ExecuteDelete();
        _dbContext.Set<Warrant>().ExecuteDelete();
        _dbContext.Set<WarrantStep>().ExecuteDelete();
        _dbContext.Set<Procedure>().ExecuteDelete();
        _dbContext.Set<Technician>().ExecuteDelete();
    }
}
