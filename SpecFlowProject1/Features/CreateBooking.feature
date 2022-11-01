Feature: CreateBooking
	In order to Book a room
	As a Customer
	I want to book a room within a set timeframe i provide

A short summary of the feature

@mytag
Scenario: Book a room with valid dates after occupied timeframe
	Given i have entered a <startDate>
	And i have also entered a <endDate>
	When i press book 
	Then the booking should succeed or fail <available>

	Examples: 
	| startDate | endDate | available |
	| 25        | 30      | true      |
	| 11        | 15      | true      |

@mytag
Scenario: Book a room with valid dates before occupied timeframe 
	Given i have entered a <startDate>
	And i have also entered a <endDate>
	When i press book 
	Then the booking should succeed or fail <available>

	Examples: 
	| startDate | endDate | available |
	| 1         | 4       | true      |
	| 2         | 3       | true      |

@mytag
Scenario: Book a room with both dates in occupied timeframe
Given i have entered a <startDate>
	And i have also entered a <endDate>
	When i press book 
	Then the booking should succeed or fail <available>

	Examples: 
	| startDate | endDate | available |
	| 5        | 10      | false      |
	| 6        | 9       | false      |

@mytag
Scenario: Book a room with one date within occupied days and one in unoccupied days
Given i have entered a <startDate>
	And i have also entered a <endDate>
	When i press book 
	Then the booking should succeed or fail <available>

	Examples: 
	| startDate | endDate | available |
	| 2         | 7       | false     |
	| 8         | 14      | false     |