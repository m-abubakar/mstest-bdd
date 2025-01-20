Feature: Login scenarios

This feature is about verifying the Login

#We work with fixed item prices for now:
#* Username: user
#* Password: pass

Rule: The login is only successful with correct username and password

Scenario: User Tries to login
    Given the home page is open
    And the user navigates to the login page
    When the user enters the username 'John Doe' and password 'ThisIsNotAPassword' into the fields
    Then the user is logged in