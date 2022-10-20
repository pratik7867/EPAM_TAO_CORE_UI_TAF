Feature: PostsFeature
	Posts feature to create, update and delete post(s)

Background: 
	Given I have the baseURL 'http://localhost:3000'

@mytag
Scenario Outline: 01) Create a post
	Given I create a post using resource '<resource>', id '<id>', title '<title>' and author '<author>'	
	When I execute the create post request
	Then I should be able to get the post with id '<id>', title '<title>', author '<author>' and status '<status>'

	Examples: 
	| resource | id | title        | author    | status |
	| /posts   | 2  | json-server2 | typicode2 | 201	|
	| /posts   | 3  | json-server3 | typicode3 | 201	|

@mytag
Scenario Outline: 02) Update a post
	Given I update a post using resource '<resource>', id '<id>', title '<title>' and author '<author>'	
	When I execute the update post request
	Then I should be able to get the updated post with id '<id>', title '<title>', author '<author>' and status '<status>'

	Examples: 
	| resource | id | title        | author    | status |
	| /posts/2 | 2  | json-server3 | typicode3 | 200	|
	| /posts/3 | 3  | json-server4 | typicode4 | 200	|

@mytag
Scenario Outline: 03) Delete a post
	Given I delete a post using resource '<resource>'
	When I execute the delete post request
	Then I should get the status '<status>'

	Examples: 
	| resource | status |
	| /posts/2 | 200    |
	| /posts/3 | 200    |