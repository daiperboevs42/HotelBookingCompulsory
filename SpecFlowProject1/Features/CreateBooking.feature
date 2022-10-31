Feature: CreateBooking
	In order to Book a room
	As a Customer
	I want to book a room within a set timeframe i provide

A short summary of the feature

@mytag
Scenario: Book a Room
	Given i have entered a start Date
	And i have also entered a end Date
	When i press book 
	Then the booking should succeed or fail

	#Examples: 
	#| available |
	#| true |
	##| false |
