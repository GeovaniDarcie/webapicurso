using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

public class ErrorHandlingMiddelware
{
    private readonly RequestDelegate Next;

    public ErrorHandlingMiddelware(RequestDelegate next)
    {
        this.Next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {

        }
        catch (Exception ex)
        {

        }
        await Next(context);
    }
}