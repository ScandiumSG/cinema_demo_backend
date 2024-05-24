# About

This folder contains API documentation, contained with the specs.html file.

The specifications were created using [TypeSpec](https://typespec.io/), which generates a openapi.yaml file which is then converted to a [Redocly](https://redocly.com/) html spec sheet.

The API definition is done in the `main.tsp` file.

## How to run

To generate the files node is required, then run a `npm install -g @typespec/compiler`.

```#Bash
npm run generate
```

<details>

The generate command will first install the project via `npm install`, then generate the openapi.yaml file, by doing the command 

```#Bash
tsp compile .
```

The command will then execute the redocly build command:

```#Bash
redocly build-docs ./tsp-output/@typespec/openapi3/openapi.yaml -o api-specs.html
```

</details>
