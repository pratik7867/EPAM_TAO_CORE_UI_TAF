Feature: CheckoutFeature
	Checkout workflow for checking out with product(s)

@mytag
Scenario: Checkout with product
	Given the user is logged in with valid credentials "standard_user", "secret_sauce"	
	When the user adds product to the cart "Sauce Labs Backpack"
	When the user navigates to cart page
	Then the product should be added to the cart with product price
	When the user navigates to checkout page
	When the user fills up checkout details "Test_FName", "Test_LName", "12345" and click on continue
	Then the product should be checkedout with product name, product price "Sauce Labs Backpack", "$29.99"