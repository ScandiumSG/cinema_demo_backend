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

@tag("Authentication")
namespace Authentication {
    @route("/auth/register")
    @summary("Register user")
    @doc("Register a new application user")
    @post op Register(
        @body PostRegisterRequest: authModels.registerRequest
    ): {
        @statusCode statusCode: 201;
        @body user: authModels.registerResponse;
    } | {
        @statusCode statusCode: 400;
        @body "DuplicateUserName": errors.usernameTaken[];
    };

    @route("/auth/login")
    @summary("Login user")
    @doc("Login a user to the application, generating a JWT token")
    @post op Login(
        @body PostLoginRequest: authModels.loginRequest
    ): {
        @statusCode statusCode: 200;
        @body user: authModels.loginResponse
    };
}

@tag("Movie Endpoint")
namespace MovieEndpoint {
    @route("/movie")
    @summary("Get all")
    @doc("Retrieve a list of movies")
    @get op GetMovies(): {
        @statusCode statusCode: 200;
        @body movies: movieModels.moviesPayload;
    } | {
        @statusCode statusCode: 204;
        @body "No content": errors.noContent;
    };

    @route("/movie/{id}")
    @summary("Get")
    @doc("Retrieve a specific movie based on movieId")
    @get op GetMovie(@path id: integer): {
        @statusCode statusCode: 200;
        @body movie: movieModels.specificMoviePayload;
    } | {
        @statusCode statusCode: 404;
        @body "Not found": errors.notFound;
    };

    @route("/movie")
    @summary("Post")
    @doc("Create a new movie")
    @post op Create(
        @body PostMovie: movieModels.postMovie
    ): {
        @statusCode statusCode: 201;
        @body movie: movieModels.specificMoviePayload;
    } | {
        @statusCode statusCode: 400;
        @body error: errors.badRequest;
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



namespace movieModels {
    model moviesPayload {
        status: string;
        responseTime: string;
        data: movie[];
    }

    model specificMoviePayload {
        status: string;
        responseTime: string;
        data: movie;
    }

    model movie {
        id: int64;
        title: string;
        description: string;
        runtime: int64;
        year: int64;
        rating: string
    }

    model postMovie {
        title: string;
        description: string;
        runtime: int64;
        year: int64;
        rating: string,
    }
}

namespace errors {
    @error
    model usernameTaken {
        code: 400,
        message: "Username taken"
    }

    @error
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
        code: 400,
        message: "Bad request",
    }

    @error
    model noContent {
        code: 204,
        message: "No content",
    }
}