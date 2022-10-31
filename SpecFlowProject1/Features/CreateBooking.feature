﻿Feature: CreateBooking
	In order to Book a room
	As a Customer
	I want to book a room within a set timeframe i provide

A short summary of the feature

@mytag
Scenario: Book a Room
	Given i have entered a <startDate>
	And i have also entered a <endDate>
	When i press book 
	Then the booking should succeed or fail <available>

	Examples: 
	| startDate   | endDate      | available |
	| '01-11-2022'| '02-11-2022' | true |
	| '02-11-2022'| '02-11-2022' | false |