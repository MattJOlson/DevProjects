# string-calc

String Calculator kata. You probably did this in a job interview.

## Tasks

* Write a function `calc` that takes a comma-delimited string of
  integers and adds them
** For an empty string, return zero
** Start with 0, 1, or 2 numbers
* Refactor to allow an arbitrary number of integers
* Refactor to allow newlines and commas as delimiters
* Refactor to allow a `/^\/\/(.)\n$/` first line to specify a special
  delimiter
** Allow three delimiters in this case (',', '\n', and special)? Or
  should "special" be the only delimiter allowed?
* Throw an exception on negative numbers in the string
* Ignore numbers bigger than 1000
