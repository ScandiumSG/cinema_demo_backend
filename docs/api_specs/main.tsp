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

    @route("/movie")
    @summary("Put")
    @doc("Update a movie")
    @put op Update(
        @body PutMovie: movieModels.postMovie
    ): {
        @statusCode statusCode: 201;
        @body movie: movieModels.specificMoviePayload;
    } | {
        @statusCode statusCode: 400;
        @body error: errors.badRequest;
    } | {
        @statusCode statusCode: 404;
        @body error: errors.notFound;
    };

    @route("/movie")
    @summary("Delete")
    @doc("Delete a movie")
    @delete op Delete(
        @query movieId: integer
    ): {
        @statusCode statusCode: 200;
        @body deletedMovie: movieModels.specificMoviePayload;
    } | {
        @statusCode statusCode: 404;
        @body error: errors.notFound;
    };
}

@tag("Screening Endpoint")
namespace ScreeningEndpoint {
    @route("/screening")
    @summary("Get all")
    @doc("Get a array of screenings")
    @get op GetAll(): {
        @statusCode statusCode: 200;
        @body screenings: screeningModels.multipleScreeningPayload;
    } | {
        @statusCode statusCode: 204;
        @body error: errors.noContent;
    };

    @route("/screening/{screeningId}-{movieId}")
    @summary("Get")
    @doc("Get a specific screening, defined by both screeningId and movieId")
    @get op Get(
        @path screeningId: integer;
        @path movieId: integer;
    ): {
        @statusCode statusCode: 200;
        @body screening: screeningModels.specificScreeningPayload;
    } | {
        @statusCode statusCode: 404;
        @body error: errors.notFound;
    };

    @route("/screening")
    @summary("Post")
    @doc("Create a new screening")
    @post op Create(
        @body screening: screeningModels.postScreening;
    ): {
        @statusCode statusCode: 201;
        @body screening: screeningModels.specificScreeningPayload;
    } | {
        @statusCode statusCode: 400;
        @body error: errors.badRequest;
    };

    @route("/screening")
    @summary("Put")
    @doc("Update a screening value")
    @put op Update(
        @body screenig: screeningModels.postScreening;
    ): {
        @statusCode statusCode: 201;
        @body screening: screeningModels.specificScreeningPayload;
    } | {
        @statusCode statusCode: 400;
        @body error: errors.badRequest;
    };

    @route("/screening/{screeningId}-{movieId}")
    @summary("Delete")
    @doc("Delete a screening")
    @delete op Delete(
        @path screeningId: integer;
        @path movieId: integer;
    ): {
        @statusCode statusCode: 200;
        @body screening: screeningModels.specificScreeningPayload;
    } | {
        @statusCode statusCode: 404;
        @body error: errors.notFound; 
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
        id: integer;
        title: string;
        description: string;
        runtime: integer;
        year: integer;
        rating: string
    }

    model postMovie {
        title: string;
        description: string;
        runtime: integer;
        year: integer;
        rating: string,
    }
}

namespace screeningModels {
    model multipleScreeningPayload {
        status: string;
        responseTime: string;
        data: screening[];
    }

    model specificScreeningPayload {
        status: string;
        responseTime: string;
        data: screening;
    }

    model screening {
        id: integer;
        movie: movieModels.movie;
        theater: theaterModels.theater;
        tickets: [];
        startTime: string;
    }

    model postScreening {
        movieId: integer;
        theaterId: integer;
        startTime: string;
    }
}


namespace theaterModels {
    model theater {
        id: integer;
        capacity: integer;
        name: string;
        seats: seatModels.seat[];
    }
}

namespace seatModels {
    model seat {
        id: integer;
        row: integer;
        seatNumber: integer;
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