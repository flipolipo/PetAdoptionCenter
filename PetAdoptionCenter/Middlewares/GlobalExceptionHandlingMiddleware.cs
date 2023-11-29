using Microsoft.AspNetCore.Mvc;
using SimpleWebDal.Exceptions;
using System.Net;
using System.Text.Json;

namespace PetAdoptionCenter.Middlewares;

public class GlobalExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;
    public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger) => _logger = logger;


    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);

        }
        catch (UserValidationException ex)
        {
            _logger.LogError(ex, ex.Message);
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            ProblemDetails problem = new()
            {
                Status = (int)HttpStatusCode.BadRequest,
                Type = "Bad request for the user",
                Title = "Bad request for the user",
                Detail = ex.Message
            };
            string json = JsonSerializer.Serialize(problem);
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(json);
        }
        catch (ShelterValidationException ex)
        {
            _logger.LogError(ex, ex.Message);
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            ProblemDetails problem = new()
            {
                Status = (int)HttpStatusCode.BadRequest,
                Type = "Bad request for the shelter",
                Title = "Bad request for the shelter",
                Detail = ex.Message
            };
            string json = JsonSerializer.Serialize(problem);
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(json);
        }
        catch (PetValidationException ex)
        {
            _logger.LogError(ex, ex.Message);
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            ProblemDetails problem = new()
            {
                Status = (int)HttpStatusCode.BadRequest,
                Type = "Bad request for the pet",
                Title = "Bad request for the pet",
                Detail = ex.Message
            };
            string json = JsonSerializer.Serialize(problem);
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(json);
        }
        catch (RoleValidationException ex)
        {
            _logger.LogError(ex, ex.Message);
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            ProblemDetails problem = new()
            {
                Status = (int)HttpStatusCode.BadRequest,
                Type = "Bad request for the role",
                Title = "Bad request for the role",
                Detail = ex.Message
            };
            string json = JsonSerializer.Serialize(problem);
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(json);
        }
        catch (DiseaseValidationException ex)
        {
            _logger.LogError(ex, ex.Message);
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            ProblemDetails problem = new()
            {
                Status = (int)HttpStatusCode.BadRequest,
                Type = "Bad request for the disease",
                Title = "Bad request for the disease",
                Detail = ex.Message
            };
            string json = JsonSerializer.Serialize(problem);
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(json);
        }
        catch (VaccinationValidationException ex)
        {
            _logger.LogError(ex, ex.Message);
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            ProblemDetails problem = new()
            {
                Status = (int)HttpStatusCode.BadRequest,
                Type = "Bad request for the vaccination",
                Title = "Bad request for the vaccination",
                Detail = ex.Message
            };
            string json = JsonSerializer.Serialize(problem);
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(json);
        }
        catch (ActivityValidationException ex)
        {
            _logger.LogError(ex, ex.Message);
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            ProblemDetails problem = new()
            {
                Status = (int)HttpStatusCode.BadRequest,
                Type = "Bad request for the activity",
                Title = "Bad request for the activity",
                Detail = ex.Message
            };
            string json = JsonSerializer.Serialize(problem);
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(json);
        }
        catch (CalendarValidationException ex)
        {
            _logger.LogError(ex, ex.Message);
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            ProblemDetails problem = new()
            {
                Status = (int)HttpStatusCode.BadRequest,
                Type = "Bad request for the calendar",
                Title = "Bad request for the calendar",
                Detail = ex.Message
            };
            string json = JsonSerializer.Serialize(problem);
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(json);
        }
        catch (TempHouseValidationException ex)
        {
            _logger.LogError(ex, ex.Message);
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            ProblemDetails problem = new()
            {
                Status = (int)HttpStatusCode.BadRequest,
                Type = "Bad request for the temporary house",
                Title = "Bad request for the temporary house",
                Detail = ex.Message
            };
            string json = JsonSerializer.Serialize(problem);
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(json);
        }
        catch (AdoptionValidationException ex)
        {
            _logger.LogError(ex, ex.Message);
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            ProblemDetails problem = new()
            {
                Status = (int)HttpStatusCode.BadRequest,
                Type = "Bad request for the adoption",
                Title = "Bad request for the adoption",
                Detail = ex.Message
            };
            string json = JsonSerializer.Serialize(problem);
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(json);
        }
        catch (BasicHealthInfoValidationException ex)
        {
            _logger.LogError(ex, ex.Message);
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            ProblemDetails problem = new()
            {
                Status = (int)HttpStatusCode.BadRequest,
                Type = "Bad request for the basic health info for the pet",
                Title = "Bad request for the basic health info for the pet",
                Detail = ex.Message
            };
            string json = JsonSerializer.Serialize(problem);
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(json);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            ProblemDetails problem = new()
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Type = "Server error",
                Title = "Server error",
                Detail = "An internal server error has occured"
            };
            string json = JsonSerializer.Serialize(problem);
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(json);
        }
    }
}
