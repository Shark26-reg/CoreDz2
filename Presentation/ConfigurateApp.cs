namespace Presentation;

public static class ConfigurateApp
{
    public static WebApplication ConfigurateRequestPipelines(this WebApplication app)
    {
        app
            .UseInDevelopeMode()
            .UsePipelines();
        return app;
    }

    private static WebApplication UsePipelines(this WebApplication app)
    {
        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
        
        return app;
    }

    private static WebApplication UseInDevelopeMode(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        return app;
    }
}
