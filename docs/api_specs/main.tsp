import "@typespec/http";
import "@typespec/rest";
import "@typespec/openapi3";

using TypeSpec.Http;
using TypeSpec.Rest;

@service({
    title: "Cinema Demo API"
})
@server("sgcinemad.azurewebsites.net", "Azure hosted site")
namespace CinemaDemo;

@tag("CinemaDemo")
namespace Authentication {
    @route("/auth/register")
    @summary("Register user")
    @doc("Register a new application user")
    @post op Register(): {
        @statusCode statusCode: 201;
        @body user: authModels.registerResponse;
    } | {
        @statusCode statusCode: 400;
        @body "DuplicateUserName": errors.usernameTaken;
    };
}


namespace authModels {
    model registerRequest {
        email: string,
        username: string,
        password: string,
    }

    model registerResponse {
        email: string,
        username: string,
        role: string,
    }

    model loginRequest {
        email: string,
        password: string,
    }
    
    model loginResponse {
        id: string,
        username: string,
        email: string,
        role: string,
        token: string,
    }
}

namespace errors {
    @error
    model usernameTaken {
        code: 400,
        message: "Username taken"
    }

    model emailTaken {
        code: 400,
        message: "email taken"
    }

    @error
    model notFound {
        code: 404, 
        message: "Not found",
    }

    @error
    model badRequest {
        code: 401,
        message: "Bad request",
    }
}