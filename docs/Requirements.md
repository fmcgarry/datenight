# Functional Requirements

```
1. System shall have individual user accounts.
	1.1. System shall store the name of the account holder.
	1.2. System shall use email and password to authenticate a user account login.
        	1.2.1. System shall not allow unauthorized access.
        	1.2.2. System shall not allow duplicated email address.

2. System shall allow users to register as a couple.
    	2.1. System shall generate a unique code for the user on account creation.
        	2.1.1. System shall use 8 alpha-numeric characters in the unique code.
    	2.2. System shall accept a generated code on account creation so a new account can be linked to an existing account.
        	2.2.1. System shall not accept the user's own generated code.
    	2.3. System shall display a page after user account creation to allow user to enter a couple code.  
	    	2.3.1. System shall allow user to input a couple code.  
		    	2.3.1.1. System shall not allow more that 8 characters in the couple code field.  
	    	2.3.2. System shall allow user to skip page.

3. System shall display a date idea to the user chosen at random from the couple's pool of ideas.
	3.1. System shall allow the user to accept the date idea.
        	3.1.1. System shall set the accepted idea as the active date.
            		3.1.1.1. System shall only have 1 active date at a time.
    	3.2. System shall allow the user to deny the date idea.
        	3.2.1. System shall display a new random idea.
        	3.2.2. System shall not display the same idea again to the user.
    	3.3. System shall allow the user to filter date ideas provided by their completion status.

4. System shall include an "Active Date" page to the user that displays data from the couple's active date.
	4.1. System shall display the title of the active date idea.
    	4.2. System shall display the description of the active date idea.
    	4.3. System shall display the name of the user that created the active date idea.
    	4.4. System shall display the date of which the active idea was last completed.
    	4.5. System shall allow a user to mark the date idea as completed.
        	4.5.1. System shall store the last completed date of the date idea when the active date is marked completed by a user.
    	4.6. System shall allow a user in the couple to set the active idea to inactive.

5. System shall display a "Create" page to the user that allows a user to create new date ideas.
    	5.1. System shall provide a text input field for the date idea title.
    	5.2. System shall provide a text input field for the date idea description.
    	5.3. System shall provide a "Submit" button that saves the date idea to the database.
        	5.3.1. System shall not submit the idea if the title field is empty.
    	5.4. System shall save the date the idea was created on.

6. System shall provide a rich text box experience for the idea description.
    	6.1. System shall allow text in the idea description to be bold.
    	6.2. System shall allow text in the idea description to be italicized.
    	6.3. System shall allow text in the idea description to be underlined.
    	6.4. System shall allow text in the idea description using a numberedlist format.
    	6.5. System shall allow text in the idea description using a bulletedlist format.
    	6.6. System shall allow text in the idea description to be indented.

7. System shall display a "Couple" page to the user that displays a couple's statistics.
    	7.1. System shall display the total number of activatable date ideas submitted by the couple as "Total Date Ideas".
    	7.2. System shall display the number of total dates completed by a couple as "Dates Completed".
    	7.3. System shall display the title of the date idea with the greatest number of completions as "Most Popular Idea".
        	7.3.1. System shall not display the "Most Popular Idea" statistic if two or more ideas are tied for the highest number of completions.
        	7.3.2. System shall not display date ideas with a completion count of less than one in the "Most Popular Idea" statistic.
    	7.4. System shall display the date idea with the least number of completions as "Least Popular Idea".
        	7.4.1. System shall not display the "Least Popular Idea" statistic if two or more ideas are tied for the least number of completions.
        	7.4.2. System shall not display date ideas with a completion count of less than one in the "Least Popular Idea" statistic.
    	7.5. System shall display the username of the user in the couple that contributed the most date ideas as "Most Creative".
    	7.6. System shall display the username of the user in the couple that has had their date ideas chosen the most as "Top Contributor".

8. System shall display a "View All" page to the user that shows all date ideas created the user.
    	8.1. System shall allow a user to edit date ideas on the "View All" page.
    	8.2. System shall allow a user to delete date ideas from the "View All" page.

9. System shall display an "Account" page to the user.
    	9.1. System shall display the generated couple code to the user if the user is not in a couple.
    	9.2. System shall allow a user to leave a couple.
    	9.3. System shall allow the user to change their name.

10. System shall display a "Suggested" page to the user.
    	10.1. System shall aggregate the top 100 ideas from other couples in the app based on number of completions.
    	10.2. System shall display 10 ideas per page.
    	10.3. System shall allow user to add an idea to idea pool.
```

# Non-Functional Requirements

```
1. System shall provide updates to ideas for users on page load.
2. System shall utilize the Android platform for the main phone app.
3. System shall use free services for all architectural components.
4. System shall host the API project in an Azure App Service.
5. System shall utilize Azure Key Vault for secrets storage.
6. System shall utilize Azure CosmosDB for data storage.
7. System shall utilize REST for communication between the App and API project.
8. System shall use GitHub to store and track code changes.
9. System shall use GitHub pipelines to deploy code releases.
10. System shall use Microsoft Visual Studio 2022 as a development environment.
11. Systems shall utilize .NET 7 for the developer platform.
```
