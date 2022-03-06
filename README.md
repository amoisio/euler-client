# Project Euler Client

A Project Euler web site (https://projecteuler.net/) client which allows users to generate files based on the problem details 
and a user provided template. 

For example, the client can be used to generate a Vue single-file components which displays the number, title and full description 
of the error and provides a placeholder for you code.

## Usage

The client comes with a simple cli with syntax

``` bash
$ euler <problem> -t <template> -o <output> -m <mode>
```

where 
* `<problem>` is the project euler problem number.
* `<template>` is optional, and is the path to the template file. If not given, then the client will only output the problem details in the terminal.
* `<output>` is optional, and is the path to the output file. If not given, then the client will output the template file contents merged with problem details.
* `<mode>` is optional, and allows overwriting an existing output file. If not given, the application will give an error if targeting an existing output file.

## Template

The template file can be any readable file. The following placeholders can be used within the template to fill in the information from the Project Euler problem.

Placeholder | Description 
--- | --- 
`##Title##` | Problem title
`##No##` | Problem number
`##Details##`| Problem description as an HTML fragment.
`##Ref##` | Problem url.
