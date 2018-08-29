Feature: GetMovieFunctionTests
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@1.0 Return a movie
Scenario: GET Test With Valid Request
	Given a Get Movie Function exists
	When I specify 'Mark Hamill' as the lead actor
	And I specify '2017' as the year
	And I execute the Get Movie function
	Then the result should have lead actor 'Mark Hamill'
	And the result should have year '2017'
	And the result should have Title 'The Last Jedi'
	And the result should have Genre 'Comedy'
