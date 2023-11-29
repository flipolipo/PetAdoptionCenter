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
                Type = "Bad request for user",
                Title = "Bad request for user",
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
                Type = "Bad request for shelter",
                Title = "Bad request for shelter",
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
                Type = "Bad request for pet",
                Title = "Bad request for pet",
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
                Type = "Bad request for role",
                Title = "Bad request for role",
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
                Type = "Bad request for disease",
                Title = "Bad request for disease",
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
                Type = "Bad request for vaccination",
                Title = "Bad request for vaccination",
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
                Type = "Bad request for activity",
                Title = "Bad request for activity",
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
                Type = "Bad request for calendar",
                Title = "Bad request for calendar",
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
                Type = "Bad request for temporary house",
                Title = "Bad request for temporary house",
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
                Type = "Bad request for adoption",
                Title = "Bad request for adoption",
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
