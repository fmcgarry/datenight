# Overview

A comprehensive suite of testing scenarios.

# Unit Tests

The tests defined in this section are separated by their respective projects.

## DateNightApp

Since this is the UI project, these tests comprise of safe guards for user input and error checking user data. They do not follow the naming format like the rests of do because they are UI tests.

### Join Couple Dialog: Couple Code input is more than 8 characters

**Description:** User inputs more that 8 characters in the couple code field after creating their account.  
**Related Issues:** #5  
**Preconditions:** Filled out information on create account page and pressed Sign Up button. User is on the enter couple code screen.

| Step | Action                       | Expected Response     |
| :--: | ---------------------------- | --------------------- |
|  1   | Enter more than 8 characters | An error is displayed |

**Post-Conditions:** An error is displayed to the user and the user remains on the couple code screen.  
**Associated Requirements:** 2.1.1

### Join Couple Dialog: Couple Code is user's own code

**Description:** User input's their own code into the couple code field after creating their account.  
**Related Issues:** #5  
**Preconditions:** Filled out information on create account page and pressed Sign Up button. User is on the enter couple code screen.

| Step | Action                                                           | Expected Response     |
| :--: | ---------------------------------------------------------------- | --------------------- |
|  1   | Enter the same code that is presented into the couple code field | An error is displayed |

**Post-Conditions:** An error is displayed to the user and the user remains on the couple code screen.  
**Associated Requirements:** 2.2.1

### Create Idea Page: Required Title field

**Description:** Ensure that the user has filled out the Title field on the Create Idea page on submit.  
**Related Issues:** #2  
**Preconditions:** The user is on the Create Idea page

| Step | Action                                                    | Expected Response |
| :--: | --------------------------------------------------------- | ----------------- |
|  1   | Fill out description field, leaving the title field empty |                   |
|  2   | Press submit button                                       |                   |

**Post-Conditions:** The idea is not submitted, and error is displayed, and the user stays on the Create Idea page.  
**Associated Requirements:** 5.3.1

### Edit Idea Dialog: All Required Fields Have Data

**Description:** When a user is editing ideas make sure the Title field has data.  
**Related Issues:** [#8](https://github.com/fmcgarrydev/datenight/issues/8)  
**Preconditions:** The user is editing an idea.

| Step | Action                | Expected Response     |
| :--: | --------------------- | --------------------- |
|  1   | Delete all field data |                       |
|  2   | Press the Save button | An error is displayed |

**Post-Conditions:** The user stays on the Edit dialog and has the option to either fix the invalid data or quit without saving.  
**Associated Requirements:** 8.1

### Edit Idea Dialog: Created On Date Is Not In The Future

**Description:** When editing an idea, make sure the user doesn't set the Created On date in the future.  
**Related Issues:** [#8](https://github.com/fmcgarrydev/datenight/issues/8)  
**Preconditions:** A user is editing an idea.

| Step | Action                                          | Expected Response     |
| :--: | ----------------------------------------------- | --------------------- |
|  1   | Set the Created On date to a date in the future | An error is displayed |

**Post-Conditions:** The user stays on the edit page and has the option to fix their error or close without saving.  
**Associated Requirements:** 8.1

## DateNight.Core

This project contains most of the core business logic of the application, therefore, has the most unit tests.

### AddIdeaAsync_WhenNewIdea_ThenCreatedOnDateIsToday

**Description:** When a new idea is created, then the CreatedOn field is set to the current date.
**Related Issues:** #2
**Preconditions:** None

| Step | Action                                                | Expected Response |
| :--: | ----------------------------------------------------- | ----------------- |
|  1   | Call the AddIdeaAsync method with a valid Idea object | Success           |
|  2   | Verify CreatedOn date is today                        | Success           |

**Post-Conditions:** The idea created has a CreatedOn date of today
**Associated Requirements:** 5.4

### UpdateIdea_WhenIdeaDoesNotExist_ThrowsArgumentException

**Description:** When an idea is submitted to the UpdateIdea method, ensure it exists first before trying to update it  
**Related Issues:** [#8](https://github.com/fmcgarrydev/datenight/issues/8)  
**Preconditions:** None

| Step | Action                                                             | Expected Response           |
| :--: | ------------------------------------------------------------------ | --------------------------- |
|  1   | Call the UpdateIdea method with an idea that hasn't been added yet | ArgumentException is thrown |

**Post-Conditions:** The UpdateIdea method has thrown an ArgumentException  
**Associated Requirements:** 8.1

### UpdateIdea_WhenIdeaIdIsNull_ThrowsArgumentNullException

**Description:** When an idea is submitted to the UpdateIdea method, ensure the Id property is not null.  
**Related Issues:** [#8](https://github.com/fmcgarrydev/datenight/issues/8)  
**Preconditions:** None

| Step | Action                                                         | Expected Response               |
|:----:| -------------------------------------------------------------- | ------------------------------- |
|  1   | Call the UpdateIdea method with an idea that has an Id of null | ArgumentNullException is thrown |

**Post-Conditions:** The UpdateIdea method has thrown an ArgumentNullException.  
**Associated Requirements:** 8.1

### UpdateIdea_WhenValidIdea_ThenRepositoryUpdateIdeaIsCalled

**Description:** When a valid idea is submitted to the UpdateIdea method, ensure the repository UpdateAsync() method is called.  
**Related Issues:** [#8](https://github.com/fmcgarrydev/datenight/issues/8)  
**Preconditions:** None

| Step | Action                                                | Expected Response                         |
|:----:| ----------------------------------------------------- | ----------------------------------------- |
|  1   | Call the UpdateIdea method with an idea that is valid | Repository UpdateAsync() method is called |

**Post-Conditions:** None.  
**Associated Requirements:** 8.1

### DeleteIdea_WhenIdeaDoesNotExist_ThrowsArgumentException

**Description:** When an idea ID is submitted for deletion, ensure the idea exists.  
**Related Issues:** #9  
**Preconditions:** None

| Step | Action                                                   | Expected Response           |
| :--: | -------------------------------------------------------- | --------------------------- |
|  1   | Call the DeleteIdea method with an ID that doesn't exist | ArgumentException is thrown |

**Post-Conditions:** The DeleteIdea method has thrown an ArgumentException  
**Associated Requirements:** 8.2

### DeleteIdea_WhenIdeaExists_ThenIdeaRemovedFromCollection

**Description:** When an idea is requested to be deleted and it exists in the collection, remove it from the collection.
**Related Issues:** #9  
**Preconditions:** An idea exists in the collection

| Step | Action                     | Expected Response |
| :--: | -------------------------- | ----------------- |
|  1   | Call the DeleteIdea method | Success           |

**Post-Conditions:** The caller is returned a success.  
**Associated Requirements:** 8.2

### GetAllIdeasAsync_WhenCalled_ReturnsOnlyUserIdeas

**Description:** When calling the GetIdeas method, only the ideas created by the current user are returned.  
**Related Issues:** #7  
**Preconditions:** Ideas have been added via the AddIdeas method. User has been created and ID is known.

| Step | Action                                    | Expected Response           |
| :--: | ----------------------------------------- | --------------------------- |
|  1   | Call GetAllIdeasAsync method with user id | A list of ideas is returned |

**Post-Conditions:** A list of ONLY the user's ideas has been returned.  
**Associated Requirements:** 8, 1

### GetAllIdeasAsync_WhenNoIdeasInCollection_ReturnsEmpty

**Description:** When there are not ideas in the collection for a user, then return an empty collection.  
**Related Issues:** #7  
**Preconditions:** None

| Step | Action                       | Expected Response |
| :--: | ---------------------------- | ----------------- |
|  1   | Call GetAllIdeasAsync method | Empty collection  |

**Post-Conditions:** The caller is returned an empty collection.  
**Associated Requirements:** 8

### ActivateIdea_WhenNewIdeaActivated_ThenCurrentActiveIdeaDeactivated

**Description:** Multiple active Ideas are not allowed.  
**Related Issues:** [#6](https://github.com/fmcgarrydev/datenight/issues/6)  
**Preconditions:** An idea is already marked as active and a new Idea ID is known

| Step | Action                                                 | Expected Response       |
| :--: | ------------------------------------------------------ | ----------------------- |
|  1   | Submit an Idea ID to the ActivateIdea method           | Success                 |
|  2   | Query all Ideas using GetIdeas method for active state | Only 1 Idea is returned |

**Post-Conditions:** None  
**Associated Requirements:** 4.1.1.1

### ActivateIdea_WhenIdeaDoesNotExist_ThrowsArgumentOutOfRangeException

**Description:** Activation requests for ideas that haven't been created yet should fail.  
**Related Issues:** [#6](https://github.com/fmcgarrydev/datenight/issues/6)  
**Preconditions:** A random Idea ID is known

| Step | Action                                | Expected Response                        |
| :--: | ------------------------------------- | ---------------------------------------- |
|  1   | Submit Idea ID to ActivateIdea method | An ArgumentOutOfRangeException is thrown |

**Post-Conditions:** No ideas have been marked active  
**Associated Requirements:** 4.1.1

### ActivateIdea_WhenIdIsNull_ThrowsArgumentNullException

**Description:** Activation requests for ideas that haven't been created yet should fail.  
**Related Issues:** [#6](https://github.com/fmcgarrydev/datenight/issues/6)  
**Preconditions:** None

| Step | Action                               | Expected Response                        |
| :--: | ------------------------------------ | ---------------------------------------- |
|  1   | Submit a null to ActivateIdea method | An ArgumentNullException is thrown |

**Post-Conditions:** No ideas have been marked active  
**Associated Requirements:** 4.1.1

### GetActiveIdea_WhenNoIdeaActive_ReturnsNull

**Description:** No Ideas should be returned if there aren't any active  
**Related Issues:** #10  
**Preconditions:** No active ideas exist in the collection

| Step | Action                    | Expected Response |
| :--: | ------------------------- | ----------------- |
|  1   | Call GetActiveDate method | Null is returned  |

**Post-Conditions:** Null is returned to the caller when there are no active dates.  
**Associated Requirements:** 4

### GetActiveIdea_WhenIdeaIsActive_ReturnsIdea

**Description:** An Idea should be returned if it is marked active  
**Related Issues:** #10  
**Preconditions:** An active Idea exists in the collection

| Step | Action                    | Expected Response |
| :--: | ------------------------- | ----------------- |
|  1   | Call GetActiveIdea method | An Idea object    |

**Post-Conditions:** An Idea is returned to the caller.  
**Associated Requirements:** 4

### GetCoupleMostPopularIdea_WhenIdeasCompletionCountTied_ReturnsNull

**Description:** When two or more ideas have the same completion count, return no Ideas from the method call.  
**Related Issues:** #4  
**Preconditions:** Three ideas with the same completion count exist in the collection

| Step | Action                         | Expected Response |
| :--: | ------------------------------ | ----------------- |
|  1   | Call GetMostPopularIdea method | Null              |

**Post-Conditions:** Null is returned to the caller  
**Associated Requirements:** 7.3.1

### GetCoupleMostPopularIdea_WhenIdeasHaveCompletionCountLessThanOne_ReturnsNull

**Description:** When all ideas have a completion count of less than one, return no Ideas from the method call.  
**Related Issues:** #4  
**Preconditions:** Two ideas with a completion count of 0 exist in the collection

| Step | Action                         | Expected Response |
| :--: | ------------------------------ | ----------------- |
|  1   | Call GetMostPopularIdea method | Null              |

**Post-Conditions:** Null is returned to the caller  
**Associated Requirements:** 7.3.2

### GetCoupleMostPopularIdea_WhenIdeaHasHighestCompletionCount_ReturnsIdea

**Description:** When one idea has a completion count higher than other ideas in the collection, return it.  
**Related Issues:** #4  
**Preconditions:** Two ideas exist in the collection, and one idea has a higher completion count

| Step | Action                         | Expected Response |
| :--: | ------------------------------ | ----------------- |
|  1   | Call GetMostPopularIdea method | Idea              |

**Post-Conditions:** The Idea with the highest completion count is returned to the user  
**Associated Requirements:** 7.3

### UpdateUser_WhenUserDoesNotExist_ThrowsUserDoesNotExistException

**Description:** A user must exist to be updated.  
**Related Issues:** #11  
**Preconditions:** No users with the user email should exist

| Step | Action                                               | Expected Response       |
| :--: | ---------------------------------------------------- | ----------------------- |
|  1   | Call UpdateUser method with user that does not exist | UserDoesNotExistException |

**Post-Conditions:** An UserDoesNotExistException is thrown to the caller  
**Associated Requirements:** 9

### AddIdea_WhenDuplicateIdeaID_ThrowsArgumentException

**Description:** When a user tries to add a duplicate idea, (e.g., from the Suggested page), an ArgumentException is thrown.  
**Related Issues:** #17  
**Preconditions:** A user has added an idea from the Suggested page

| Step | Action                                     | Expected Response           |
| :--: | ------------------------------------------ | --------------------------- |
|  1   | Call AddIdea method with duplicate idea ID | ArgumentException is thrown |

**Post-Conditions:** An ArgumentException is thrown to the caller.  
**Associated Requirements:** 10.3

# Integration Tests

These tests comprise of interactions between the API and a client or the Database and the Infrastructure project.

## DateNight.Api

These tests comprise of sending requests to and getting specific responses from the API.

### WhenAddIdeaMissingData_ReturnsBadRequest

**Description:** If an Idea is submitted with invalid or missing data, the Add Idea endpoint returns a 400 to the caller.  
**Related Issues:** #2  
**Preconditions:** User is authenticated

| Step | Action                               | Expected Response |
| :--: | ------------------------------------ | ----------------- |
|  1   | Post an Idea message to the endpoint | 400 Bad Request   |

**Post-Conditions:** A 400 is returned to the client  
**Associated Requirements:** 5.1, 5.2, 5.4

### WhenActivateNonExistingIdea_ReturnsBadRequest

**Description:** Cannot set an idea active if it does not exist  
**Related Issues:**  
**Preconditions:** User is authenticated

| Step | Action                                | Expected Response |
| :--: | ------------------------------------- | ----------------- |
|  1   | Post Idea ID to ActivateIdea endpoint | 400 bad request   |

**Post-Conditions:** A 400 bad request is returned to the client  
**Associated Requirements:** 4.1.1

### WhenDuplicateCreateUserSubmitted_ReturnsBadRequest

**Description:** Duplicated user accounts are not allowed  
**Related Issues:** #3  
**Preconditions:** A user has already been created

| Step | Action                                            | Expected Response |
| :--: | ------------------------------------------------- | ----------------- |
|  1   | Submit a user to the endpoint with the same email | 400 Bad Request   |

**Post-Conditions:** A 400 Bad Request is issues to the caller  
**Associated Requirements:** 1.2.2

### GetIdeas_WhenIdeasFound_ReturnsOk

**Description:** A 200 OK should returned when the GetIdeas endpoint is called successfully.  
**Related Issues:** #7  
**Preconditions:** Ideas have already been added.

| Step | Action                                     | Expected Response |
| :--: | ------------------------------------------ | ----------------- |
|  1   | Submit a GET request to the ideas endpoint | 200 OK            |

**Post-Conditions:** A 200 OK is issued to the caller.  
**Associated Requirements:** 8

### GetIdeas_WhenIdeasFound_ReturnsIdeas

**Description:** All ideas should be returned when the GetIdeas endpoint is called successfully.  
**Related Issues:** #7  
**Preconditions:** Ideas have already been added.

| Step | Action                                     | Expected Response     |
| :--: | ------------------------------------------ | --------------------- |
|  1   | Submit a GET request to the ideas endpoint | A collection of ideas |

**Post-Conditions:** A collection of ideas are returned to the caller.  
**Associated Requirements:** 8

# System Tests

## WhenValidUser_ThenLoginSuccessful

**Description:** Happy path for a user login.  
**Related Issues:** #3  
**Preconditions:** A user account has already been created with the email and password used in this test.

| Step | Action                        | Expected Response |
| :--: | ----------------------------- | ----------------- |
|  1   | Input user email and password |                   |
|  2   | Click sign in button          |                   |

**Post-Conditions:** User is presented with the home page of the application.  
**Associated Requirements:** 1, 1.2

## WhenInvalidUser_ThenLoginUnsuccessful

**Description:** Ensures invalid users are not logged in.  
**Related Issues:** #3  
**Preconditions:** None

| Step | Action                        | Expected Response |
| :--: | ----------------------------- | ----------------- |
|  1   | Input user email and password |                   |
|  2   | Click Sign In button          |                   |

**Post-Conditions:** User is presented with an error and remains on the sign in screen.  
**Associated Requirements:** 1, 1.2, 1.2.1

## WhenAccountCreated_ThenUserNameIsStored

**Description:** Verifies that the user's name they entered on the create account screen has been saved by the system.  
**Related Issues:** #3  
**Preconditions:** An email, password, and name is available to enter in the fields.

| Step | Action                                       | Expected Response                  |
| :--: | -------------------------------------------- | ---------------------------------- |
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
**Preconditions:** One user account has been created already and the couple code has been recorded for use in this test.

| Step | Action                                                    | Expected Response                            |
| :--: | --------------------------------------------------------- | -------------------------------------------- |
|  1   | Create a new user account                                 | Greeted with couple code page                |
|  2   | Enter other user's couple code into the couple code field |                                              |
|  3   | Press Link Button                                         | User is shown success message and home page. |

**Post-Conditions:**  
**Associated Requirements:** 2

## WhenUserCreatesIdea_ThenIdeaIsDisplayedOnViewIdeaPage

**Description:** Create an idea on the Create Idea Page and view it on the View Ideas Page.  
**Related Issues:** #7  
**Preconditions:** A user is logged into the app and is on the Create Idea page

| Step | Action                                         | Expected Response                           |
| :--: | ---------------------------------------------- | ------------------------------------------- |
|  1   | Fill out the title and description for an idea |                                             |
|  2   | Press the Submit button                        | Success message is displayed, form is reset |
|  3   | Navigate to the View Ideas page                |                                             |
|  4   | Find idea that was submitted                   |                                             |

**Post-Conditions:** The idea that was created on the Create Idea page is displayed on the View Ideas page.  
**Associated Requirements:** 5.3, 8

## WhenUserEditsIdea_ThenUpdatedIdeaIsDisplayedOnViewIdeasPage

**Description:** When a user edits an idea on the View Ideas Page, the updated information is immediately viewable.  
**Related Issues:** [#8](https://github.com/fmcgarrydev/datenight/issues/8)  
**Preconditions:** There is at least 1 idea on the View Ideas page

| Step | Action                            | Expected Response                                          |
| :--: | --------------------------------- | ---------------------------------------------------------- |
|  1   | Select the kebab menu on the idea | A dropdown menu is displayed                               |
|  2   | Choose Edit                       | A dialog appears with all fields for an idea               |
|  3   | Change all fields on the form     | The changed data is displayed                              |
|  4   | Press Save button                 | The dialog closes, and user is returned to View Ideas page |

**Post-Conditions:** The idea displayed on the View Ideas page now has the data that the user entered on the edit page.  
**Associated Requirements:** 8, 8.1

## WhenUserDeletesAnIdea_ThenIdeaIsRemovedFromIdeasPage

**Description:** When a user chooses to delete an idea, it is no longer displayed on the View Ideas page.  
**Related Issues:** #9  
**Preconditions:** There is at least 1 idea displayed on the View Ideas page.

| Step | Action                            | Expected Response   |
| :--: | --------------------------------- | ------------------- |
|  1   | Select the kebab menu on the idea | A menu is displayed |
|  2   | Choose Delete                     | The menu closes     |

**Post-Conditions:** The deleted idea is no longer displayed on the View Ideas page  
**Associated Requirements:** 8.2

## WhenUserAcceptsIdea_ThenIdeaIsActivated

**Description:** A user accepts an idea on the Get Idea page and the idea is marked as active  
**Related Issues:** [#6](https://github.com/fmcgarrydev/datenight/issues/6)  
**Preconditions:** An idea has been created and added to the collection. User is on the Get Idea page.

| Step | Action                      | Expected Response           |
| :--: | --------------------------- | --------------------------- |
|  1   | Press the accept button     | A confirmation is displayed |
|  2   | Go to the view ideas page   |                             |
|  3   | Search for Idea in the list |                             |

**Post-Conditions:** The idea is marked with an accepted status  
**Associated Requirements:** 4.1.1

## WhenDuplicateUserCreatesAccount_ThenErrorDisplays

**Description:** Accounts cannot be linked to more than 1 user email  
**Related Issues:** #3  
**Preconditions:** A user account has already been created using the email address.

| Step | Action                                                    | Expected Response     |
| :--: | --------------------------------------------------------- | --------------------- |
|  1   | On the Create Account page, fill out required information |                       |
|  2   | Press Submit button                                       | An error is displayed |

**Post-Conditions:** User remains on the create account page and a duplicate user email error message is displayed  
**Associated Requirements:** 1.2.2

## WhenIdeaSetActive_DisplayOnActiveIdeaPage

**Description:** A user should see a recently marked Active idea on the Active Idea page  
**Related Issues:** #10  
**Preconditions:** An idea has been added to the collection using the Create Idea page

| Step | Action                           | Expected Response                         |
| :--: | -------------------------------- | ----------------------------------------- |
|  1   | Browse to GetIdea page           | A random idea is displayed                |
|  2   | Press the accept idea button     | A confirmation is displayed               |
|  3   | Navigate to the Active Idea page | The previously accepted idea is displayed |

**Post-Conditions:** The user is shown the active idea on the Active Idea page.  
**Associated Requirements:** 4

## WhenUserLeavesCouple_ThenUserIsRemovedFromCouple

**Description:** When a user opts to leave a couple on the Account page, then they are removed and the couple code is now displayed on their profile.  
**Related Issues:** #11  
**Preconditions:** Two users are in a couple

| Step | Action                       | Expected Response              |
| :--: | ---------------------------- | ------------------------------ |
|  1   | User chooses to leave couple | A confirmation dialog is shown |
|  2   | User accepts                 | The Account page is shown      |

**Post-Conditions:** The user is no longer in a couple, and the user's own couple code is displayed. The user also has the option to enter another user's couple code  
**Associated Requirements:** 9.1, 9.2, 9.3

## WhenUserAddsPopularIdea_ThenIdeaAddedToPool

**Description:** When a user chooses to add an idea from the Suggested Ideas page, then the idea is added to their pool of ideas.  
**Related Issues:** #17  
**Preconditions:** The user is logged in.

| Step | Action                                      | Expected Response                                 |
| :--: | ------------------------------------------- | ------------------------------------------------- |
|  1   | Navigate to the Suggested Ideas page        | The top 10 most popular ideas are displayed       |
|  2   | Choose "+" button to add idea to collection | The + changes to a checkmark                      |
|  3   | Navigate to the View Ideas Page             | All of the user's ideas in the pool are displayed |
|  4   | Find added idea                             |                                                   |

**Post-Conditions:** The added idea is now in the user's idea pool  
**Associated Requirements:** 10.3

## WhenCoupleHasIdeaInSuggestedCollection_ThenDoNotDisplayThem

**Description:** When a couple's idea is the suggested collection, do not display them in the list of ideas shown to the user.  
**Related Issues:** #17  
**Preconditions:** User is logged in. User has submitted ideas and completed ideas. Other users have added and completed the user's ideas.

| Step | Action                                            | Expected Response                      |
| :--: | ------------------------------------------------- | -------------------------------------- |
|  1   | Navigate to Suggested page                        | A list of suggested ideas is displayed |
|  2   | Verify no ideas created by the user exist in list | None exist                             |

**Post-Conditions:** There are no ideas created by the current user in the suggested list  
**Associated Requirements:** 10.1

## WhenUserAppliesFormattingToDescription_ThenFormattingIsDisplayed

**Description:** When the user chooses to apply formatting to an idea's description, then when viewing that same idea, the formatting is visible to the user.  
**Related Issues:** #1  
**Preconditions:** The user is logged in and on the Create Idea page.

| Step | Action                                    | Expected Response        |
| :--: | ----------------------------------------- | ------------------------ |
|  1   | Bold text in description                  |                          |
|  2   | Choose Submit button                      | A success dialog appears |
|  3   | Navigate to View Ideas page               | Ideas are displayed      |
|  4   | Verify description of idea has formatting |                          |

**Post-Conditions:** The idea with a formatted description is displayed to the user  
**Associated Requirements:** 6

# User Acceptance Tests

## User is able to edit an idea

**Description:** A user is able to edit all fields of an idea that was previously created.  
**Related Issues:** [#8](https://github.com/fmcgarrydev/datenight/issues/8)    
**Preconditions:** An idea has already been created by the user.

| Step | Action                          | Expected Response           |
| :--: | ------------------------------- | --------------------------- |
|  1   | Navigate to the View Ideas page |                             |
|  2   | Choose menu button on an idea   | A dropdown menu appears     |
|  3   | Choose Edit                     | A model edit dialog appears |
|  4   | Update each field with new data |                             |
|  5   | Choose Save button              | Model is closed             |

**Post-Conditions:** The idea displayed on the page has been updated with new values.  
**Associated Requirements:** 8.1

## User is able to delete an idea

**Description:** When a user deletes an idea, it is removed from the database.  
**Related Issues:** [#9](https://github.com/fmcgarrydev/datenight/issues/9)   
**Preconditions:** An idea has already been created by the user.

| Step | Action                          | Expected Response       |
| :--: | ------------------------------- | ----------------------- |
|  1   | Navigate to the View Ideas page |                         |
|  2   | Choose menu button on an idea   | A dropdown menu appears |
|  3   | Choose Delete                   | The dropdown closes     |

**Post-Conditions:** The idea displayed on the page is no longer there.  
**Associated Requirements:** 8.2

## A random idea is shown to the user on the Get Idea page

**Description:** When first navigating to the Get Idea page, a random idea is displayed to the user.  
**Related Issues:** [#6](https://github.com/fmcgarrydev/datenight/issues/6)  
**Preconditions:** More than 2 ideas has already been created by the user.

| Step | Action                         | Expected Response |
| :--: | ------------------------------ | ----------------- |
|  1   | Navigate to the Get Ideas page |                   |

**Post-Conditions:** An idea is shown to the user.  
**Associated Requirements:** 3

## User can accept an idea on the Get Idea page

**Description:** When on the Get Idea page, a user can accept the random idea shown.  
**Related Issues:** [#6](https://github.com/fmcgarrydev/datenight/issues/6)  
**Preconditions:** More than 1 idea has been previously created by the user.

| Step | Action                               | Expected Response          |
| :--: | ------------------------------------ | -------------------------- |
|  1   | Navigate to the Get Ideas page       |                            |
|  2   | Choose the accept button on the idea | Routed to Active Idea page |

**Post-Conditions:** The accepted idea is now displayed on the Active Idea page.  
**Associated Requirements:** 3.1

## User can decline an idea on the Get Idea page

**Description:** When on the Get Idea page, a user can decline the random idea.  
**Related Issues:** [#6](https://github.com/fmcgarrydev/datenight/issues/6)  
**Preconditions:** More than 1 idea has been previously created by the user.

| Step | Action                                | Expected Response       |
| :--: | ------------------------------------- | ----------------------- |
|  1   | Navigate to the Get Ideas page        |                         |
|  2   | Choose the decline button on the idea | a new idea is displayed |

**Post-Conditions:** The idea displayed is now a new idea.  
**Associated Requirements:** 3.2

## User can filter out completed ideas on the Get Idea page

**Description:** When on the Get Idea page, a user can choose to filter out ideas that have already been completed.  
**Related Issues:** [#6](https://github.com/fmcgarrydev/datenight/issues/6)  
**Preconditions:** More than 1 idea has been previously created by the user.

| Step | Action                         | Expected Response       |
| :--: | ------------------------------ | ----------------------- |
|  1   | Navigate to the Get Ideas page |                         |
|  2   | Choose filter dropdown         | A dropdown menu appears |
|  3   | Choose "Not Completed"         | The dropdown closes     |
|  4   | Decline idea                   | A new idea is shown     |
|  5   | Repeat step 4 as needed        |                         |

**Post-Conditions:** The ideas shown are ideas that have not been completed.  
**Associated Requirements:** 3.3

## User can apply formatting to Idea description

**Description:** When creating/editing an idea, formatting may be applied to the text.  
**Related Issues:** [#1](https://github.com/fmcgarrydev/datenight/issues/1)  
**Preconditions:** None

| Step | Action                           | Expected Response                            |
| :--: | -------------------------------- | -------------------------------------------- |
|  1   | Navigate to the Create Idea page |                                              |
|  2   | Select description textbox       | A format bar appears above the textbox       |
|  3   | Choose a text format             | The format activates                         |
|  4   | Write text                       | The text is formatted with the chosen format |
|  5   | Repeat steps 3-4 for all formats |                                              |
|  6   | Choose Save                      | Empty fields are shown                       |
|  7   | Navigate to View Ideas page      |                                              |

**Post-Conditions:** Idea with formatted description is shown  
**Associated Requirements:** 6

## User can view active Idea information

**Description:** A user can view information about an idea they have marked as active.  
**Related Issues:** [#10](https://github.com/fmcgarrydev/datenight/issues/10)  
**Preconditions:** A user has set an idea as active  

| Step | Action                           | Expected Response                                                              |
| :--: | -------------------------------- | ------------------------------------------------------------------------------ |
|  1   | Navigate to the Active Idea page | The title, description, name of the user, and last completed date is displayed |

**Post-Conditions:** None  
**Associated Requirements:** 4, 4.1, 4.2, 4.3, 4.4  

## User can complete an active Idea

**Description:** A user can mark an active idea as completed  
**Related Issues:** [#10](https://github.com/fmcgarrydev/datenight/issues/10)  
**Preconditions:** A user has set an idea as active  

| Step | Action                           | Expected Response                                                              |
| :--: | -------------------------------- | ------------------------------------------------------------------------------ |
|  1   | Navigate to the Active Idea page | The title, description, name of the user, and last completed date is displayed |
|  2   | Choose the Complete button       | A confirmation is shown.                                                       |
|  3   | Accept the confirmation          | The active idea is no longer shown.                                            |

**Post-Conditions:** The idea now has a last-completed date of today.  
**Associated Requirements:** 4, 4.5  

## User can abandon an active Idea

**Description:** A user can abandon an active idea.  
**Related Issues:** [#10](https://github.com/fmcgarrydev/datenight/issues/10)  
**Preconditions:** A user has set an idea as active  

| Step | Action                           | Expected Response                                                              |
| :--: | -------------------------------- | ------------------------------------------------------------------------------ |
|  1   | Navigate to the Active Idea page | The title, description, name of the user, and last completed date is displayed |
|  2   | Choose the Abandon button        | A confirmation is shown.                                                       |
|  3   | Accept the confirmation          | The active idea is no longer shown.                                            |

**Post-Conditions:** None  
**Associated Requirements:** 4, 4.6  

## User can filter out completed ideas

**Description:** A user can choose to filter out completed ideas from the Random Idea page  
**Related Issues:** [#42](https://github.com/fmcgarrydev/datenight/issues/42)  
**Preconditions:** A user has > 2 ideas, and 1 idea is marked as completed  

| Step | Action                                | Expected Response              |
| :--: | ------------------------------------- | ------------------------------ |
|  1   | Navigate to the Random Idea page      | A random idea is displayed     |
|  2   | Choose the option to filter out ideas |                                |
|  3   | Choose reject idea button             | A new random idea is displayed |
|  4   | Repeat step 3 as necessary            |                                |        

**Post-Conditions:** No ideas that are completed are shown  
**Associated Requirements:** 3.3  

## New user can create an account

**Description:** A new user can create an account  
**Related Issues:** [#3](https://github.com/fmcgarrydev/datenight/issues/3)  
**Preconditions:** None  

| Step | Action           | Expected Response                                         |
| :--: | ---------------- | --------------------------------------------------------- |
|  1   | Open the app     | A login form is displayed                                 |
|  2   | Choose "Sign Up" | A form is displayed with email, password, and name fields |

**Post-Conditions:** No ideas that are completed are shown  
**Associated Requirements:** 1, 1.1  

## Existing user can log in

**Description:** An existing user can log into the app  
**Related Issues:** [#3](https://github.com/fmcgarrydev/datenight/issues/3)  
**Preconditions:** None  

| Step | Action                  | Expected Response                       |
| :--: | ----------------------- | --------------------------------------- |
|  1   | Open the app            | A login form is displayed               |
|  2   | Enter email an password |                                         |
|  3   | Choose "Sign In"        | User is presented with Active Idea page |

**Post-Conditions:** The user is logged in  
**Associated Requirements:** 1, 1.2  
