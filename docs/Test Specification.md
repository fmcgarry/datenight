# Overview

A comprehensive suite of testing scenarios.

# Unit Tests

The tests defined in this section are separated by their respective projects.

## DateNight.App

Since this is the UI project, these tests comprise of safe guards for user input and error checking user data. They do not follow the naming format like the rests of do because they are UI tests. 

### Join Couple Dialog: Couple Code input is more than 8 characters

**Description:** User inputs more that 8 characters in the couple code field after creating their account.  
**Related Issues:** #5  
**URL:**  
**Preconditions:** Filled out information on create account page and pressed Sign Up button. User is on the enter couple code screen.

| Step | Action                       | Expected Response     |
|:----:| ---------------------------- | --------------------- |
|  1   | Enter more than 8 characters | An error is displayed | 

**Post-Conditions:** An error is displayed to the user and the user remains on the couple code screen.  
**Associated Requirements:** 2.1.1

### Join Couple Dialog: Couple Code is user's own code

**Description:** User input's their own code into the couple code field after creating their account.  
**Related Issues:** #5  
**URL:**  
**Preconditions:** Filled out information on create account page and pressed Sign Up button. User is on the enter couple code screen.

| Step | Action                                                           | Expected Response     |
|:----:| ---------------------------------------------------------------- | --------------------- |
|  1   | Enter the same code that is presented into the couple code field | An error is displayed |

**Post-Conditions:** An error is displayed to the user and the user remains on the couple code screen.  
**Associated Requirements:** 2.2.1

### Create Idea Page: Required Title field

**Description:** Ensure that the user has filled out the Title field on the Create Idea page on submit.  
**Related Issues:** #2  
**URL:**  
**Preconditions:** The user is on the Create Idea page 

| Step | Action                                                    | Expected Response |
|:----:| --------------------------------------------------------- | ----------------- |
|  1   | Fill out description field, leaving the title field empty |                   |
|  2   | Press submit button                                       |                   |

**Post-Conditions:** The idea is not submitted, and error is displayed, and the user stays on the Create Idea page.  
**Associated Requirements:** 5.3.1

### Edit Idea Dialog: All Required Fields Have Data

**Description:** When a user is editing ideas make sure the Title field has data.  
**Related Issues:** #8  
**URL:**  
**Preconditions:** The user is editing an idea.

| Step | Action                | Expected Response     |
|:----:| --------------------- | --------------------- |
|  1   | Delete all field data |                       |
|  2   | Press the Save button | An error is displayed | 

**Post-Conditions:** The user stays on the Edit dialog and has the option to either fix the invalid data or quit without saving.  
**Associated Requirements:** 8.1

### Edit Idea Dialog: Created On Date Is Not In The Future

**Description:** When editing an idea, make sure the user doesn't set the Created On date in the future.  
**Related Issues:** #8  
**URL:**  
**Preconditions:** A user is editing an idea.

| Step | Action                                          | Expected Response     |
|:----:| ----------------------------------------------- | --------------------- |
|  1   | Set the Created On date to a date in the future | An error is displayed | 

**Post-Conditions:** The user stays on the edit page and has the option to fix their error or close without saving.  
**Associated Requirements:** 8.1

## DateNight.Core

This project contains most of the core business logic of the application, therefore, has the most unit tests.

### AddIdeaAsync_WhenNewIdea_ThenCreatedOnDateIsToday

**Description:**  When a new idea is created, then the CreatedOn field is set to the current date.
**Related Issues:**  #2
**URL:**  
**Preconditions:** None

| Step | Action                                                                 | Expected Response |
|:----:| ---------------------------------------------------------------------- | ----------------- |
|  1   | Call the AddIdeaAsync method with a valid Idea object                  | Success           |
|  2   | Verify CreatedOn date is today                                         | Success           | 

**Post-Conditions:**  The idea created has a CreatedOn date of today
**Associated Requirements:** 5.4
### UpdateIdea_WhenIdeaDoesNotExist_ThrowsArgumentException

**Description:** When an idea is submitted to the UpdateIdea method, ensure it exists first before trying to update it  
**Related Issues:** #8  
**URL:**  
**Preconditions:** None

| Step | Action                                                             | Expected Response           |
|:----:| ------------------------------------------------------------------ | --------------------------- |
|  1   | Call the UpdateIdea method with an idea that hasn't been added yet | ArgumentException is thrown | 

**Post-Conditions:** The UpdateIdea method has thrown an ArgumentException  
**Associated Requirements:** 8.1

### DeleteIdea_WhenIdeaDoesNotExist_ThrowsArgumentException

**Description:** When an idea ID is submitted for deletion, ensure the idea exists.  
**Related Issues:** #9  
**URL:**  
**Preconditions:** None

| Step | Action                                                   | Expected Response        |
|:----:| -------------------------------------------------------- | ------------------------ |
|  1   | Call the DeleteIdea method with an ID that doesn't exist | ArgumentException is thrown | 

**Post-Conditions:** The DeleteIdea method has thrown an ArgumentException  
**Associated Requirements:** 8.2

### DeleteIdea_WhenIdeaExists_ThenIdeaRemovedFromCollection

**Description:**  When an idea is requested to be deleted and it exists in the collection, remove it from the collection.
**Related Issues:**  #9
**URL:**  
**Preconditions:** An idea exists in the collection

| Step | Action                     | Expected Response |
|:----:| -------------------------- | ----------------- |
|  1   | Call the DeleteIdea method | Success                  |

**Post-Conditions:**  The caller is returned a success.
**Associated Requirements:** 8.2

### GetIdeas_WhenCalled_ReturnsOnlyUserIdeas

**Description:** When calling the GetIdeas method, only the ideas created by the current user are returned.  
**Related Issues:** #7  
**URL:**  
**Preconditions:** Ideas have been added via the AddIdeas method. User has been created and ID is known.

| Step | Action                            | Expected Response           |
|:----:| --------------------------------- | --------------------------- |
|  1   | Call GetIdeas method with user id | A list of ideas is returned | 

**Post-Conditions:** A list of ONLY the user's ideas has been returned.  
**Associated Requirements:** 8

### GetIdeas_WhenNoIdeasInCollection_ReturnsEmpty

**Description:**  When there are not ideas in the collection for a user, then return an empty collection.
**Related Issues:**  #7
**URL:**  
**Preconditions:** None

| Step | Action               | Expected Response |
|:----:| -------------------- | ----------------- |
|  1   | Call GetIdeas method | Empty collection  | 

**Post-Conditions:**  The caller is returned an empty collection.
**Associated Requirements:** 8
### ActivateIdea_WhenNewIdeaActivated_ThenCurrentActiveIdeaDeactivated

**Description:** Multiple active Ideas are not allowed.  
**Related Issues:** #6  
**URL:**  
**Preconditions:** An idea is already marked as active and a new Idea ID is known

| Step | Action                                                 | Expected Response       |
|:----:| ------------------------------------------------------ | ----------------------- |
|  1   | Submit an Idea ID to the ActivateIdea method           | Success                 |
|  2   | Query all Ideas using GetIdeas method for active state | Only 1 Idea is returned | 

**Post-Conditions:** None  
**Associated Requirements:** 4.1.1.1

### ActivateIdea_WhenIdeaDoesNotExist_ThrowsArgumentException

**Description:** Activation requests for ideas that haven't been created yet should fail.  
**Related Issues:** #6  
**URL:**  
**Preconditions:** A random Idea ID is known

| Step | Action                                | Expected Response              |
|:----:| ------------------------------------- | ------------------------------ |
|  1   | Submit Idea ID to ActivateIdea method | An ArgumentException is thrown | 

**Post-Conditions:** No ideas have been marked active  
**Associated Requirements:** 4.1.1

### GetActiveIdea_WhenNoIdeaActive_ReturnsNull

**Description:**  No Ideas should be returned if there aren't any active
**Related Issues:**  #10
**URL:**  
**Preconditions:** No active ideas exist in the collection

| Step | Action                    | Expected Response |
|:----:| ------------------------- | ----------------- |
|  1   | Call GetActiveDate method | Null is returned                  |

**Post-Conditions:**  Null is returned to the caller when there are no active dates.
**Associated Requirements:** 4

### GetActiveIdea_WhenIdeaIsActive_ReturnsIdea

**Description:**  An Idea should be returned if it is marked active
**Related Issues:**  #10
**URL:**  
**Preconditions:** An active Idea exists in the collection

| Step | Action                    | Expected Response |
|:----:| ------------------------- | ----------------- |
|  1   | Call GetActiveIdea method | An Idea object    | 

**Post-Conditions:**  An Idea is returned to the caller.
**Associated Requirements:** 4

### GetCoupleMostPopularIdea_WhenIdeasCompletionCountTied_ReturnsNull 

**Description:** When two or more ideas have the same completion count, return no Ideas from the method call.
**Related Issues:**  #4
**URL:**  
**Preconditions:** Three ideas with the same completion count exist in the collection

| Step | Action                         | Expected Response |
|:----:| ------------------------------ | ----------------- |
|  1   | Call GetMostPopularIdea method | Null              |

**Post-Conditions:**  Null is retuned to the caller
**Associated Requirements:** 7.3.1

### GetCoupleMostPopularIdea_WhenIdeasHaveCompletionCountLessThanOne_ReturnsNull 

**Description:** When all ideas have a completion count of less than one, return no Ideas from the method call.
**Related Issues:**  #4
**URL:**  
**Preconditions:** Two ideas with a completion count of 0 exist in the collection

| Step | Action                         | Expected Response |
|:----:| ------------------------------ | ----------------- |
|  1   | Call GetMostPopularIdea method | Null              |

**Post-Conditions:**  Null is retuned to the caller
**Associated Requirements:** 7.3.2

### GetCoupleMostPopularIdea_WhenIdeaHasHighestCompletionCount_ReturnsIdea 

**Description:** When one idea has a completion count higher than other ideas in the collection, return it.
**Related Issues:**  #4
**URL:**  
**Preconditions:** Two ideas exist in the collection, and one idea has a higher completion count

| Step | Action                         | Expected Response |
|:----:| ------------------------------ | ----------------- |
|  1   | Call GetMostPopularIdea method | Idea              | 

**Post-Conditions:**  The Idea with the highest completion count is returned to the user
**Associated Requirements:** 7.3

### UpdateUser_WhenUserDoesNotExist_ThrowsArgumentException

**Description:**  A user must exist to be updated.
**Related Issues:**  #11
**URL:**  
**Preconditions:** No users with the user email should exist

| Step | Action                                               | Expected Response       |
|:----:| ---------------------------------------------------- | ----------------------- |
|  1   | Call UpdateUser method with user that does not exist | ThrowsArgumentException | 

**Post-Conditions:**  An ArgumentException is thrown to the caller
**Associated Requirements:** 9

### AddUserToCouple_WhenUserAlreadyInCouple_ThrowsArgumentException

**Description:**  When a user is already in a couple and an attempt is made to add to another couple, then throw an ArgumentException.
**Related Issues:**  #11
**URL:**  
**Preconditions:** A user is already linked to a couple

| Step | Action                                                              | Expected Response |
|:----:| ------------------------------------------------------------------- | ----------------- |
|  1   | Call AddUserToCouple method with a user that is already in a couple | ArgumentException | 

**Post-Conditions:**  An ArgumentException is returned to the caller.
**Associated Requirements:** 9.2

### AddIdea_WhenDuplicateIdeaID_ThrowsArgumentException

**Description:**  When a user tries to add a duplicate idea, (e.g., from the Suggested page), an ArgumentException is thrown.
**Related Issues:**  #17
**URL:**  
**Preconditions:** A user has added an idea from the Suggested page

| Step | Action                                     | Expected Response           |
|:----:| ------------------------------------------ | --------------------------- |
|  1   | Call AddIdea method with duplicate idea ID | ArgumentException is thrown | 

**Post-Conditions:**  An ArgumentException is thrown to the caller.
**Associated Requirements:** 10.3

# Integration Tests

These tests comprise of interactions between the API and a client or the Database and the Infrastructure project.

## DateNight.Api

These tests comprise of sending requests to and getting specific responses from the API.

### WhenAddIdeaMissingData_ReturnsBadRequest

**Description:** If an Idea is submitted with invalid or missing data, the Add Idea endpoint returns a 400 to the caller.  
**Related Issues:** #2  
**URL:**  
**Preconditions:** User is authenticated

| Step | Action                               | Expected Response |
|:----:| ------------------------------------ | ----------------- |
|  1   | Post an Idea message to the endpoint | 400 Bad Request   | 

**Post-Conditions:** A 400 is returned to the client  
**Associated Requirements:** 5.1, 5.2, 5.4

### WhenAcivateNonExistingIdea_ReturnsBadRequest

**Description:** Cannot set an idea active if it does not exist  
**Related Issues:**  
**URL:**  
**Preconditions:** User is authenticated

| Step | Action                                | Expected Response |
|:----:| ------------------------------------- | ----------------- |
|  1   | Post Idea ID to ActivateIdea endpoint | 400 bad request   | 

**Post-Conditions:** A 400 bad request is returned to the client  
**Associated Requirements:** 4.1.1

### WhenDuplicateCreateUserSubmitted_RetunsBadRequest

**Description:**  Duplicated user accounts are not allowed
**Related Issues:** #3  
**URL:**  
**Preconditions:** A user has already been created

| Step | Action                                            | Expected Response |
|:----:| ------------------------------------------------- | ----------------- |
|  1   | Submit a user to the endpoint with the same email | 400 Bad Request                  |

**Post-Conditions:**  A 400 Bad Request is issues to the caller
**Associated Requirements:** 1.2.2

# System Tests

## WhenValidUser_ThenLoginSuccessful

**Description:** Happy path for a user login.  
**Related Issues:** #3 
**URL:**  
**Preconditions:** A user account has already been created with the email and password used in this test.

| Step | Action                        | Expected Response |
|:----:| ----------------------------- | ----------------- |
|  1   | Input user email and password |                   |
|  2   | Click sign in button          |                   |

**Post-Conditions:** User is presented with the home page of the application.  
**Associated Requirements:** 1, 1.2

## WhenInvalidUser_ThenLoginUnsuccessful

**Description:** Ensures invalid users are not logged in.  
**Related Issues:** #3  
**URL:**  
**Preconditions:** None

| Step | Action                        | Expected Response |
|:----:| ----------------------------- | ----------------- |
|  1   | Input user email and password |                   |
|  2   | Click Sign In button          |                   |

**Post-Conditions:** User is presented with an error and remains on the sign in screen.  
**Associated Requirements:** 1, 1.2, 1.2.1

## WhenAccountCreated_ThenUserNameIsStored

**Description:** Verifies that the user's name they entered on the create account screen has been saved by the system.  
**Related Issues:** #3  
**URL:**  
**Preconditions:** An email, password, and name is available to enter in the fields.

| Step | Action                                       | Expected Response                  |
|:----:| -------------------------------------------- | ---------------------------------- |
|  1   | On the sign in page, choose create account   | Presented with create account page |
|  2   | Enter email, password, and name into fields. |                                    |
|  3   | Click Sign Up button.                        | Presented with couple code screen  |
|  4   | Click skip on couple code screen             | Presented with home page           |
|  5   | Click hamburger menu in upper right.         | Available pages are displayed      |
|  6   | Select Account page                          | Presented with Account page        | 

**Post-Conditions:** Name field on account page matches name input on sign up screen.  
**Associated Requirements:** 1.1, 9, 9.4

## WhenNewUserEntersValidCoupleCode_ThenUserIsRegisteredToCouple

**Description:** A new user enters a valid couple code and is added to the couple  
**Related Issues:** #5  
**URL:**  
**Preconditions:** One user account has been created already and the couple code has been recorded for use in this test.

| Step | Action                                                    | Expected Response                            |
|:----:| --------------------------------------------------------- | -------------------------------------------- |
|  1   | Create a new user account                                 | Greeted with couple code page                |
|  2   | Enter other user's couple code into the couple code field |                                              |
|  3   | Press Link Button                                         | User is shown success message and home page. | 

**Post-Conditions:**  
**Associated Requirements:** 2

## WhenUserCreatesIdea_ThenIdeaIsDisplayedOnViewIdeaPage

**Description:** Create an idea on the Create Idea Page and view it on the View Ideas Page.  
**Related Issues:** #7  
**URL:**  
**Preconditions:** A user is logged into the app and is on the Create Idea page

| Step | Action                                         | Expected Response                           |
|:----:| ---------------------------------------------- | ------------------------------------------- |
|  1   | Fill out the title and description for an idea |                                             |
|  2   | Press the Submit button                        | Success message is displayed, form is reset |
|  3   | Navigate to the View Ideas page                |                                             |
|  4   | Find idea that was submitted                   |                                             | 

**Post-Conditions:** The idea that was created on the Create Idea page is displayed on the View Ideas page.  
**Associated Requirements:** 5.3, 8

## WhenUserEditsIdea_ThenUpdatedIdeaIsDisplayedOnViewIdeasPage

**Description:** When a user edits an idea on the View Ideas Page, the updated information is immediately viewable.  
**Related Issues:** #8  
**URL:**  
**Preconditions:** There is at least 1 idea on the View Ideas page

| Step | Action                            | Expected Response                                          |
|:----:| --------------------------------- | ---------------------------------------------------------- |
|  1   | Select the kebab menu on the idea | A dropdown menu is displayed                               |
|  2   | Choose Edit                       | A dialog appears with all fields for an idea               |
|  3   | Change all fields on the form     | The changed data is displayed                              |
|  4   | Press Save button                 | The dialog closes, and user is returned to View Ideas page |

**Post-Conditions:** The idea displayed on the View Ideas page now has the data that the user entered on the edit page.  
**Associated Requirements:** 8, 8.1

## WhenUserDeletesAnIdea_ThenIdeaIsRemovedFromIdeasPage

**Description:** When a user chooses to delete an idea, it is no longer displayed on the View Ideas page.  
**Related Issues:** #9  
**URL:**  
**Preconditions:** There is at least 1 idea displayed on the View Ideas page.

| Step | Action                            | Expected Response   |
|:----:| --------------------------------- | ------------------- |
|  1   | Select the kebab menu on the idea | A menu is displayed |
|  2   | Choose Delete                     | The menu closes     | 

**Post-Conditions:** The deleted idea is no longer displayed on the View Ideas page  
**Associated Requirements:** 8.2

## WhenUserAcceptsIdea_ThenIdeaIsActivated  

**Description:** A user accepts an idea on the Get Idea page and the idea is marked as active  
**Related Issues:** #6  
**URL:**  
**Preconditions:** An idea has been created and added to the collection. User is on the Get Idea page.

| Step | Action                      | Expected Response           |
|:----:| --------------------------- | --------------------------- |
|  1   | Press the accept button     | A confirmation is displayed |
|  2   | Go to the view ideas page   |                             |
|  3   | Search for Idea in the list |                             | 

**Post-Conditions:** The idea is marked with an accepted status  
**Associated Requirements:** 4.1.1

## WhenDuplicateUserCreatesAccount_ThenErrorDisplays

**Description:**  Accounts cannot be linked to more than 1 user email 
**Related Issues:** #3  
**URL:**  
**Preconditions:** A user account has already been created using the email address.

| Step | Action                                                    | Expected Response     |
|:----:| --------------------------------------------------------- | --------------------- |
|  1   | On the Create Account page, fill out required information |                       |
|  2   | Press Submit button                                       | An error is displayed | 

**Post-Conditions:**  User remains on the create account page and a duplicate user email error message is displayed
**Associated Requirements:** 1.2.2

## WhenIdeaSetActive_DisplayOnActiveIdeaPage

**Description:**  A user should see a recently marked Active idea on the Active Idea page
**Related Issues:**  #10
**URL:**  
**Preconditions:** An idea has been added to the collection using the Create Idea page

| Step | Action                           | Expected Response                         |
|:----:| -------------------------------- | ----------------------------------------- |
|  1   | Browse to GetIdea page           | A random idea is displayed                |
|  2   | Press the accept idea button     | A confirmation is displayed               |
|  3   | Navigate to the Active Idea page | The previously accepted idea is displayed | 

**Post-Conditions:**  The user is shown the active idea on the Active Idea page.
**Associated Requirements:** 4

## WhenUserLeavesCouple_ThenUserIsRemovedFromCouple

**Description:**  When a user opts to leave a couple on the Account page, then they are removed and the couple code is now displayed on their profile.
**Related Issues:**  #11
**URL:**  
**Preconditions:** Two users are in a couple

| Step | Action                       | Expected Response              |
|:----:| ---------------------------- | ------------------------------ |
|  1   | User chooses to leave couple | A confirmation dialog is shown |
|  2   | User accepts                 | The Account page is shown      | 

**Post-Conditions:**  The user is no longer in a couple, and the user's own couple code is displayed. The user also has the option to enter another user's couple code
**Associated Requirements:** 9.1, 9.2, 9.3

## WhenUserAddsPopularIdea_ThenIdeaAddedToPool

**Description:**  When a user chooses to add an idea from the Suggested Ideas page, then the idea is added to their pool of ideas.
**Related Issues:**  #17
**URL:**  
**Preconditions:** The user is logged in.

| Step | Action                                      | Expected Response                                 |
|:----:| ------------------------------------------- | ------------------------------------------------- |
|  1   | Navigate to the Suggested Ideas page          | The top 10 most popular ideas are displayed       |
|  2   | Choose "+" button to add idea to collection | The + changes to a checkmark                      |
|  3   | Navigate to the View Ideas Page             | All of the user's ideas in the pool are displayed |
|  4   | Find added idea                             |                                                   |

**Post-Conditions:**  The added idea is now in the user's idea pool
**Associated Requirements:** 10.3

## WhenCoupleHasIdeaInSuggestedCollection_ThenDoNotDisplayThem

**Description:**  When a couple's idea is the suggested collection, do not display them in the list of ideas shown to the user.
**Related Issues:**  #17
**URL:**  
**Preconditions:** User is logged in. User has submitted ideas and completed ideas. Other users have added and completed the user's ideas.

| Step | Action                                            | Expected Response                      |
|:----:| ------------------------------------------------- | -------------------------------------- |
|  1   | Navigate to Suggested page                        | A list of suggested ideas is displayed |
|  2   | Verify no ideas created by the user exist in list | None exist                             | 

**Post-Conditions:**  There are no ideas created by the current user in the suggested list
**Associated Requirements:** 10.1

## WhenUserAppliesFormattingToDescription_ThenFormattingIsDisplayed

**Description:**  When the user chooses to apply formatting to an idea's description, then when viewing that same idea, the formatting is visible to the user.
**Related Issues:**  #1
**URL:**  
**Preconditions:** The user is logged in and on the Create Idea page.

| Step | Action                                    | Expected Response        |
|:----:| ----------------------------------------- | ------------------------ |
|  1   | Bold text in description                  |                          |
|  2   | Choose Submit button                      | A success dialog appears |
|  3   | Navigate to View Ideas page               | Ideas are displayed      |
|  4   | Verify description of idea has formatting |                          |

**Post-Conditions:**  The idea with a formatted description is displayed to the user
**Associated Requirements:** 6
